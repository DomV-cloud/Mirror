using AutoMapper;
using FluentAssertions;
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
        public async Task CreateProgress_ShouldReturnCreatedAtAction_WhenRequestIsValid()
        {
            var progressValues = FakeProgress.GenerateProgressValues(3);
            var request = new CreateProgressRequest("Test Progress", FakeProgressValue.GenerateProgressValueDTO(), Guid.NewGuid());

            var mappedProgress = FakeProgress.GetProgressFaker(progressValueCount: 3, progressName: request.ProgressName)
                .Generate();

            var createdProgress = new Mirror.Domain.Entities.Progress
            {
                Id = Guid.NewGuid(),
                ProgressName = request.ProgressName,
                ProgressValue = mappedProgress.ProgressValue
            };

            var response = new CreatedProgressResponse
            {
                CreatedProgressId = createdProgress.Id,
                ProgressName = createdProgress.ProgressName,
                ProgressValue = FakeProgressValue.GenerateProgressValueDTO()
            };

            _mockMapper.Setup(m => m.Map<Mirror.Domain.Entities.Progress>(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == request.UserId))).Returns(mappedProgress);
            _mockProgressRepository.Setup(r => r.CreateProgress(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == mappedProgress.Id))).ReturnsAsync(createdProgress);
            _mockMapper.Setup(m => m.Map<CreatedProgressResponse>(createdProgress)).Returns(response);

            var result = await _controller.CreateProgress(request);

            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.Value.Should().BeEquivalentTo(response);

            _mockProgressRepository.Verify(r => r.CreateProgress(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == request.UserId)), Times.Once);
            _mockMapper.Verify(m => m.Map<Mirror.Domain.Entities.Progress>(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == mappedProgress.Id)), Times.Once);
            _mockMapper.Verify(m => m.Map<CreatedProgressResponse>(It.Is<Mirror.Domain.Entities.Progress>(p => p.Id == createdProgress.Id)), Times.Once);
        }
    }
}
