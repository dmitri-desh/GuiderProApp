using AutoMapper;
using GuiderPro.Api.DTOs;
using GuiderPro.Core.Entities;
using GuiderPro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuiderPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                _logger.LogWarning($"Category withId = {id} isn't found");

                return NotFound();
            }


            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Category {categoryDto.Name} is invalid");

                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(categoryDto);

            var result = await _categoryService.CreateAsync(category);

            if (result > 0)
            {
                _logger.LogInformation($"Category {category.Name} has been added");
            }

            categoryDto.Id = category.Id;

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Category {categoryDto.Name} is invalid");

                return BadRequest(ModelState);
            }

            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogWarning($"Category with Id = {id} isn't found");

                return NotFound();
            }

            _mapper.Map(categoryDto, category);

            var result = await _categoryService.UpdateAsync(category);

            if (result > 0)
            {
                _logger.LogInformation($"Category {category.Name} has been updated");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (result > 0)
            {
                _logger.LogInformation($"Category with Id = {id} has been removed");
            }

            return NoContent();
        }
    }
}
