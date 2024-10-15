using GuiderPro.Core.Entities;
using GuiderPro.Persistence.Interfaces;
using GuiderPro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _tagRepository.GetAllAsync();

        public async Task<Tag?> GetByIdAsync(int id) => await _tagRepository.GetByIdAsync(id);

        public async Task<int> CreateAsync(Tag tag) => await _tagRepository.AddAsync(tag);

        public async Task<int> UpdateAsync(Tag tag) => await _tagRepository.UpdateAsync(tag);

        public async Task<int> DeleteAsync(int id) => await _tagRepository.DeleteAsync(id);
    }
}
