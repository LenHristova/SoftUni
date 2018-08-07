namespace ProductShop.App
{
    using System.IO;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;

    public class Exporter
    {
        private readonly ProductShopContext context;

        public Exporter(ProductShopContext context)
        {
            this.context = context;
        }

        public void ExportUsersAndProducts()
        {
            var users = new
            {
                usersCount = this.context.Users.Count(),
                users = this.context.Users
                    .Where(u => u.ProductsSold.Any() && u.ProductsSold.Any(p => p.Buyer != null))
                    .OrderByDescending(us => us.ProductsSold.Count)
                    .ThenBy(us => us.LastName)
                    .Select(x => new
                    {
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        age = x.Age,
                        soldProducts = new
                        {
                            count = x.ProductsSold.Count,
                            products = x.ProductsSold
                                .Select(p => new
                                {
                                    name = p.Name,
                                    pice = p.Price

                                })
                                .ToArray()
                        }
                    })
                    .ToArray()
            };

        var serialized = JsonConvert.SerializeObject(users, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        });
        var path = "Json/users-and-products.json";
        File.WriteAllText(path, serialized);
        }

    public void ExportCategoriesByProductsCount()
    {
        var users = this.context.Categories
            .Select(c => new
            {
                category = c.Name,
                productsCount = c.CategoryProducts.Count,
                averagePrice = c.CategoryProducts
                                .Sum(cp => cp.Product.Price) / c.CategoryProducts.Count,
                totalRevenue = c.CategoryProducts
                                .Sum(cp => cp.Product.Price)
            })
            .OrderByDescending(c => c.productsCount)
            .ToArray();

        var serialized = JsonConvert.SerializeObject(users, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        });
        var path = "Json/categories-by-products.json";
        File.WriteAllText(path, serialized);
    }

    public void ExportSoldProducts()
    {
        var users = this.context.Users
            .Where(u => u.ProductsSold.Any() && u.ProductsSold.Any(p => p.Buyer != null))
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                soldProducts = u.ProductsSold
                    .Where(p => p.Buyer != null)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    })
                    .ToArray()
            })
            .ToArray();

        var serialized = JsonConvert.SerializeObject(users, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        });
        var path = "Json/users-sold-products.json";
        File.WriteAllText(path, serialized);
    }

    public void ExportProductsInRange(decimal minPrice, decimal maxPrice)
    {
        var products = this.context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .Select(x => new
            {
                name = x.Name,
                price = x.Price,
                seller = $"{x.Seller.FirstName} {x.Seller.LastName}".Trim()
            })
            .ToArray();

        var serialized = JsonConvert.SerializeObject(products, Formatting.Indented);
        var path = "Json/products-in-range.json";
        File.WriteAllText(path, serialized);
    }
}
}
