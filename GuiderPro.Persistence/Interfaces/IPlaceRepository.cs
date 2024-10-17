using GuiderPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Persistence.Interfaces
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetAllAsync();
        Task<Place?> GetByIdAsync(int id);
        Task<int> AddAsync(Place place, List<int> tagIds);
        Task<int> UpdateAsync(Place place, List<int> tagIds);
        Task<int> DeleteAsync(int id);
    }
}
