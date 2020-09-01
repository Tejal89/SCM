using SCM.Models;
using System.Collections.Generic;

namespace SCM.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(long ProductId);
    }
}
