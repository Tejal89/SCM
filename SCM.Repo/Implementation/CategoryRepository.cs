using SCM.Interfaces;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SCM.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository()
        {
        }

        public IEnumerable<Category> GetCategories()
        {
            return new List<Category>()
            {
            new Category{ CategoryId = 1, Name = "Physical"},
            new Category{ CategoryId = 2, Name = "Books"},
            new Category{ CategoryId = 3, Name = "Membership"},
            new Category{ CategoryId = 4, Name = "Digital"},
            };
        }

        public IEnumerable<Category> GetCategoriesByProductId(long ProductId)
        {
            switch (ProductId)
            {
                case 1:
                    return GetCategories().Where(x => x.CategoryId == 1).ToList();
                case 2:
                    return GetCategories().Where(x => x.CategoryId == 2).ToList();
                case 3:
                    return GetCategories().Where(x => x.CategoryId == 4).ToList();
                case 5:
                    return GetCategories().Where(x => x.CategoryId == 3).ToList();
            }

            return null;
        }
    }
}