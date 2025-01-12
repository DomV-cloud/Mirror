using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Contracts.Response.Memory;
using Mirror.Contracts.Response.Progress;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("users")]
    [ErrorHandlingFilter]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IProgressRepository _progressRepository;
        private readonly IUserMemoryRepository _memoryRepository;
        private readonly IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            IProgressRepository progressRepository,
            IMapper mapper,
            IUserMemoryRepository memoryRepository)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
            _memoryRepository = memoryRepository;
        }

        /// <summary>
        /// Retrieves all progresses associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        [HttpGet("{userId:guid}/progresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProgressResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProgressesByUserId([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("Invalid user ID provided for progresses.");
                return BadRequest("User ID cannot be empty.");
            }

            _logger.LogInformation("Fetching progresses for user ID {UserId}.", userId);

            var progressesByUser = await _progressRepository.GetProgressesByUserAsync(userId);

            if (progressesByUser == null || progressesByUser.Count == 0)
            {
                _logger.LogInformation("No progresses found for user ID {UserId}.", userId);
                return NoContent();
            }

            var response = _mapper.Map<List<ProgressResponse>>(progressesByUser);
            _logger.LogInformation("Successfully fetched {Count} progresses for user ID {UserId}.", response.Count, userId);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all memories associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        [HttpGet("{userId:guid}/memories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserMemoryResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemoriesById([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("Invalid user ID provided for memories.");
                return BadRequest("User ID cannot be empty.");
            }

            _logger.LogInformation("Fetching memories for user ID {UserId}.", userId);

            var memories = await _memoryRepository.GetAllMemoryByUserIdAsync(userId);

            if (memories == null || memories.Count == 0)
            {
                _logger.LogInformation("No memories found for user ID {UserId}.", userId);
                return NoContent();
            }

            var response = _mapper.Map<List<UserMemoryResponse>>(memories);
            _logger.LogInformation("Successfully fetched {Count} memories for user ID {UserId}.", response.Count, userId);

            return Ok(response);
        }
    }
}