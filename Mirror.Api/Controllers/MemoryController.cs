using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Image;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Contracts.Request.Memory.PUT;
using Mirror.Contracts.Response.Memory;
using Mirror.Domain.Entities;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("memories")]
    [ErrorHandlingFilter]
    public class MemoryController : Controller
    {
        private readonly ILogger<MemoryController> _logger;
        private readonly IUserMemoryRepository _memoryRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public MemoryController(ILogger<MemoryController> logger, IUserMemoryRepository memoryRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _logger = logger;
            _memoryRepository = memoryRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        /// <summary>
        /// Creates a new memory record.
        /// </summary>
        /// <param name="request">Request body containing memory details.</param>
        /// <returns>Created memory object.</returns>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserMemoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMemory([FromForm] UserMemoryCreateRequest request)
        {
            _logger.LogInformation($"Starting {nameof(CreateMemory)} endpoint.");

            if (request is null)
            {
                _logger.LogWarning("CreateMemory request is null.");
                return BadRequest("Request body cannot be null.");
            }

            var images = new List<Image>();
            if (request.Images?.Count > 0)
            {
                foreach (var formFile in request.Images)
                {
                    if (formFile.Length > 0)
                    {
                        var savedImage = await _imageRepository.UploadImageAsync(formFile);
                        if (savedImage is null)
                        {
                            _logger.LogError("Failed to upload image {ImageName}.", formFile.FileName);
                            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to upload image: {formFile.FileName}");
                        }

                        images.Add(savedImage);
                        _logger.LogInformation("Image {ImageName} successfully uploaded.", formFile.FileName);
                    }
                }
            }

            var mappedMemory = _mapper.Map<UserMemory>(request);
            mappedMemory.Images = images;

            var createdMemory = await _memoryRepository.CreateMemoryAsync(mappedMemory);

            if (createdMemory is null)
            {
                _logger.LogWarning("Failed to create memory.");
                return BadRequest("Memory creation failed.");
            }

            var response = _mapper.Map<UserMemoryResponse>(createdMemory);
            _logger.LogInformation("Memory created successfully with ID {MemoryId}.", createdMemory.Id);

            return Created(nameof(CreateMemory), response);
        }

        /// <summary>
        /// Retrieves a memory by its ID.
        /// </summary>
        /// <param name="memoryId">The ID of the memory to retrieve.</param>
        /// <returns>The memory object.</returns>
        [HttpGet("{memoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMemoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMemoriesById([FromRoute] Guid memoryId)
        {
            _logger.LogInformation($"Starting {nameof(GetAllMemoriesById)} endpoint with ID {memoryId}.");

            if (memoryId == Guid.Empty)
            {
                _logger.LogWarning("Invalid memory ID provided.");
                return BadRequest("Memory ID cannot be empty.");
            }

            var memory = await _memoryRepository.GetMemoryByIdAsync(memoryId);

            if (memory is null)
            {
                _logger.LogWarning("Memory with ID {MemoryId} not found.", memoryId);
                return NotFound($"Memory with ID {memoryId} not found.");
            }

            var response = _mapper.Map<UserMemoryResponse>(memory);
            _logger.LogInformation("Memory retrieved successfully with ID {MemoryId}.", memoryId);

            return Ok(response);
        }

        [HttpPut("{memoryId}")]
        public async Task<IActionResult> UpdateMemoryById([FromRoute] Guid memoryId, [FromForm] UserMemoryUpdateRequest request)
        {
            _logger.LogInformation($"Starting {nameof(UpdateMemoryById)} endpoint with ID {memoryId}.");

            if (memoryId == Guid.Empty)
            {
                _logger.LogWarning("Invalid memory ID provided.");
                return BadRequest("Memory ID cannot be empty.");
            }

            if (request == null)
            {
                _logger.LogWarning("Update memory request is null.");
                return BadRequest("Request body cannot be null.");
            }

            _logger.LogInformation("Fetching existing memory with ID {MemoryId}.", memoryId);

            var existingMemory = await _memoryRepository.GetMemoryByIdAsync(memoryId);

            if (existingMemory == null || existingMemory.Id == Guid.Empty)
            {
                _logger.LogWarning("Memory with ID {MemoryId} not found.", memoryId);
                return NotFound($"Memory with ID {memoryId} not found.");
            }

            if (request.NewImages.Count != 0)
            {
                foreach (var newImage in request.NewImages)
                {
                    var uploadedNewImage = await _imageRepository.UploadImageAsync(newImage);
                    existingMemory.Images.Add(uploadedNewImage);
                }
            }

            _logger.LogInformation("Mapping update request to memory entity.");
            var updatedMemory = _mapper.Map<UserMemory>(request);

            _logger.LogInformation("Updating memory with ID {MemoryId}.", memoryId);
            var isUpdated = await _memoryRepository.UpdateMemoryAsync(existingMemory, updatedMemory);

            if (request.ImagesToDelete.Count != 0 && request.ImagesToDelete != null)
            {
                await _imageRepository.DeleteImageAsync(request.ImagesToDelete);
            }

            if (!isUpdated)
            {
                _logger.LogError("Failed to update memory with ID {MemoryId}.", memoryId);
                return BadRequest("Failed to update memory.");
            }

            _logger.LogInformation("Successfully updated memory with ID {MemoryId}.", memoryId);

            return Ok(updatedMemory);
        }

        [HttpDelete("{memoryId:guid}")]
        public async Task<IActionResult> DeleteMemoryById([FromRoute] Guid memoryId)
        {
            if (memoryId == Guid.Empty)
            {
                _logger.LogWarning("Invalid memory ID provided.");
                return BadRequest("Memory ID cannot be empty.");
            }

            var memoryToDelete = await _memoryRepository.GetMemoryByIdAsync(memoryId);

            if (memoryToDelete is null)
            {
                _logger.LogWarning("Memory with ID {MemoryId} not found.", memoryId);
                return NotFound($"Memory with ID {memoryId} not found.");
            }

            bool isDeleted = await _memoryRepository.DeleteMemoryAsync(memoryToDelete);

            if (!isDeleted)
            {
                _logger.LogError("Failed to delete memory with ID {MemoryId}.", memoryId);
                return BadRequest("Failed to delete memory.");
            }

            return Ok(isDeleted);
        }
    }
}
