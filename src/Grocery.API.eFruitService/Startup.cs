namespace Grocery.API.eFruitService
{
    using AutoMapper;
    using Grocery.Core.Data;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Repositories;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB
            services.AddDbContext<eFruitEntities>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
            services.AddAutoMapper();

            // DI
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<ICartRepository, CartRepository>();

            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddSingleton<IDbFactory, DbFactory>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ICartItemsRepository, CartItemsRepository>();
            services.AddSingleton<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddSingleton<ICustomerCartRepository, CustomerCartRepository>();
            services.AddSingleton<IOrderItemsRepository, OrderItemsRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var client = new eFruitEntities())
            {
                client.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
