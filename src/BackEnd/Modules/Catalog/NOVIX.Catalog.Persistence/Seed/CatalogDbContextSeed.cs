using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Catalog.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Persistence.Seed
{
    public static class CatalogDbContextSeed
    {
        public static async Task SeedAsync(CatalogDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    CreateCategory("Electronics", "electronic devices and gadgets", null),
                    CreateCategory("Smartphones", "mobile phones", null),
                    CreateCategory("Home & Kitchen", "home appliances and kitchenware", null),
                    CreateCategory("Fashion", "clothes and apparel", null),
                    CreateCategory("Food", "groceries and food items", null)
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Brands.Any())
            {
                var brands = new List<Brand>
                {
                    CreateBrand("Apple", "Apple Inc.", "https://logo.com/apple.png"),
                    CreateBrand("Samsung", "Samsung Electronics", "https://logo.com/samsung.png"),
                    CreateBrand("Nike", "Nike Inc.", "https://logo.com/nike.png"),
                    CreateBrand("LG", "LG Electronics", "https://logo.com/lg.png"),
                    CreateBrand("Nestlé", "Nestlé S.A.", "https://logo.com/nestle.png")
                };

                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var electronics = context.Categories.First(c => c.Name == "Electronics");
                var smartphones = context.Categories.First(c => c.Name == "Smartphones");
                var home = context.Categories.First(c => c.Name == "Home & Kitchen");
                var fashion = context.Categories.First(c => c.Name == "Fashion");
                var food = context.Categories.First(c => c.Name == "Food");

                var apple = context.Brands.First(b => b.Name == "Apple");
                var samsung = context.Brands.First(b => b.Name == "Samsung");
                var nike = context.Brands.First(b => b.Name == "Nike");
                var lg = context.Brands.First(b => b.Name == "LG");
                var nestle = context.Brands.First(b => b.Name == "Nestlé");

                var products = new List<Product>
                {
                    CreateProduct("iPhone 15 Pro Max", "iphone-15-pro-max", "Latest Apple iPhone", "Full description of iPhone 15 Pro Max", 1199m, smartphones.Id, apple.Id),
                    CreateProduct("Samsung Galaxy S24 Ultra", "samsung-galaxy-s24-ultra", "Latest Samsung flagship", "Full description of S24 Ultra", 1299m, smartphones.Id, samsung.Id),
                    CreateProduct("MacBook Pro M3 Max", "macbook-pro-m3-max", "Apple Laptop", "Full description of MacBook Pro", 3499m, electronics.Id, apple.Id),
                    CreateProduct("Nike Air Max 270", "nike-air-max-270", "Running Shoes", "Full description of Nike Shoes", 150m, fashion.Id, nike.Id),
                    CreateProduct("LG OLED C3 65\" TV", "lg-oled-c3-65", "Smart TV", "Full description of LG TV", 1599m, home.Id, lg.Id),
                    CreateProduct("Nescafe Gold Blend", "nescafe-gold", "Instant Coffee", "Full description of coffee", 12m, food.Id, nestle.Id)
                };

                foreach (var product in products)
                {
                    product.Publish();
                }

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        private static Category CreateCategory(string name, string desc, Guid? parentId)
        {
            var category = new Category { Name = name, Description = desc, ParentCategoryId = parentId };
            return category;
        }

        private static Brand CreateBrand(string name, string desc, string logo)
        {
            var brand = new Brand { Name = name, Description = desc, LogoUrl = logo };
            return brand;
        }

        private static Product CreateProduct(string name, string slug, string shortDesc, string desc, decimal price, Guid catId, Guid brandId)
        {
            return Product.Create(name, slug, shortDesc, desc, "", price, catId, brandId);
        }
    }
}
