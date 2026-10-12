using System;
using System.Collections.Generic;
using NOVIX.Shared.Core.Domain;

namespace NOVIX.Catalog.Domain.Entities
{
    public class Brand : AggregateRoot<Guid>
    {
        public Brand()
        {
            Id = Guid.NewGuid();
            Products = new List<Product>();
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
