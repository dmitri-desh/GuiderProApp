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
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;
        private readonly ILogger<PlacesController> _logger;

        public PlacesController(IPlaceService placeService, IMapper mapper, ILogger<PlacesController> logger)
        {
            _placeService = placeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var places = await _placeService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PlaceDTO>>(places));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var place = await _placeService.GetByIdAsync(id);

            if (place == null)
            {
                _logger.LogWarning($"Place withId = {id} isn't found");

                return NotFound();
            }
               

            var placeDto = _mapper.Map<PlaceDTO>(place);

            return Ok(placeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlaceDTO placeDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Place {placeDto.Name} is invalid");

                return BadRequest(ModelState);
            } 

            var place = _mapper.Map<Place>(placeDto);

            var result = await _placeService.CreateAsync(place, placeDto.TagIds);

            if (result > 0)
            {
                _logger.LogInformation($"Place {place.Name} has been added");
            }

            placeDto.Id = place.Id;

            return CreatedAtAction(nameof(GetById), new { id = place.Id }, placeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlaceDTO placeDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Place {placeDto.Name} is invalid");

                return BadRequest(ModelState);
            }

            var place = await _placeService.GetByIdAsync(id);
            if (place == null)
            {
                _logger.LogWarning($"Place with Id = {id} isn't found");

                return NotFound();
            }   

            //_mapper.Map(placeDto, place);

            place.Name = placeDto.Name;
            place.Description = placeDto.Description;
            place.Address = placeDto.Address;
            place.CategoryId = placeDto.CategoryId;
            place.Category = new Category { Id = placeDto.CategoryId, Name = placeDto.CategoryName };

            var result = await _placeService.UpdateAsync(place, placeDto.TagIds);

            if (result > 0)
            {
                _logger.LogInformation($"Place {place.Name} has been updated");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _placeService.DeleteAsync(id);

            if (result > 0)
            {
                _logger.LogInformation($"Place with Id = {id} has been removed");
            }

            return NoContent();
        }
    }
}
