using SCM.Interfaces;
using SCM.Models;
using System;
using System.Collections.Generic;

namespace SCM.Implementation
{
	public class ProductRepository : IProductRepository
	{
		public ProductRepository()
		{
		}

        public IEnumerable<Product> GetProducts()
        {
            return null;
        }
    }
}