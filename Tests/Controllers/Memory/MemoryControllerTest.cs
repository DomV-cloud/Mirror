using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Api.Controllers;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.FileService;
using Mirror.Application.Services.FileService.FilePathGenerator;
using Mirror.Application.Services.Repository.Image;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Contracts.Request.Memory.PUT;
using Mirror.Contracts.Response.Image;
using Mirror.Contracts.Response.Memory;
using Mirror.Domain.Entities;
using Mirror.Domain.Enums.UserMemory;
using Moq;
using System.Data.Common;
using Tests.MockData.Image;
using Tests.MockData.Memory;

namespace Tests.Controllers.Memory
{
    [TestClass]
    public class MemoryControllerTest
    {
        private readonly Mock<ILogger<MemoryController>> _mockLogger;
        private readonly Mock<IUserMemoryRepository> _mockMemoryRepository;
        private readonly Mock<IImageRepository> _mockImageRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IFileService> _mockFileService;
        private readonly Mock<IFilePathGenerator> _mockFilePathGenerator;

        private MemoryController _memoryController;
        private readonly DbConnection _connection;
        private readonly DbContextOptions<MirrorContext> _options;

        public MemoryControllerTest()
        {
            _mockLogger = new Mock<ILogger<MemoryController>>();
            _mockMemoryRepository = new Mock<IUserMemoryRepository>();
            _mockImageRepository = new Mock<IImageRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockFileService = new Mock<IFileService>();
            _mockFilePathGenerator = new Mock<IFilePathGenerator>();

            _memoryController = new(
                _mockLogger.Object,
                _mockMemoryRepository.Object,
                _mockMapper.Object,
                _mockImageRepository.Object);

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<MirrorContext>()
            .UseSqlite(_connection)
            .Options;

            using var context = new MirrorContext(_options);
            context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task Should_Create_Memory_Successfully()
        {
            UserMemoryCreateRequest request = FakeUserMemory.CreateMockUserMemoryRequest().Generate();

            var uploadedImages = new List<Image>();
            foreach (var file in request.Images)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var imageContent = ms.ToArray();

                var uploadedImage = new Image
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    Content = imageContent,
                    ContentType = file.ContentType
                };

                uploadedImages.Add(uploadedImage);

                var mockedFilePath = $"mocked/{file.FileName}";
                _mockFilePathGenerator.Setup(fpg => fpg.GenerateFilePath(It.Is<string>(name => name == file.FileName)))
                    .Returns(mockedFilePath);

                _mockImageRepository.Setup(i => i.UploadImageAsync(It.Is<IFormFile>(img => img.FileName == file.FileName)))
                    .ReturnsAsync(uploadedImage);

                _mockFileService.Setup(f => f.SaveFileToBlob(It.Is<IFormFile>(img => img.FileName == file.FileName)))
                    .Returns(mockedFilePath);
            }

            var newMemory = FakeUserMemory.CreateMockUserMemory(
                userId: request.UserId,
                memoryName: request.MemoryName,
                description: request.Description,
                reminder: (Reminder)Enum.Parse(typeof(Reminder), request.Reminder),
                images: uploadedImages
                )
                .Generate();

            _mockMemoryRepository.Setup(m => m.CreateMemoryAsync(It.IsAny<UserMemory>()))
            .ReturnsAsync(newMemory);

            _mockMapper.Setup(m => m.Map<UserMemory>(It.Is<UserMemoryCreateRequest>(r => r.UserId == request.UserId)))
                        .Returns(newMemory);

            List<ImageResponse> imagesResponse = [];
            foreach (var mappedImage in newMemory.Images)
            {
                var imageResponse = new ImageResponse
                {
                    FileName = mappedImage.FileName,
                    Url = mappedImage.Url,
                    Id = mappedImage.Id,
                    UserMemoryId = mappedImage.UserMemoryId ?? Guid.Empty,
                    ContentType = mappedImage.ContentType,
                    Content = mappedImage.Content,
                };

                imagesResponse.Add(imageResponse);
            }

            var response = FakeUserMemory.CreateMockUserMemoryResponse(
                userId: newMemory.UserId,
                memoryId: newMemory.Id,
                memoryName: newMemory.MemoryName,
                description: newMemory.Description,
                images: imagesResponse,
                reminder: newMemory.Reminder.ToString()
                )
                .Generate();

            var result = await _memoryController.CreateMemory(request);

            response.Should().NotBeNull();
            response!.MemoryId.Should().Be(newMemory.Id);
            response.MemoryName.Should().Be(newMemory.MemoryName);
            response.Description.Should().Be(newMemory.Description);
            response.Reminder.Should().Be(newMemory.Reminder.ToString());
            response.Images.Should().HaveCount(newMemory.Images.Count);
        }

        [TestMethod]
        public async Task Request_Is_Null_Should_Return_BadRequest()
        {
            UserMemoryCreateRequest request = null;

            var result = await _memoryController.CreateMemory(request);

            request.Should().BeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [TestMethod]
        public async Task Image_FileSize_Zero_Should_Not_Be_Added()
        {
            var request = FakeUserMemory.CreateMockUserMemoryRequest().Generate();
            var zeroSizeFile = new Mock<IFormFile>();
            zeroSizeFile.Setup(f => f.Length).Returns(0);
            zeroSizeFile.Setup(f => f.FileName).Returns("empty.jpg");
            var formFiles = new FormFileCollection { zeroSizeFile.Object };
            request.Images = formFiles;

            var newMemory = FakeUserMemory.CreateMockUserMemory(
                userId: request.UserId,
                memoryName: request.MemoryName,
                description: request.Description,
                reminder: (Reminder)Enum.Parse(typeof(Reminder), request.Reminder),
                images: []
            ).Generate();

            var response = FakeUserMemory.CreateMockUserMemoryResponse(
                userId: newMemory.UserId,
                memoryId: newMemory.Id,
                memoryName: newMemory.MemoryName,
                description: newMemory.Description,
                images: [],
                reminder: newMemory.Reminder.ToString()
                )
                .Generate();

            _mockMapper.Setup(m => m.Map<UserMemory>(It.Is<UserMemoryCreateRequest>(r => r.UserId == request.UserId)))
                       .Returns(newMemory);

            _mockMemoryRepository.Setup(m => m.CreateMemoryAsync(It.IsAny<UserMemory>()))
                                 .ReturnsAsync(newMemory);

            _mockMapper.Setup(m => m.Map<UserMemoryResponse>(It.Is<UserMemory>(r => r.UserId == newMemory.UserId)))
                        .Returns(response);

            var result = await _memoryController.CreateMemory(request);

            result.Should().BeOfType<CreatedResult>();

            response!.Images.Should().BeEmpty();
        }

        [TestMethod]
        public async Task Images_Are_Empty_Should_Create_Memory_Successfully()
        {
            var request = FakeUserMemory.CreateMockUserMemoryRequest().Generate();
            request.Images = new FormFileCollection();

            var newMemory = FakeUserMemory.CreateMockUserMemory(
                userId: request.UserId,
                memoryName: request.MemoryName,
                description: request.Description,
                reminder: (Reminder)Enum.Parse(typeof(Reminder), request.Reminder),
                images: []
            ).Generate();

            _mockMapper.Setup(m => m.Map<UserMemory>(It.Is<UserMemoryCreateRequest>(r => r.UserId == request.UserId)))
                       .Returns(newMemory);

            _mockMemoryRepository.Setup(m => m.CreateMemoryAsync(It.IsAny<UserMemory>()))
                                 .ReturnsAsync(newMemory);

            _mockMapper.Setup(m => m.Map<UserMemoryResponse>(newMemory))
                       .Returns(FakeUserMemory.CreateMockUserMemoryResponse(
                           userId: newMemory.UserId,
                           memoryId: newMemory.Id,
                           memoryName: newMemory.MemoryName,
                           description: newMemory.Description,
                           images: [],
                           reminder: newMemory.Reminder.ToString()
                       ).Generate());

            var result = await _memoryController.CreateMemory(request);

            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;
            createdResult!.Value.Should().BeOfType<UserMemoryResponse>();
            var response = createdResult.Value as UserMemoryResponse;

            response.Should().NotBeNull();
            response.Images.Should().BeEmpty();
        }

        [TestMethod]
        public async Task Should_Update_Memory_By_Id_Successfully()
        {
            var memoryId = Guid.NewGuid();
            var request = FakeUserMemory.UpdateMockUserMemoryRequest(
                imagesToDelete: []).Generate();
            var images = FakeImage.CreateMockImage().Generate(5);

            var existingMemory = FakeUserMemory.CreateMockUserMemory(
                userId: Guid.NewGuid(),
                memoryName: "Existing Memory",
                description: "Existing Description",
                images: images
            ).Generate();

            var updatedMemory = FakeUserMemory.CreateMockUserMemory(
                userId: existingMemory.UserId,
                memoryName: request.MemoryName,
                description: request.Description,
                reminder: (Reminder)Enum.Parse(typeof(Reminder), request.Reminder),
                images: existingMemory.Images
            ).Generate();

            _mockMemoryRepository.Setup(m => m.GetMemoryByIdAsync(It.Is<Guid>(id => id == memoryId)))
                                 .ReturnsAsync(existingMemory);

            _mockImageRepository.Setup(i => i.UploadImageAsync(It.IsAny<IFormFile>()))
                                .ReturnsAsync(FakeImage.CreateMockImage().Generate());

            _mockMemoryRepository.Setup(m => m.UpdateMemoryAsync(
                It.Is<UserMemory>(m => m.Id == existingMemory.Id),
                It.Is<UserMemory>(m => m.Id == updatedMemory.Id)))
                                 .ReturnsAsync(true);

            _mockMapper.Setup(m => m.Map<UserMemory>(It.Is<UserMemoryUpdateRequest>(r => r.UserId == request.UserId)))
                       .Returns(updatedMemory);

            var result = await _memoryController.UpdateMemoryById(memoryId, request);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be(updatedMemory);

            _mockMemoryRepository.Verify(m => m.GetMemoryByIdAsync(It.Is<Guid>(id => id == memoryId)), Times.Once);
            _mockImageRepository.Verify(i => i.UploadImageAsync(It.IsAny<IFormFile>()), Times.Exactly(request.NewImages.Count));
            _mockMemoryRepository.Verify(m => m.UpdateMemoryAsync(
                It.Is<UserMemory>(m => m.Id == existingMemory.Id),
                It.Is<UserMemory>(m => m.Id == updatedMemory.Id)), Times.Once);
        }

        [TestMethod]
        public async Task ValidId_Should_Return_Memory()
        {
            var memoryId = Guid.NewGuid();
            var request = FakeUserMemory.UpdateMockUserMemoryRequest(imagesToDelete: []).Generate();

            var existingMemory = FakeUserMemory.CreateMockUserMemory(id: memoryId).Generate();

            _mockMemoryRepository.Setup(m => m.GetMemoryByIdAsync(It.Is<Guid>(id => id == memoryId)))
                .ReturnsAsync(existingMemory);

            var result = await _memoryController.UpdateMemoryById(memoryId, request);

            existingMemory.Should().NotBeNull();
            existingMemory.Id.Should().Be(memoryId);

            _mockMemoryRepository.Verify(m => m.GetMemoryByIdAsync(It.Is<Guid>(id => id == memoryId)));
        }

        [TestMethod]
        [DataRow("00000000-0000-0000-0000-000000000000", null, "Memory ID cannot be empty.", typeof(BadRequestObjectResult))] // memoryId is empty
        [DataRow("2f9c4542-c2b9-4adf-a8fc-3c1d567b3f9d", null, "Request body cannot be null.", typeof(BadRequestObjectResult))] // request is null
        public async Task UpdateMemoryById_Should_Handle_Edge_Cases(string memoryIdString, UserMemoryUpdateRequest? request, string expectedErrorMessage, Type expectedResponseType)
        {
            var memoryId = Guid.Parse(memoryIdString);

            _mockMemoryRepository.Setup(m => m.GetMemoryByIdAsync(memoryId))
                                 .ReturnsAsync((UserMemory)null);

            var result = await _memoryController.UpdateMemoryById(memoryId, request);

            result.Should().BeOfType(expectedResponseType);
            if (result is BadRequestObjectResult badRequest)
            {
                badRequest.Value.Should().Be(expectedErrorMessage);
            }
        }

        [TestMethod]
        public async Task Should_Delete_Memory_By_Valid_Id()
        {
            var memoryIdToDelete = Guid.NewGuid();
            var memoryToDelete = FakeUserMemory.CreateMockUserMemory(
                id: memoryIdToDelete)
                .Generate();

            _mockMemoryRepository
                .Setup(m => m.GetMemoryByIdAsync(It.Is<Guid>(id => id == memoryIdToDelete)))
                .ReturnsAsync(memoryToDelete);

            _mockMemoryRepository
                .Setup(m => m.DeleteMemoryAsync(It.Is<UserMemory>(memory => memory.Id == memoryToDelete.Id)))
                .ReturnsAsync(true);

            var result = await _memoryController.DeleteMemoryById(memoryIdToDelete);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be(true);

            _mockMemoryRepository.Verify(
                m => m.GetMemoryByIdAsync(It.Is<Guid>(id => id == memoryIdToDelete)),
                Times.Once
            );

            _mockMemoryRepository.Verify(
                m => m.DeleteMemoryAsync(It.Is<UserMemory>(memory => memory.Id == memoryIdToDelete)),
                Times.Once
            );
        }
    }
}