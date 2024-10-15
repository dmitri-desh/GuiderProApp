using GuiderPro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Persistence.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds);
        Task<int> AddAsync(Tag tag);
        Task<int> UpdateAsync(Tag tag);
        Task<int> DeleteAsync(int id);
    }
}
