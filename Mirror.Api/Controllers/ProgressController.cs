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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedProgressResponse))]
        public async Task<IActionResult> GetProgressById([FromRoute] Guid progressId)
        {
            if (progressId == Guid.Empty)
            {
                return BadRequest();
            }

            var progress = await _progressRepository.GetProgressesById(progressId);

            var response = _mapper.Map<CreatedProgressResponse>(progress);

            return Ok(response);
        }

        [HttpPost("progress")]
        public async Task<IActionResult> CreateProgress([FromBody] CreateProgressRequest progress)
        {
            if (progress is null)
            {
                return BadRequest();
            }

            var mappedProgress = _mapper.Map<Mirror.Domain.Entities.Progress>(progress);

            var createdProgress = await _progressRepository.CreateProgress(mappedProgress);

            var response = _mapper.Map<CreatedProgressResponse>(createdProgress);

            return CreatedAtAction(nameof(GetProgressById), response.CreatedProgressId, response);
        }

        [HttpGet("{progressId:guid}/progress-values")]
        public async Task<IActionResult> GetAllProgressValuesByProgressId([FromRoute] Guid progressId)
        {
            if (progressId == Guid.Empty)
            {
                return BadRequest();
            }

            var progressValues = await _progressValueRepository.GetProgressValueByProgressAsync(progressId);

            return Ok(progressValues);
        }
    }
}
