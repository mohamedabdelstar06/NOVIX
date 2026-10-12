using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOVIX.Catalog.Domain.Entities;

namespace NOVIX.Catalog.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(e => e.ParentCategory)
                   .WithMany(e => e.SubCategories)
                   .HasForeignKey(e => e.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
