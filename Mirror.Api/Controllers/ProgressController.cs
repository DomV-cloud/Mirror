using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Application.Services.Repository.ProgressValues;
using Mirror.Contracts.Request.Progress;
using Mirror.Contracts.Response.Progress;
using Mirror.Domain.Entities;
using System;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("progress")]
    [ErrorHandlingFilter]
    //[Authorize]
    public class ProgressController : Controller
    {
        private readonly ILogger<ProgressController> _logger;
        private readonly IProgressRepository _progressRepository;
        private readonly IProgressValueRepository _progressValueRepository;
        private readonly IMapper _mapper;

        public ProgressController(
            ILogger<ProgressController> logger,
            IProgressRepository progressRepository,
            IMapper mapper,
            IProgressValueRepository progressValueRepository)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
            _progressValueRepository = progressValueRepository;
        }

        [HttpGet("progresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProgressResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllProgresses()
        {
            var progress = await _progressRepository.GetProgressesAsync();

            if (progress.Count == 0 || progress is null)
            {
                return NoContent();
            }

            var response = _mapper.Map<List<ProgressResponse>>(progress);

            return Ok(response);
        }

        [HttpGet("{progressId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgressResponse))]
        public async Task<IActionResult> GetProgressById([FromRoute] Guid progressId)
        {
            if (progressId == Guid.Empty)
            {
                return BadRequest();
            }

            var progress = await _progressRepository.GetProgressesById(progressId);

            if (progress is null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ProgressResponse>(progress);

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProgress([FromBody] CreateProgressRequest request)
        {
            if (request is null)
            {
                _logger.LogWarning("Progress is null");
                return BadRequest();
            }

            var mappedProgress = _mapper.Map<Mirror.Domain.Entities.Progress>(request);
            _logger.LogInformation("Succesfully mapped progress with {ProgressId}", mappedProgress.Id);

            var createdProgress = await _progressRepository.CreateProgress(mappedProgress);
            _logger.LogInformation("Succesfully created progress with {ProgressId}", createdProgress.Id);

            var response = _mapper.Map<ProgressResponse>(createdProgress);
            _logger.LogInformation("Succesfully mapped progress with {ProgressId}", response.CreatedProgressId);

            return CreatedAtAction(nameof(CreateProgress), response.CreatedProgressId, response);
        }
    }
}
