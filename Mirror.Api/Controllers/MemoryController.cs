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
        private IUserMemoryRepository _memoryRepository;
        private IMapper _mapper;
        private IFileService _fileService;

        public MemoryController(ILogger<MemoryController> logger, IUserMemoryRepository memoryRepository, IMapper mapper, IFileService fileService)
        {
            _logger = logger;
            _memoryRepository = memoryRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMemory([FromForm] UserMemoryCreateRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var images = new List<Image>();
            if (request.Images?.Count > 0)
            {
                foreach (var formFile in request.Images)
                {
                    if (formFile.Length > 0)
                    {
                        var imagePath = await _fileService.ValidateAndSaveFile(formFile, request.UserId);

                        images.Add(new Image
                        {
                            FileName = formFile.FileName,
                            Url = imagePath,
                            ContentType = formFile.ContentType
                        });
                    }
                }
            }

            var mappedMemory = _mapper.Map<UserMemory>(request);

            mappedMemory.Images = images;

            var createdMemory = await _memoryRepository.CreateMemory(mappedMemory);

            if (createdMemory is null)
            {
                return BadRequest();
            }

            var response = _mapper.Map<UserMemoryResponse>(createdMemory);

            return Created(nameof(CreateMemory), response);
        }
    }
}
