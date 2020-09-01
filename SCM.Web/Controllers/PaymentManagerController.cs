using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCM.Interfaces;
using SCM.Web.Models;

namespace SCM.Web.Controllers
{
    public class PaymentManagerController : Controller
    {
        private readonly ILogger<PaymentManagerController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public PaymentManagerController(ILogger<PaymentManagerController> logger, IProductService productService, IOrderService orderService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GeneratePaySlip(int productId)
        {
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
