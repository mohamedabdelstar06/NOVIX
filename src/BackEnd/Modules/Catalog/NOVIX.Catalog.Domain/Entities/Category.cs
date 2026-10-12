using System;
using System.Collections.Generic;
using NOVIX.Shared.Core.Domain;

namespace NOVIX.Catalog.Domain.Entities
{
    public class Category : AggregateRoot<Guid>
    {
        public Category()
        {
            Id = Guid.NewGuid();
            SubCategories = new List<Category>();
            Products = new List<Product>();
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        
        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
