using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Contracts.Progress;
using Mirror.Domain.Entities;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("progress")]
    [ErrorHandlingFilter]
    //[Authorize]
    public class ProgressController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IProgressRepository _progressRepository;
        private readonly IMapper _mapper;

        public ProgressController(
            ILogger<AuthenticationController> logger,
            IProgressRepository progressRepository,
            IMapper mapper)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
        }

        [HttpGet("all", Name = "all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProgressDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var progress = await _progressRepository.GetProgressesAsync();

            var response = _mapper.Map<List<ProgressDTO>>(progress);

            return Ok(response);
        }

        [HttpPost("create-progress", Name = "create-progress")]
        public async Task<IActionResult> CreateProgress([FromBody] CreateProgressDTO progress)
        {
            if (progress is null)
            {
                return BadRequest();
            }

            var mappedProgress = _mapper.Map<Mirror.Domain.Entities.Progress>(progress);

            var response = await _progressRepository.CreateProgress(mappedProgress);

            return Ok(response);
        }
    }
}
