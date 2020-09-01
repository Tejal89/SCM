using SCM.Interfaces;
using SCM.Models;
using System.Collections.Generic;

namespace SCM.Implementation
{
	public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService()
		{
            _categoryRepository = new CategoryRepository();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public IEnumerable<Category> GetCategoriesByProductId(long ProductId)
        {
            return _categoryRepository.GetCategoriesByProductId(ProductId);
        }
    }
}