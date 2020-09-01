using SCM.Interfaces;
using SCM.Models;
using System.Collections.Generic;

namespace SCM.Implementation
{
	public class ProductService : IProductService
	{
        private IProductRepository _productRepository;
        public ProductService()
		{
            _productRepository = new ProductRepository();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public Product GetProductById(long ProductId)
        {
            return _productRepository.GetProductById(ProductId);
        }
    }
}