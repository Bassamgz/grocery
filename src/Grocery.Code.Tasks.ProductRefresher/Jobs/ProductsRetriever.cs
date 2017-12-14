namespace Grocery.Core.Tasks.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Extension;
    using Microsoft.Extensions.Configuration;
    using PeterKottas.DotNetCore.WindowsService.Base;
    using PeterKottas.DotNetCore.WindowsService.Interfaces;

    internal class ProductsRetriever : MicroService, IMicroService
    {
        private static IConfiguration configuration;
        private readonly IMicroServiceController controller;
        private string apiPath;
        private string getProductsAPI;
        private string putProductsAPI;

        public ProductsRetriever(IMicroServiceController controller, IConfiguration configuration)
        {
            ProductsRetriever.configuration = configuration;
            this.controller = controller;
            getProductsAPI = ProductsRetriever.configuration["GETProductsAPI"];
            putProductsAPI = ProductsRetriever.configuration["PUTProductsAPI"];
            apiPath = ProductsRetriever.configuration["APIURL"];
        }

        public string ServiceName => "Product Refresher";

        public void Stop()
        {
            this.StopBase();
            Console.WriteLine("Product Refresher stopped");
        }

        public void Start()
        {
            this.StartBase();
            Timers.Start(
                "Refresher",
                Convert.ToInt32(configuration["Frequency"]),
                async () =>
            {
                Console.WriteLine($"Job starting {DateTime.Now}");

                var statusGenerator = new Random();
                IEnumerable<Product> products = new List<Product>();

                using (HttpClient httpClient = new HttpClient())
                {
                    Console.WriteLine($"Getting list of all products");

                    httpClient.DefaultRequestHeaders.Add(configuration["Key"], configuration["Pass"]);

                    HttpResponseMessage response = await httpClient.GetAsync($"{getProductsAPI}");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"GET succeeded");
                        products = await response.Content.ReadAsJsonAsync<IEnumerable<Product>>();
                    }

                    Console.WriteLine($"Save/Update each product");

                    // Assumed that valid products will have an Id and name and not for free :)
                    foreach (var product in products.Where(
                        item => item.Id != 0 &&
                        item.Price > 0 &&
                        !string.IsNullOrEmpty(item.Name)))
                    {
                        Console.WriteLine($"Product {product.Id}");

                        // Save/Update each product;
                        try
                        {
                            var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Id", product.Id.ToString()),
                        new KeyValuePair<string, string>("Name", product.Name),
                        new KeyValuePair<string, string>("Price", product.Price.ToString())
                    });

                            response =
                            await
                                httpClient.PutAsync(new Uri($"{apiPath}{putProductsAPI}"), content);

                            Console.WriteLine($"Product is updated {response.IsSuccessStatusCode}");
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            },
            (e) =>
            {
                Console.WriteLine(
                    "Exception while polling: {0}\n",
                    e.ToString());
            });
            Console.WriteLine("Product Refresher started");
        }
    }
}
