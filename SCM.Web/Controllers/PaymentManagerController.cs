using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
            IProductService productService, IOrderService orderService, ICategoryService categoryService)
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
            Product product = _productService.GetProductById(productId);
            List<Category> productCategories;
            List<Order> productOrders;
            StringBuilder content = new StringBuilder();
            StringBuilder html = new StringBuilder();
            decimal totalAmount = 0;
            
            if (product == null)
            {
                content.Append("<div>Product not found</div>");
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = content.ToString()
                };
            }

            content.Append("<a href=\"" + Url.Action("Index", "PaymentManager") + "\">Back</a>");

            productCategories =_categoryService.GetCategoriesByProductId(productId).ToList();
            productOrders = _orderService.GetOrders().ToList();

            //for physical products and books, generate packing slip , and duplicate packing slip if needed
            if (productCategories.Any(x => x.CategoryId == 1) || productCategories.Any(x => x.CategoryId == 2)) 
            {
                html.Append(F.File.ReadAllText(F.Path.Combine(_hostingEnvironment.WebRootPath, "templates", "PackingSlipTemplate.html")));

                Order order = productOrders.FirstOrDefault(x => x.OrderItems.Any(x => x.ProductId == productId));

                html = html.Replace("{ORDER}", order.InvoiceNo);
                html = html.Replace("{SHIPTO}", order.ShippingAddress);
                html = html.Replace("{BILLTO}", order.BillingAddress);

                StringBuilder lineItems = new StringBuilder();
                lineItems.Append("<table style=\"width:100%;border:1px solid #000;text-align:right;\"><thead><tr><th>Item</th><th>Qty</th><th>Price</th></tr></thead><tbody>");


                foreach (OrderItem item in order.OrderItems)
                {
                    lineItems.Append("<tr style=\"border:1px solid #000;text-align:right;\"><td>" + product.Name + "</td><td>" + item.Quantity + "</td><td><strong>" + item.Quantity * item.UnitPrice + "</strong></td></tr>");
                    totalAmount += item.Quantity * item.UnitPrice;
                }

                lineItems.Append("<tr style=\"border:1px solid #000;text-align:right;\"><td></td><td></td><td><strong>" + totalAmount + "</strong></td></tr>");
                lineItems.Append("</tbody><table>");

                html.Replace("{LINEITEMS}", lineItems.ToString());
                content.Append(html);

                if (productCategories.Any(x => x.CategoryId == 2)) //for books, generate  duplicate packing slip also 
                {
                    content.Append("</hr>");
                    content.Append("<strong>For Royalty Department</strong>");
                    content.Append("</hr>");
                    content.Append(html);
                }
            }

            if (html.Length > 0)
            {
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
