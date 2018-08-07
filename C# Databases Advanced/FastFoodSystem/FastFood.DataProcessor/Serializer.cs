using System;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Dto.Export;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var employee = context.Employees
                .Where(e => string.Equals(e.Name, employeeName, StringComparison.CurrentCultureIgnoreCase))
                .Select(e => new
                {
                    Name = e.Name,
                    Orders = e.Orders
                        .Where(o => string.Equals(o.Type.ToString(), orderType,
                            StringComparison.CurrentCultureIgnoreCase))
                        .OrderByDescending(o => o.TotalPrice)
                        .ThenByDescending(o => o.OrderItems.Count)
                        .Select(o => new
                        {
                            Customer = o.Customer,
                            Items = o.OrderItems
                                .Select(i => new
                                {
                                    Name = i.Item.Name,
                                    Price = i.Item.Price,
                                    Quantity = i.Quantity
                                })
                                .ToArray(),
                            TotalPrice = o.TotalPrice
                        })
                        .ToArray(),
                    TotalMade = e.Orders
                        .Where(o => string.Equals(o.Type.ToString(), orderType,
                            StringComparison.CurrentCultureIgnoreCase))
                        .Sum(o => o.TotalPrice)
                })
                .FirstOrDefault();

            var serialized = JsonConvert.SerializeObject(employee, Formatting.Indented);

            return serialized;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categoriesNames = categoriesString.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var categories = context.Categories
                .Where(c => categoriesNames.Contains(c.Name))
                .Select(c => new CategoryStatisticsDto()
                {
                    Name = c.Name,
                    MostPopularItem = c.Items.Select(i => new MostPopularItemDto
                    {
                        Name = i.Name,
                        TotalMade = i.OrderItems.Sum(x => x.Item.Price * x.Quantity),
                        TimesSold = i.OrderItems.Sum(x => x.Quantity)
                    })
                        .OrderByDescending(x => x.TotalMade)
                        .ThenByDescending(x => x.TimesSold)
                        .FirstOrDefault()
                })
                .OrderByDescending(c => c.MostPopularItem.TotalMade)
                .ThenByDescending(c => c.MostPopularItem.TimesSold)
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CategoryStatisticsDto[]), new XmlRootAttribute("Categories"));
            serializer.Serialize(new StringWriter(sb), categories, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString().Trim();
        }
    }
}