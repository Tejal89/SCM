using SCM.Models;
using System.Collections.Generic;

namespace SCM.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategoriesByProductId(long ProductId);       
    }
}
