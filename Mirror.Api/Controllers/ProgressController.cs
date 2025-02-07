using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.FileService;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Application.Services.Repository.ProgressValues;
using Mirror.Contracts.Request.Progress.POST;
using Mirror.Contracts.Request.Progress.PUT;
using Mirror.Contracts.Response.Progress;
using Mirror.Domain.Entities;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("progresses")]
    [ErrorHandlingFilter]
    public class ProgressController : Controller
    {
        private readonly ILogger<ProgressController> _logger;
        private readonly IProgressRepository _progressRepository;
        private readonly IProgressValueRepository _progressValueRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileUploadService;

        public ProgressController(
            ILogger<ProgressController> logger,
            IProgressRepository progressRepository,
            IMapper mapper,
            IProgressValueRepository progressValueRepository,
            IFileService fileUploadService)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
            _progressValueRepository = progressValueRepository;
            _fileUploadService = fileUploadService;
        }

        /// <summary>
        /// Retrieves all progresses.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProgressResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProgresses()
        {
            _logger.LogInformation("Fetching all progresses.");

            var progresses = await _progressRepository.GetProgressesAsync();

            if (progresses.Count == 0)
            {
                _logger.LogInformation("No progresses found.");
                return NoContent();
            }

            var response = _mapper.Map<List<ProgressResponse>>(progresses);
            _logger.LogInformation("Successfully fetched {Count} progresses.", response.Count);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific progress by ID.
        /// </summary>
        /// <param name="progressId">The ID of the progress to retrieve.</param>
        [HttpGet("{progressId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgressResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProgressesByProgressId([FromRoute] Guid progressId)
        {
            if (progressId == Guid.Empty)
            {
                _logger.LogWarning("Invalid progress ID provided.");
                return BadRequest("Invalid progress ID.");
            }

            _logger.LogInformation("Fetching progress with ID {ProgressId}.", progressId);

            var progress = await _progressRepository.GetProgressesByIdAsync(progressId);

            if (progress == null)
            {
                _logger.LogWarning("Progress with ID {ProgressId} not found.", progressId);
                return NotFound($"Progress with ID {progressId} not found.");
            }

            var response = _mapper.Map<ProgressResponse>(progress);
            _logger.LogInformation("Successfully fetched progress with ID {ProgressId}.", progressId);

            return Ok(response);
        }

        /// <summary>
        /// Creates a new progress.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgressResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProgressAsync([FromBody] CreateProgressRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Create progress request is null.");
                return BadRequest("Request cannot be null.");
            }

            _logger.LogInformation("Mapping progress creation request.");

            var mappedProgress = _mapper.Map<Progress>(request);

            _logger.LogInformation("Creating progress in repository.");
            var createdProgress = await _progressRepository.CreateProgressAsync(mappedProgress);

            if (request.image?.Length > 0)
            {
                 _fileUploadService.SaveFileToBlob(request.image);
                _logger.LogInformation("File {FileName} uploaded successfully.", request.image.Name);
            }

            var response = _mapper.Map<ProgressResponse>(createdProgress);
            _logger.LogInformation("Successfully created progress with ID {ProgressId}.", response.CreatedProgressId);

            return Created(nameof(CreateProgressAsync), response);
        }

        /// <summary>
        /// Updates an existing progress.
        /// </summary>
        [HttpPut("{progressId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Progress))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProgress([FromRoute] Guid progressId, [FromBody] UpdateProgressRequest request)
        {
            if (progressId == Guid.Empty)
            {
                _logger.LogWarning("Invalid progress ID in URL.");
                return BadRequest("Invalid progress ID.");
            }

            if (request == null)
            {
                _logger.LogWarning("Update progress request is null.");
                return BadRequest("Request body cannot be null.");
            }

            _logger.LogInformation("Fetching existing progress with ID {ProgressId}.", progressId);

            var existingProgress = await _progressRepository.GetProgressesByIdAsync(progressId);

            if (existingProgress == null || existingProgress.Id == Guid.Empty)
            {
                _logger.LogWarning("Progress with ID {ProgressId} not found.", progressId);
                return NotFound($"Progress with ID {progressId} not found.");
            }

            _logger.LogInformation("Mapping update request to progress entity.");
            var updatedProgress = _mapper.Map<Progress>(request);

            _logger.LogInformation("Updating progress with ID {ProgressId}.", progressId);
            var isProgressUpdated = await _progressRepository.UpdateProgressAsync(existingProgress, updatedProgress);

            if (!isProgressUpdated)
            {
                _logger.LogError("Failed to update progress with ID {ProgressId}.", progressId);
                return BadRequest("Failed to update progress.");
            }

            _logger.LogInformation("Successfully updated progress with ID {ProgressId}.", progressId);

            return Ok(updatedProgress);
        }
    }
}