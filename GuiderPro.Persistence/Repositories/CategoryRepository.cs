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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.AsNoTracking().ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> AddAsync(Category category)
        {
            _context.Categories.Add(category);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;

            _context.Categories.Update(category);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            var result = -1;

            if (category != null)
            {
                _context.Categories.Remove(category);

                result = await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
