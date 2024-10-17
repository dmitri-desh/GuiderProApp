using GuiderPro.Core.Entities;
using GuiderPro.Persistence.Interfaces;
using GuiderPro.Persistence.Repositories;
using GuiderPro.Services.Interfaces;

namespace GuiderPro.Services;

public class PlaceService : IPlaceService
{
    private readonly IPlaceRepository _placeRepository;
    private readonly ITagRepository _tagRepository;

    public PlaceService(IPlaceRepository placeRepository, ITagRepository tagRepository)
    {
        _placeRepository = placeRepository;
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<Place>> GetAllAsync() => await _placeRepository.GetAllAsync();

    public async Task<Place?> GetByIdAsync(int id) => await _placeRepository.GetByIdAsync(id);

    public async Task<int> CreateAsync(Place place, List<int> tagIds) => await _placeRepository.AddAsync(place, tagIds);

    public async Task<int> UpdateAsync(Place place, List<int> tagIds) 
    {
        return await _placeRepository.UpdateAsync(place, tagIds);
    }

    public async Task<int> DeleteAsync(int id) => await _placeRepository.DeleteAsync(id);
}
