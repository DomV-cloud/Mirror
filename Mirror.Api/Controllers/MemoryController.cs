using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Domain.Entities;

namespace Mirror.Api.Controllers
{
    [ApiController]
    [Route("memories")]
    [ErrorHandlingFilter]
    public class MemoryController : Controller
    {
        private readonly ILogger<MemoryController> _logger;
        private IUserMemoryRepository _memoryRepository;
        private IMapper _mapper;

        public MemoryController(ILogger<MemoryController> logger, IUserMemoryRepository memoryRepository, IMapper mapper)
        {
            _logger = logger;
            _memoryRepository = memoryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMemory([FromBody] UserMemoryCreateRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var mappedMemory =  _mapper.Map<UserMemory>(request);

            var createdMemory = await _memoryRepository.CreateMemory(mappedMemory);

            if (createdMemory is null)
            {
                return BadRequest();
            }

            return View(createdMemory);
        }
    }
}
