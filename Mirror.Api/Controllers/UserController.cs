using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Memory;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Contracts.Response.Memory;
using Mirror.Contracts.Response.Progress;
using Mirror.Domain.Entities;
using System;

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

        public UserController(ILogger<UserController> logger, IProgressRepository progressRepository, IMapper mapper, IUserMemoryRepository memoryRepository)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
            _memoryRepository = memoryRepository;
        }

        [HttpGet("{userId:guid}/progresses")]
        public async Task<IActionResult> GetProgressesByUserId([FromRoute] Guid userId)
        {
            // TODO: I would consider to make some Validation method/service
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var progressesByUser = await _progressRepository.GetProgressesByUserAsync(userId);

            if (progressesByUser is null || progressesByUser.Count == 0)
            {
                return NoContent();
            }

            var response = _mapper.Map<List<ProgressResponse>>(progressesByUser);

            return Ok(response);
        }

        [HttpGet("{userId:guid}/memories")]
        public async Task<IActionResult> GetMemoriesById([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var memories = await _memoryRepository.GetAllMemoryByUserId(userId);

            if (memories is null)
            {
                return NoContent();
            }

            var response = _mapper.Map<List<UserMemoryResponse>>(memories);

            return Ok(response);
        }
    }
}
