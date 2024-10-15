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
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _categoryRepository.GetAllAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _categoryRepository.GetByIdAsync(id);

        public async Task<int> CreateAsync(Category category) => await _categoryRepository.AddAsync(category);

        public async Task<int> UpdateAsync(Category category) => await _categoryRepository.UpdateAsync(category);

        public async Task<int> DeleteAsync(int id) => await _categoryRepository.DeleteAsync(id);
    }
}
