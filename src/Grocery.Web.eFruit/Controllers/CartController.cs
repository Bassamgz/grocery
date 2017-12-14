namespace Grocery.Web.eFruit.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Extension;
    using Grocery.Web.eFruit.Extensions;
    using Grocery.Web.eFruit.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [Authorize]
    public class CartController : Controller
    {
        private static HttpClient httpClient;
        private IConfiguration configuration;
        private string apiPath;
        private string cartsAPI;
        private string ordersAPI;
        private int pageSize;

        public CartController(
            IConfiguration configuration)
        {
            this.configuration = configuration;
            apiPath = this.configuration["APIURL"];
            cartsAPI = this.configuration["CartAPI"];
            ordersAPI = this.configuration["OrderAPI"];
            pageSize = Convert.ToInt32(this.configuration["PageSize"]);
        }

        // GET: View current items in cart
        public async Task<IActionResult> Index(int? page)
        {
            IEnumerable<CartItem> cartItems = new List<CartItem>();
            using (httpClient = new HttpClient())
            {
                var response =
                    await httpClient.GetAsync($"{apiPath}{cartsAPI}/{User.Identity.Name}");
                if (response.IsSuccessStatusCode)
                {
                    cartItems = await response.Content.ReadAsJsonAsync<IEnumerable<CartItem>>();
                }

                return View(PaginatedList<CartItem>.Create(cartItems, page ?? 1, pageSize));
            }
        }

        // POST: Add item to Cart
        public async Task<IActionResult> AddToCartAsync(int productId)
        {
            using (httpClient = new HttpClient())
            {
                var response =
                    await
                        httpClient.PostAsync(new Uri($"{apiPath}{cartsAPI}/{User.Identity.Name}/{productId}"), null);
            }

            return RedirectToAction("Index");
        }

        // DELETE: Item from Cart
        public async Task<IActionResult> DeleteItemFromCartAsync(int productId)
        {
            using (httpClient = new HttpClient())
            {
                var response =
                    await
                        httpClient.DeleteAsync(new Uri($"{apiPath}{cartsAPI}/{User.Identity.Name}/{productId}").ToString());
            }

            return RedirectToAction("Index");
        }

        // POST: Place order
        public async Task<IActionResult> PlaceOrderAsync()
        {
            var result = new OrderResultViewModel();
            using (httpClient = new HttpClient())
            {
                var response =
                                    await
                                        httpClient.PostAsync(new Uri($"{apiPath}{ordersAPI}/{User.Identity.Name}"), null);

                result.IsSuccessfull = response.IsSuccessStatusCode;
                result.OrderReference = await response.Content.ReadAsStringAsync();
            }

            return View("OrderResult", result);
        }
    }
}