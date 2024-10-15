using GuiderPro.Core.Entities;
using GuiderPro.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _context.Tags.AsNoTracking().ToListAsync();

        public async Task<Tag?> GetByIdAsync(int id) =>
            await _context.Tags.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds) =>
            await _context.Tags.Where(tag => tagIds.Contains(tag.Id)).AsNoTracking().ToListAsync();

        public async Task<int> AddAsync(Tag tag)
        {
            _context.Tags.Add(tag);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;

            _context.Tags.Update(tag);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            var result = -1;

            if (tag != null)
            {
                _context.Tags.Remove(tag);

                result = await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
