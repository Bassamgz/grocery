namespace Grocery.Core.Data.Configuration
{
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }
}
