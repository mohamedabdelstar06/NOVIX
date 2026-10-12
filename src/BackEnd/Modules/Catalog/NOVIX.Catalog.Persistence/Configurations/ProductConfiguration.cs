using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOVIX.Catalog.Domain.Entities;

namespace NOVIX.Catalog.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Slug).IsRequired().HasMaxLength(200);
            builder.HasIndex(e => e.Slug).IsUnique();
            builder.Property(e => e.BasePrice).HasColumnType("decimal(18,2)");
            
            builder.HasOne(e => e.Category)
                   .WithMany(e => e.Products)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
                  
            builder.HasOne(e => e.Brand)
                   .WithMany(e => e.Products)
                   .HasForeignKey(e => e.BrandId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
