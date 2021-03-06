using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCM.Implementation;
using System;
using System.Linq;

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

        [TestMethod]
        public void GetProducts_ReturnsNull()
        {
            Assert.IsNull(_productService.GetProducts());
        }

        [TestMethod]
        public void GetProducts_Success()
        {
            Assert.AreEqual(_productService.GetProducts().Count(),4);
        }
    }
}
