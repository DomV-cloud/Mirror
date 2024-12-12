using AutoMapper;
using Azure;
using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Api.Controllers;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Application.Services.Repository.ProgressValues;
using Mirror.Contracts.Request.Progress;
using Mirror.Contracts.Request.ProgressValue;
using Mirror.Contracts.Response.Progress;
using Mirror.Domain.Entities;
using Mirror.Domain.Enums.Progress;
using Mirror.Infrastructure.Mapper.Progress;
using Moq;
using System.Data.Common;
using Tests.MockData.Progress;

namespace Tests.Controllers.Progress
{
    [TestClass]
    public class ProgressControllerTest
    {
        private readonly Mock<IProgressRepository> _mockProgressRepository;
        private readonly Mock<IProgressValueRepository> _mockProgressValueRepository;
        private readonly Mock<ILogger<ProgressController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        private ProgressController _controller;

        private readonly DbConnection _connection;
        private readonly DbContextOptions<MirrorContext> _options;

        public ProgressControllerTest()
        {
            _mockProgressRepository = new Mock<IProgressRepository>();
            _mockLogger = new Mock<ILogger<ProgressController>>();
            _mockProgressValueRepository = new Mock<IProgressValueRepository>();
            _mockMapper = new Mock<IMapper>();

            _controller = new(_mockLogger.Object,
                _mockProgressRepository.Object,
                _mockMapper.Object,
                _mockProgressValueRepository.Object
                );

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<MirrorContext>()
            .UseSqlite(_connection)
            .Options;

            using var context = new MirrorContext(_options);
            context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task GetProgressById_ShouldReturnNotFound_WhenProgressDoesNotExist()
        {

        }

        [TestMethod]
        public async Task GetProgressById_ShouldReturnBadRequest_WhenProgressIdIsEmpty()
        {
            var progressId = Guid.Empty;

            var result = await _controller.GetProgressById(progressId);

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]

        public async Task GetProgressById_ShouldReturnProgressResponse_WhenProgressIdIsValid()
        {
            var progressId = Guid.NewGuid();

            var mockedProgress = FakeProgress.GetProgressFaker(
                progressValueCount: 5, 
                progressName: "Test Progress",
                id: progressId
                ).Generate();

            var mappedResponse = new ProgressResponse()
            {
                CreatedProgressId = mockedProgress.Id,
                ProgressValue = mockedProgress.ProgressValue.Select(dto => new ProgressValueDTO(dto.ProgressColumnHead, dto.ProgressColumnValue, dto.ProgressDate_Day, dto.ProgressDate_Month, dto.ProgressDate_Year)).ToList(),
                ProgressName = mockedProgress.ProgressName,
                Description = mockedProgress?.Description,
            };

            _mockProgressRepository.Setup(m => m.GetProgressesById(It.Is<Guid>(id => id == progressId)))
                .ReturnsAsync(mockedProgress);
            _mockMapper.Setup(m => m.Map<ProgressResponse>(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == mockedProgress.Id)))
                .Returns(mappedResponse);

            var result = await _controller.GetProgressById(progressId);

            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(mappedResponse);

            _mockProgressRepository.Verify(m => m.GetProgressesById(It.Is<Guid>(id => id == progressId)), Times.Once);
            _mockMapper.Verify(m => m.Map<ProgressResponse>(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == mockedProgress.Id)), Times.Once);
        }

        [TestMethod]
        public async Task CreateProgress_ShouldReturnCreatedAtAction_WhenRequestIsValid()
        {
            var progressValueDTOs = FakeProgressValue.GenerateProgressValueDTO(3);
            var request = new CreateProgressRequest("Test Progress", progressValueDTOs, Guid.NewGuid());

            var mappedProgress = new Mirror.Domain.Entities.Progress
            {
                Id = Guid.NewGuid(),
                ProgressName = request.ProgressName,
                UserId = request.UserId,
                ProgressValue = progressValueDTOs.Select(dto => new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = dto.ProgressColumnHead,
                    ProgressColumnValue = dto.ProgressColumnValue,
                    ProgressDate_Day = dto.ProgressDate_Day,
                    ProgressDate_Month = dto.ProgressDate_Month,
                    ProgressDate_Year = dto.ProgressDate_Year,
                }).ToList()
            };

            var createdProgress = new Mirror.Domain.Entities.Progress
            {
                Id = Guid.NewGuid(),
                ProgressName = request.ProgressName,
                UserId = request.UserId,
                ProgressValue = mappedProgress.ProgressValue
            };

            var response = new ProgressResponse
            {
                CreatedProgressId = createdProgress.Id,
                ProgressName = createdProgress.ProgressName,
                ProgressValue = progressValueDTOs
            };

            _mockMapper.Setup(m => m.Map<Mirror.Domain.Entities.Progress>(request)).Returns(mappedProgress);
            _mockProgressRepository.Setup(r => r.CreateProgress(mappedProgress)).ReturnsAsync(createdProgress);
            _mockMapper.Setup(m => m.Map<ProgressResponse>(createdProgress)).Returns(response);

            var result = await _controller.CreateProgress(request);

            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.Value.Should().BeEquivalentTo(response);

            _mockProgressRepository.Verify(r => r.CreateProgress(It.Is<Mirror.Domain.Entities.Progress>(p =>
                p.ProgressName == mappedProgress.ProgressName &&
                p.UserId == mappedProgress.UserId &&
                p.ProgressValue.Count == mappedProgress.ProgressValue.Count)), Times.Once);

            _mockMapper.Verify(m => m.Map<Mirror.Domain.Entities.Progress>(request), Times.Once);
            _mockMapper.Verify(m => m.Map<ProgressResponse>(createdProgress), Times.Once);
        }
    }
}
