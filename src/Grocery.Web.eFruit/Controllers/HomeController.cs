namespace Grocery.Web.eFruit.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Extension;
    using Grocery.Web.eFruit.Extensions;
    using Grocery.Web.eFruit.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class HomeController : Controller
    {
        private static HttpClient httpClient;
        private IConfiguration configuration;
        private string apiPath;
        private string productsAPI;
        private int pageSize;

        public HomeController(
            IConfiguration configuration)
        {
            this.configuration = configuration;
            apiPath = this.configuration["APIURL"];
            productsAPI = this.configuration["ProductAPI"];
            pageSize = Convert.ToInt32(this.configuration["PageSize"]);
        }

        // GET: Products
        public async Task<ActionResult> Index(int? page)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri($"{apiPath}{productsAPI}"))
            {
                Version = HttpVersion.Version10,
                Content = null
            };

            IEnumerable<Product> products = new List<Product>();
            using (httpClient = new HttpClient())
            {
                using (var response =
                    await httpClient.SendAsync(httpRequestMessage))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        products = await response.Content.ReadAsJsonAsync<IEnumerable<Product>>();
                    }
                }

                return View(PaginatedList<Product>.Create(products, page ?? 1, pageSize));
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
