using GuiderPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task<int> CreateAsync(Tag tag);
        Task<int> UpdateAsync(Tag tag);
        Task<int> DeleteAsync(int id);
    }
}
