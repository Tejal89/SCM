using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCM.Interfaces;
using SCM.Models;
using SCM.Web.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using F = System.IO;

namespace SCM.Web.Controllers
{
    public class PaymentManagerController : Controller
    {
        private readonly ILogger<PaymentManagerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;

        public PaymentManagerController(ILogger<PaymentManagerController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment,
            IProductService productService, IOrderService orderService, ICategoryService categoryService)
        {
            _logger = logger;
            _configuration = configuration;
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
        public IActionResult GeneratePaySlip(int productId, string emailAddress)
        {
            Product product = null;
            Order order;
            List<Product> products;
            List<Category> productCategories;
            StringBuilder content = new StringBuilder();
            StringBuilder html = new StringBuilder();
            decimal totalAmount = 0;

            products = _productService.GetProducts().ToList();

            if (products != null && products.Any(x => x.ProductId == productId))
            {
                product = products.FirstOrDefault(x => x.ProductId == productId);
            }
            else
            {
                content.Append("<div>Product not found</div>");
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = content.ToString()
                };
            }

            content.Append("<a href=\"" + Url.Action("Index", "PaymentManager") + "\">Back</a>");

            productCategories = _categoryService.GetCategoriesByProductId(productId).ToList();
            order = _orderService.GetOrders().ToList().FirstOrDefault(x => x.OrderItems.Any(x => x.ProductId == productId));

            //generate packing slip for all categories except membership
            if (productCategories.Any(x => x.CategoryId != 3))
            {
                html.Append(F.File.ReadAllText(F.Path.Combine(_hostingEnvironment.WebRootPath, "templates", "PackingSlipTemplate.html")));

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

                //check if free items need to be appended
                if (productCategories.Any(x => x.CategoryId == 4))
                {
                    product = products.FirstOrDefault(x => x.ProductId == 4);
                    lineItems.Append("<tr style=\"border:1px solid #000;text-align:right;\"><td>" + product.Name + "</td><td>1</td><td><strong>Free</strong></td></tr>");
                }

                lineItems.Append("<tr style=\"border:1px solid #000;text-align:right;\"><td></td><td></td><td><strong>" + totalAmount + "</strong></td></tr>");
                lineItems.Append("</tbody><table>");

                html.Replace("{LINEITEMS}", lineItems.ToString());
                content.Append(html);

                //for books, duplicate packing slip needed
                if (productCategories.Any(x => x.CategoryId == 2))
                {
                    content.Append("</hr>");
                    content.Append("<strong>For Royalty Department</strong>");
                    content.Append("</hr>");
                    content.Append(html);
                }
            }

            //if this is a membership activate/upgrade it. Email functionality pending
            if (productCategories.Any(x => x.CategoryId == 3))
            {
                content.Append("<div class=\"row\" style=\"height:100px;\">");
                content.Append("<div class=\"col-md-12\">");

                if (!order.IsMemberShipActive)
                {
                    content.Append("Membership Activated. Please check your email for details");
                }
                else
                {
                    content.Append("Membership Upgraded for one year. Please check your email for details");
                }

                content.Append("</div>");
                content.Append("</div>");

                if (!string.IsNullOrEmpty(emailAddress))
                {
                    SendMemberShipEmail(!order.IsMemberShipActive, emailAddress);
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

        /// <summary>
        /// Send Email for Membership Activation/Upgrade. Templates can be used but avoided for simplicity
        /// </summary>
        /// <param name="isActivated"></param>
        /// <param name="emailAddress"></param>
        private void SendMemberShipEmail(bool isActivated, string emailAddress)
        {
            string subject = "Congratulations! MemberShip Activated";
            string body = "Membership Activated. Enjoy a whole new world of technology";
            if (!isActivated) //its an upgrade;
            {
                subject = "Congratulations! MemberShip Upgraded";
                body = "Membership Upgraded. Continue enjoying a whole new world of technology";
            }

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            _configuration.GetValue<string>("Smtp:FromAddress"));
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            emailAddress);
            message.To.Add(to);

            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>" + body + "</h1>";
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect(_configuration.GetValue<string>("Smtp:Server"), _configuration.GetValue<int>("Smtp:Port"), true);
                client.Authenticate(_configuration.GetValue<string>("Smtp:FromUser"), _configuration.GetValue<string>("Smtp:FromPassword"));
                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                //swallow exception if sending failed as this is just a demo
                //Enhancement will be to log this using Logger
            }
            client.Dispose();

        }
    }
}
