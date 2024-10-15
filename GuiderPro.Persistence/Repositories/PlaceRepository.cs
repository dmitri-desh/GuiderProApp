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
    public class PlaceRepository : IPlaceRepository
    {
        private readonly AppDbContext _context;

        public PlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Place>> GetAllAsync() =>
            await _context.Places.Include(p => p.Category).Include(p => p.Tags).AsNoTracking().ToListAsync();

        public async Task<Place?> GetByIdAsync(int id) => 
            await _context.Places
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<int> AddAsync(Place place)
        {
            _context.Places.Add(place);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Place place)
        {
            _context.Entry(place).State = EntityState.Modified;

            _context.Places.Update(place);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var place = await _context.Places.FindAsync(id);

            var result = -1;

            if (place != null)
            {
                _context.Places.Remove(place);

                result = await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
