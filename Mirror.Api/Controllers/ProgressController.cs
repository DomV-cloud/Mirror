using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Contracts.Progress;
using Mirror.Domain.Entities;
using Mirror.Infrastructure.Services.Repository.Progress;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var progress = await _progressRepository.GetProgressesAsync();

                var response = _mapper.Map<List<ProgressDTO>>(progress);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
