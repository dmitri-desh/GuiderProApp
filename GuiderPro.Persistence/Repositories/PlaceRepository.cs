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

        public async Task<int> AddAsync(Place place, List<int> tagIds)
        {
            place.Tags.Clear();

            _context.Places.Add(place);

           var result = await _context.SaveChangesAsync();

            if (tagIds.Count > 0)
            {
                var tagsToAdd = await _context.Tags.Where(t => tagIds.Contains(t.Id)).AsNoTracking().ToListAsync();

                foreach (var tag in tagsToAdd)
                {
                    place.Tags.Add(tag);
                }

                _context.Places.Update(place);

               result = await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<int> UpdateAsync(Place place, List<int> tagIds)
        {
            _context.Places.Update(place);
            _context.Entry(place).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();

            if (tagIds.Count > 0)
            {
                var existingTagIds = place.Tags.Select(t => t.Id).ToList();

                var tagsToRemove = place.Tags.Where(t => !tagIds.Contains(t.Id)).ToList();
                foreach (var tag in tagsToRemove)
                {
                    place.Tags.Remove(tag);
                }

                 var tagsToAdd = await _context.Tags
                    .Where(t => tagIds.Contains(t.Id) && !existingTagIds.Contains(t.Id))
                    .ToListAsync();

                foreach (var tag in tagsToAdd)
                {
                    place.Tags.Add(tag);
                }               
            }
            else
            {
                place.Tags.Clear();
            }

            _context.Places.Update(place);
            _context.Entry(place).State = EntityState.Modified;

            result = await _context.SaveChangesAsync();

            return result;
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
