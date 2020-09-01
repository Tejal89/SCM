using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCM.Implementation;
using System;

namespace SCM.Test
{
    [TestClass]
    public class ProductTest
    {
        private readonly ProductService _productService;

        public ProductTest()
        {
            _productService = new ProductService();
        }

        [TestMethod]
        public void GetProducts_ThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(() => _productService.GetProducts());
        }
    }
}
