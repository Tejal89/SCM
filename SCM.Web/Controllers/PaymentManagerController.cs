using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCM.Interfaces;
using SCM.Models;
using SCM.Web.Models;

namespace SCM.Web.Controllers
{
    public class PaymentManagerController : Controller
    {
        private readonly ILogger<PaymentManagerController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;

        public PaymentManagerController(ILogger<PaymentManagerController> logger, IProductService productService, IOrderService orderService,ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// <paramref name="productId"/>
        /// Generate the desired response for the given problem statement. Logic is not written in separate function for simplicity
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult GeneratePaySlip(int productId)
        {
            bool packagingSlip = false;
            bool dupPackagingSlip = false;
            bool sendEmail = false;
            bool addFreeProduct = false;



            List<Category> productCategories = _categoryService.GetCategoriesByProductId(productId).ToList();

            return new ContentResult
            {
                ContentType = "text/html",
                Content = "<div>Hello World</div>"
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
