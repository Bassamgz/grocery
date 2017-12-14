namespace Grocery.Core.Data.Configuration
{
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.Id)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

            builder.Property(e => e.PurchasedOn).HasDefaultValueSql("(getdate())");
        }
    }
}
