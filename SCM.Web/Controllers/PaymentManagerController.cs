using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SCM.Interfaces;
using SCM.Models;
using SCM.Web.Models;
using F = System.IO;

namespace SCM.Web.Controllers
{
    public class PaymentManagerController : Controller
    {
        private readonly ILogger<PaymentManagerController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PaymentManagerController(ILogger<PaymentManagerController> logger, IWebHostEnvironment hostingEnvironment,
            IProductService productService, IOrderService orderService,ICategoryService categoryService)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
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
            StringBuilder content = new StringBuilder();
            content.Clear();
            Product product = _productService.GetProductById(productId);

            if (product == null)
            {
                content.Append("<div>Product not found</div>");
            }

            List<Category> productCategories = _categoryService.GetCategoriesByProductId(productId).ToList();

            if (productCategories.Any(x => x.CategoryId == 1) || productCategories.Any(x => x.CategoryId == 2)) //for physical products and books, generate packing slip 
            {
                content.Append(F.File.ReadAllText(F.Path.Combine(_hostingEnvironment.WebRootPath, "templates", "PackingSlipTemplate.html")));

                content.Append("<a href=\"" + Url.Action("Index", "PaymentManager") + "\">Back</a>");
            }

            return new ContentResult
            {
                ContentType = "text/html",
                Content = content.ToString()
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
