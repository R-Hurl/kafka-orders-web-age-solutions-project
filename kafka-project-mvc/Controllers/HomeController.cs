using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using kafka_project_mvc.Models;
using kafka_project_mvc.Helpers;

namespace kafka_project_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private OrderHelper _orderHelper;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _orderHelper = new OrderHelper();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, ActionName("View")]
        public IActionResult ViewOrders()
        {
            var content = _orderHelper.GetOrders();
            var orders = JsonConvert.DeserializeObject<List<OrderModel>>(content.Result);

            return View(orders);
        }

        [HttpGet, ActionName("Order")]
        public IActionResult PlaceOrder()
        {

            return View();
        }

        [HttpPost, ActionName("Order")]
        public IActionResult PlaceOrder(OrderModel model)
        {
            var response = _orderHelper.PlaceOrder(model).Result;
            ViewBag.Message = response;
            return View();
        }

        [HttpDelete, ActionName("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(string id)
        {
            
            var response = _orderHelper.DeleteOrder(id).Result;
            return View("ViewOrders");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
