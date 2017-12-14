namespace Grocery.Core.Data.Configuration
{
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CartItemsConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => new { e.CartId, e.ProductId });

            builder.HasIndex(e => e.ProductId);

            builder.Property(e => e.AddedOn).HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Cart)
                .WithMany(p => p.CartItem)
                .HasForeignKey(d => d.CartId);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.CartItem)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Product");
        }
    }
}
