namespace Grocery.Web.eFruit.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Extension;
    using Grocery.Web.eFruit.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [Authorize]
    public class OrderController : Controller
    {
        private static HttpClient httpClient;
        private IConfiguration configuration;
        private string apiPath;
        private string ordersAPI;
        private int pageSize;

        public OrderController(
            IConfiguration configuration)
        {
            this.configuration = configuration;
            apiPath = this.configuration["APIURL"];
            ordersAPI = this.configuration["OrderAPI"];
            pageSize = Convert.ToInt32(this.configuration["PageSize"]);
        }

        // GET: Orders for user
        public async Task<ActionResult> Index(int? page)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri($"{apiPath}{ordersAPI}/{User.Identity.Name}"))
            {
                Version = HttpVersion.Version10,
                Content = null
            };

            IEnumerable<Order> products = new List<Order>();
            using (httpClient = new HttpClient())
            {
                using (var response =
                    await httpClient.SendAsync(httpRequestMessage))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        products = await response.Content.ReadAsJsonAsync<IEnumerable<Order>>();
                    }
                }

                return View(PaginatedList<Order>.Create(products, page ?? 1, pageSize));
            }
        }
    }
}