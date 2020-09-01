using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCM.Implementation;
using System;
using System.Linq;

namespace SCM.Test
{
    [TestClass]
    public class CategoryTest
    {
        private readonly CategoryService _categoryService;

        public CategoryTest()
        {
            _categoryService = new CategoryService();
        }

        [TestMethod]
        public void GetCategories_ThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(() => _categoryService.GetCategories());
        }

        [TestMethod]
        public void GetCategories_ReturnsNull()
        {
            Assert.IsNull(_categoryService.GetCategories());
        }

        [TestMethod]
        public void GetCategories_Success()
        {
            Assert.AreEqual(_categoryService.GetCategories().Count(),4);
        }

        [TestMethod]
        public void GetCategoriesByProductId_ThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(() => _categoryService.GetCategoriesByProductId(1));
        }

        [TestMethod]
        public void GetCategoriesByProductId_ReturnsNull()
        {
            Assert.IsNull(_categoryService.GetCategoriesByProductId(1));
        }

        [TestMethod]
        public void GetCategoriesByProductId_Success()
        {
            Assert.AreEqual(_categoryService.GetCategoriesByProductId(1).Count(), 1);
        }
    }
}
