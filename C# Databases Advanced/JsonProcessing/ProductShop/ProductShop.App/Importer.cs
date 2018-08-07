namespace ProductShop.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Data;
    using Models;
    using Newtonsoft.Json;

    public class Importer
    {
        private readonly ProductShopContext context;

        public Importer(ProductShopContext context)
        {
            this.context = context;
        }

        public void ImportCategoryProducts()
        {
            var rnd = new Random();

            var categoriesIds = this.context.Categories.Select(c => c.Id).ToArray();

            var productsIds = this.context.Products.Select(c => c.Id).ToArray();

            var categoryProducts = new List<CategoryProduct>();
            foreach (var productId in productsIds)
            {
                var productCategoriesIds = categoriesIds
                    .OrderBy(p => rnd.Next())
                    .Take(rnd.Next(1, categoriesIds.Length))
                    .ToArray();

                foreach (var categoriesId in productCategoriesIds)
                {
                    var categoryProduct = new CategoryProduct
                    {
                        ProductId = productId,
                        CategoryId = categoriesId
                    };

                    categoryProducts.Add(categoryProduct);
                }
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        public void ImportCategories()
        {
            var jsonString = File.ReadAllText("Json/categories.json");
            var deserialized = JsonConvert.DeserializeObject<Category[]>(jsonString);

            var categories = deserialized
                .Where(IsValid)
                .ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public void ImporProducts()
        {
            var jsonString = File.ReadAllText("Json/products.json");
            var deserialized = JsonConvert.DeserializeObject<Product[]>(jsonString);

            var rnd = new Random();
            var usersCount = this.context.Users.Count();
            var productWithBuyers = Enumerable.Range(0, deserialized.Length)
                .OrderBy(p => rnd.Next())
                .Take(rnd.Next(deserialized.Length / 2, deserialized.Length))
                .ToArray();

            var products = new List<Product>();
            for (int i = 0; i < deserialized.Length; i++)
            {
                if (IsValid(deserialized[i]))
                {
                    var sellerId = rnd.Next(1, usersCount + 1);
                    deserialized[i].SellerId = sellerId;

                    var buyerId = rnd.Next(1, usersCount + 1);

                    if (buyerId != sellerId && productWithBuyers.Contains(i))
                    {
                        deserialized[i].BuyerId = buyerId;
                    }

                    products.Add(deserialized[i]);
                }
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public void ImportUsers()
        {
            var jsonString = File.ReadAllText("Json/users.json");
            var deserialized = JsonConvert.DeserializeObject<User[]>(jsonString);

            var users = deserialized
                .Where(IsValid)
                .ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object obj)
            => Validator.TryValidateObject(
                obj,
                new System.ComponentModel.DataAnnotations.ValidationContext(obj),
                new List<ValidationResult>(),
                true);
    }
}
