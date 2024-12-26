using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mirror.Api.Filters;
using Mirror.Application.Services.Repository.Progresses;
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
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IProgressRepository progressRepository, IMapper mapper)
        {
            _logger = logger;
            _progressRepository = progressRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId:guid}/progresses")]
        public async Task<IActionResult> GetProgressesByUserId([FromRoute] Guid userId)
        {
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
    }
}
