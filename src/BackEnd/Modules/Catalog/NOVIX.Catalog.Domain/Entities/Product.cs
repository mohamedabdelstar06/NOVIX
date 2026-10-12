using System;
using NOVIX.Shared.Core.Domain;

namespace NOVIX.Catalog.Domain.Entities
{
    public class Product : AggregateRoot<Guid>
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public string ShortDescription { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
        public bool IsPublished { get; private set; } = false;
        public decimal BasePrice { get; private set; }

        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;

        public Guid BrandId { get; private set; }
        public Brand Brand { get; private set; } = null!;

        public static Product Create(string name, string slug, string shortDesc, string desc, string imageUrl, decimal basePrice, Guid categoryId, Guid brandId)
        {
            var product = new Product
            {
                Name = name,
                Slug = slug,
                ShortDescription = shortDesc,
                Description = desc,
                ImageUrl = imageUrl,
                BasePrice = basePrice,
                CategoryId = categoryId,
                BrandId = brandId,
                IsPublished = false
            };
            
            return product;
        }

        public void Publish()
        {
            IsPublished = true;
        }

        public void Unpublish()
        {
            IsPublished = false;
        }

        public void Update(string name, string slug, string shortDesc, string desc, string imageUrl, decimal basePrice, Guid categoryId, Guid brandId, bool isPublished)
        {
            Name = name;
            Slug = slug;
            ShortDescription = shortDesc;
            Description = desc;
            ImageUrl = imageUrl;
            BasePrice = basePrice;
            CategoryId = categoryId;
            BrandId = brandId;
            IsPublished = isPublished;
        }
    }
}
