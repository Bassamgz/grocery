namespace Grocery.Core.Data
{
    using Grocery.Core.Data.Configuration;
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public partial class eFruitEntities : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public eFruitEntities()
            : base()
        {
        }

        public eFruitEntities(
            DbContextOptions<eFruitEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }

        public virtual DbSet<CartItem> CartItem { get; set; }

        public virtual DbSet<CustomerCart> CustomerCart { get; set; }

        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<OrderItem> OrderItem { get; set; }

        public virtual DbSet<Product> Product { get; set; }

        public virtual void Commit()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemsConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerOrderConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemsConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerCartConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=BEAST\\SQLEXPRESS; Database = eFruit;Trusted_Connection=True;MultipleActiveResultSets=true");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
