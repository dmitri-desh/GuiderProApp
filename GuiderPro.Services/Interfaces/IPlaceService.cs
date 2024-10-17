using GuiderPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<IEnumerable<Place>> GetAllAsync();
        Task<Place?> GetByIdAsync(int id);
        Task<int> CreateAsync(Place place, List<int> tagIds);
        Task<int> UpdateAsync(Place place, List<int> tagIds);
        Task<int> DeleteAsync(int id);
    }
}
