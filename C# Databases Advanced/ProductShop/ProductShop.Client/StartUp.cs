namespace ProductShop.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Dtos;
    using Microsoft.EntityFrameworkCore;
    using DataAnotations = System.ComponentModel.DataAnnotations;
    using Models;

    public class StartUp
    {
        public static void Main()
        {
            InitializeDatabase();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();

            AddUsers(mapper);
            AddProducts(mapper);
            AddCategories(mapper);
            AddCategoryProducts();

            ProductsInRange(mapper, 1000, 2000, hasBuyer: true);
            UserWithSoldProducts(mapper);
            CategoriesByProductsCount(mapper);
            UserAndProducts();
        }

        private static void UserAndProducts()
        {
            UserWithProductsDto[] userWithSoldItemsDtos;
            using (var context = new ProductShopContext())
            {
                userWithSoldItemsDtos = context.Users
                    .Where(u => u.SoldProducts.Any())
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new UserWithProductsDto
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age.HasValue ? u.Age.Value.ToString() : null,
                        SoldProducts = new ProductsCollectionDto()
                        {
                            Count = u.SoldProducts.Count,
                            Products = u.SoldProducts
                                .Select(sp => new ProductDto
                                {
                                    Name = sp.Name,
                                    Price = sp.Price
                                })
                                .ToArray()
                        }
                    })
                    .ToArray();
            }

            var users = new UsersCollectionDto
            {
                Count = userWithSoldItemsDtos.Length,
                UserWithProducts = userWithSoldItemsDtos
            };

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var overrides = new XmlAttributeOverrides();
            AddXmlAttr(overrides, typeof(ProductDto), nameof(ProductDto.Name), "name");
            AddXmlAttr(overrides, typeof(ProductDto), nameof(ProductDto.Price), "price");

            var serializer = new XmlSerializer(typeof(UsersCollectionDto), overrides);
            serializer.Serialize(new StringWriter(sb), users, namespaces);
            const string path = "../../../Xml/users-and-products.xml";
            File.WriteAllText(path, sb.ToString());
        }

        private static void AddXmlAttr(XmlAttributeOverrides xmlAttributeOverrides, Type type, string property, string attrName)
        => xmlAttributeOverrides.Add(type, property, new XmlAttributes { XmlAttribute = new XmlAttributeAttribute(attrName) });
        
        private static void CategoriesByProductsCount(IMapper mapper)
        {
            CategoryProductsDto[] categoriesDtos;
            using (var context = new ProductShopContext())
            {
                categoriesDtos = context.Categories
                    .OrderByDescending(c => c.Products.Count)
                    .ProjectTo<CategoryProductsDto>(mapper.ConfigurationProvider)
                    .ToArray();
            }

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(CategoryProductsDto[]), new XmlRootAttribute("categories"));
            serializer.Serialize(new StringWriter(sb), categoriesDtos, namespaces);
            const string path = "../../../Xml/categories-by-products.xml";
            File.WriteAllText(path, sb.ToString());
        }

        private static void UserWithSoldProducts(IMapper mapper)
        {
            UserWithSoldItemsDto[] userWithSoldItemsDtos;
            using (var context = new ProductShopContext())
            {
                userWithSoldItemsDtos = context.Users
                    .Where(u => u.SoldProducts.Any())
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .ProjectTo<UserWithSoldItemsDto>(mapper.ConfigurationProvider)
                    .ToArray();
            }

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(UserWithSoldItemsDto[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), userWithSoldItemsDtos, namespaces);
            const string path = "../../../Xml/users-sold-products.xml";
            File.WriteAllText(path, sb.ToString());
        }

        private static void ProductsInRange(IMapper mapper, decimal minPrice, decimal maxPrice, bool hasBuyer)
        {
            ProductWithBuyerDto[] products;
            using (var context = new ProductShopContext())
            {
                products = context.Products
                   .Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.BuyerId.HasValue == hasBuyer)
                   .OrderBy(p => p.Price)
                   .ProjectTo<ProductWithBuyerDto>(mapper.ConfigurationProvider)
                   .ToArray();
            }

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ProductWithBuyerDto[]), new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(sb), products, namespaces);
            const string path = "../../../Xml/products-in-range.xml";
            File.WriteAllText(path, sb.ToString());
        }

        private static void AddCategoryProducts()
        {
            var categoriesProducts = new List<CategoryProduct>();

            var rnd = new Random();

            for (int productId = 1; productId <= 200; productId++)
            {
                var categoryProduct = new CategoryProduct
                {
                    CategoryId = rnd.Next(1, 12),
                    ProductId = productId
                };

                categoriesProducts.Add(categoryProduct);
            }

            var context = new ProductShopContext();
            context.CategoriesProducts.AddRange(categoriesProducts);
            context.SaveChanges();
        }

        private static void AddCategories(IMapper mapper)
        {
            const string path = "../../../Xml/categories.xml";
            var xmlString = File.ReadAllText(path);

            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var categoryDtos = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));

            var categories = new List<Category>();
            foreach (var categoryDto in categoryDtos)
            {
                if (IsValid(categoryDto))
                {
                    var category = mapper.Map<Category>(categoryDto);
                    categories.Add(category);
                }
            }

            var context = new ProductShopContext();
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void AddProducts(IMapper mapper)
        {
            const string path = "../../../Xml/products.xml";
            var xmlString = File.ReadAllText(path);
            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var productDtos = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            var rnd = new Random();

            var products = new List<Product>();
            foreach (var productDto in productDtos)
            {
                if (IsValid(productDto))
                {
                    var product = mapper.Map<Product>(productDto);

                    var sellerId = rnd.Next(1, 56);
                    var buyerId = rnd.Next(1, 100);

                    product.SellerId = sellerId;

                    if (buyerId <= 56 && buyerId != sellerId)
                    {
                        product.BuyerId = buyerId;
                    }

                    products.Add(product);
                }
            }

            var context = new ProductShopContext();
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void AddUsers(IMapper mapper)
        {
            const string path = "../../../Xml/users.xml";
            var xmlString = File.ReadAllText(path);

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var usersDtos = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            var users = new List<User>();
            foreach (var usersDto in usersDtos)
            {
                if (IsValid(usersDto))
                {
                    var user = mapper.Map<User>(usersDto);
                    users.Add(user);
                }
            }

            var context = new ProductShopContext();
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new DataAnotations.ValidationContext(obj);

            return DataAnotations.Validator.TryValidateObject(obj, validationContext,
                new List<DataAnotations.ValidationResult>(), true);
        }

        private static void InitializeDatabase()
        {
            var context = new ProductShopContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
