namespace Grocery.Core.Data.Configuration
{
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CustomerCartConfiguration : IEntityTypeConfiguration<CustomerCart>
    {
        public void Configure(EntityTypeBuilder<CustomerCart> builder)
        {
            builder.HasKey(e => new { e.CustomerId, e.CartId });

            builder.HasIndex(e => e.CartId);

            builder.HasOne(d => d.Cart)
                .WithMany(p => p.CustomerCart)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_CustomerCart_Cart");

            builder.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCart)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCart_AspNetUsers");
        }
    }
}
