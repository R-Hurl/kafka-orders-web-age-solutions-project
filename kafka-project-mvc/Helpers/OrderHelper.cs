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

namespace kafka_project_mvc.Helpers
{
  public class OrderHelper
  {
        public async Task<string> GetOrders()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9090");

            var response = client.GetAsync("/api/order").Result;
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PlaceOrder(OrderModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9090");

            string message = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync("/api/order", new StringContent(message, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteOrder(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9090");

            var response = client.DeleteAsync($"/api/order/{id}").Result;
            return await response.Content.ReadAsStringAsync();
        }

  }
}