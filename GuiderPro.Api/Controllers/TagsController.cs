using AutoMapper;
using GuiderPro.Api.DTOs;
using GuiderPro.Core.Entities;
using GuiderPro.Services;
using GuiderPro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuiderPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        private readonly ILogger<TagsController> _logger;

        public TagsController(ITagService tagService, IMapper mapper, ILogger<TagsController> logger)
        {
            _tagService = tagService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<TagDTO>>(tags));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);

            if (tag == null)
            {
                _logger.LogWarning($"Tag withId = {id} isn't found");

                return NotFound();
            }


            var tagDto = _mapper.Map<TagDTO>(tag);

            return Ok(tagDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagDTO tagDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Tag {tagDto.Name} is invalid");

                return BadRequest(ModelState);
            }

            var tag = _mapper.Map<Tag>(tagDto);

            var result = await _tagService.CreateAsync(tag);

            if (result > 0)
            {
                _logger.LogInformation($"Tag {tag.Name} has been added");
            }

            tagDto.Id = tag.Id;

            return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tagDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TagDTO tagDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Tag {tagDto.Name} is invalid");

                return BadRequest(ModelState);
            }

            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
            {
                _logger.LogWarning($"Tag with Id = {id} isn't found");

                return NotFound();
            }

            _mapper.Map(tagDto, tag);

            var result = await _tagService.UpdateAsync(tag);

            if (result > 0)
            {
                _logger.LogInformation($"Tag {tag.Name} has been updated");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tagService.DeleteAsync(id);

            if (result > 0)
            {
                _logger.LogInformation($"Tag with Id = {id} has been removed");
            }

            return NoContent();
        }
    }
}
