using SCM.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SCM.Interfaces
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
        public IEnumerable<Category> GetCategoriesByProductId(long ProductId);       
    }
}
