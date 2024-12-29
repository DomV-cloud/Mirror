using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.FileService;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Contracts.Request.Memory.POST;
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
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MemoryController(ILogger<MemoryController> logger, IUserMemoryRepository memoryRepository, IMapper mapper, IFileService fileService)
        {
            _logger = logger;
            _memoryRepository = memoryRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        /// <summary>
        /// Creates a new memory record.
        /// </summary>
        /// <param name="request">Request body containing memory details.</param>
        /// <returns>Created memory object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserMemoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMemory([FromForm] UserMemoryCreateRequest request)
        {
            _logger.LogInformation("Starting CreateMemory endpoint.");

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
                        var savedImage = await _fileService.SaveFileToBlob(formFile);
                        images.Add(savedImage);
                        _logger.LogInformation("Image {ImageName} successfully uploaded.", formFile.FileName);
                    }
                }
            }

            var mappedMemory = _mapper.Map<UserMemory>(request);
            mappedMemory.Images = images;

            var createdMemory = await _memoryRepository.CreateMemory(mappedMemory);

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
            _logger.LogInformation("Starting GetAllMemoriesById endpoint with ID {MemoryId}.", memoryId);

            if (memoryId == Guid.Empty)
            {
                _logger.LogWarning("Invalid memory ID provided.");
                return BadRequest("Memory ID cannot be empty.");
            }

            var memory = await _memoryRepository.GetMemoryById(memoryId);

            if (memory is null)
            {
                _logger.LogWarning("Memory with ID {MemoryId} not found.", memoryId);
                return NotFound($"Memory with ID {memoryId} not found.");
            }

            var response = _mapper.Map<UserMemoryResponse>(memory);
            _logger.LogInformation("Memory retrieved successfully with ID {MemoryId}.", memoryId);

            return Ok(response);
        }
    }
}
