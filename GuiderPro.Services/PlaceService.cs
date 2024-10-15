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

    public async Task<int> CreateAsync(Place place) => await _placeRepository.AddAsync(place);

    public async Task<int> UpdateAsync(Place place)
    {
        var tagIds = place.Tags.Select(t => t.Id).ToList();

        if (place.Tags != null && place.Tags.Count > 0)
        {
            var tags = await _tagRepository.GetTagsByIdsAsync(tagIds);

            place.Tags = tags;
        }
        else
        {
            place.Tags?.Clear(); 
        }

        return await _placeRepository.UpdateAsync(place);
    }

    public async Task<int> DeleteAsync(int id) => await _placeRepository.DeleteAsync(id);
}
