using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mirror.Api.Filters;
using Mirror.Application.Services.FileService;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Application.Services.Repository.ProgressValues;
using Mirror.Contracts.Request.Progress.POST;
using Mirror.Contracts.Request.Progress.PUT;
using Mirror.Contracts.Response.Progress;
using Mirror.Domain.Entities;
using System;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("progresses")]
    [ErrorHandlingFilter]
    //[Authorize]
    public class ProgressController : Controller
    {
        private readonly ILogger<ProgressController> _logger;
        private readonly IProgressRepository _progressRepository;
        private readonly IProgressValueRepository _progressValueRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileUploadservice;

        public ProgressController(
            ILogger<ProgressController> logger,
            IProgressRepository progressRepository,
            IMapper mapper,
            IProgressValueRepository progressValueRepository,
            IFileService fileUploadservice)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
            _progressValueRepository = progressValueRepository;
            _fileUploadservice = fileUploadservice;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProgressResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProgresses()
        {
            var progress = await _progressRepository.GetProgressesAsync();

            if (progress.Count == 0)
            {
                return NoContent();
            }

            var response = _mapper.Map<List<ProgressResponse>>(progress);

            return Ok(response);
        }

        [HttpGet("{progressId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgressResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProgressesByProgressId([FromRoute] Guid progressId)
        {
            if (progressId == Guid.Empty)
            {
                return BadRequest();
            }

            var progress = await _progressRepository.GetProgressesByIdAsync(progressId);

            if (progress is null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ProgressResponse>(progress);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgressResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProgressAsync([FromBody] CreateProgressRequest request)
        {
            if (request is null)
            {
                _logger.LogWarning("Progress is null");
                return BadRequest();
            }

            var mappedProgress = _mapper.Map<Mirror.Domain.Entities.Progress>(request);
            _logger.LogInformation("Succesfully mapped progress with {ProgressId}", mappedProgress.Id);

            var createdProgress = await _progressRepository.CreateProgressAsync(mappedProgress);
            _logger.LogInformation("Succesfully created progress with {ProgressId}", createdProgress.Id);

            if (request.image?.Length != 0 && request.image is not null)
            {
                await _fileUploadservice.SaveFileToBlob(request.image);
                _logger.LogInformation("Succesfully uploaded file {FileName}", request.image.Name);
            }
            
            var response = _mapper.Map<ProgressResponse>(createdProgress);
            _logger.LogInformation("Succesfully mapped progress with {ProgressId}", response.CreatedProgressId);

            return Created(nameof(CreateProgressAsync), response);
        }

        [HttpPut("{progressId:guid}")]
        public async Task<IActionResult> PutProgress([FromRoute] Guid progressId, [FromBody] UpdateProgressRequest request)
        {
            if (progressId == Guid.Empty)
            {
                return BadRequest("Invalid ID from URL");
            }

            if (request.UserId == Guid.Empty)
            {
                return BadRequest("ID of the user does not extists.");
            }

            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var existingProgress = await _progressRepository.GetProgressesByIdAsync(progressId);

            if (existingProgress is null)
            {
                return NotFound($"Progress with this ID {progressId} does not exixsts");
            }

            var mappedProgress = _mapper.Map<Mirror.Domain.Entities.Progress>(request);

            if (mappedProgress is null)
            {
                return BadRequest("Invalid request data. Mapping failed.");
            }

            bool isSuccess = await _progressRepository.UpdateProgress(existingProgress, mappedProgress);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok(existingProgress);
        }
    }
}
