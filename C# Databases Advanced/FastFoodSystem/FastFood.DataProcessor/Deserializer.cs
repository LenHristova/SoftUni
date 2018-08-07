namespace FastFood.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Dto.Import;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var deserialized = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var sb = new StringBuilder();
            var positions = new List<Position>();
            var employees = new List<Employee>();
            foreach (var dto in deserialized)
            {
                if (IsValid(dto))
                {
                    var position = positions.FirstOrDefault(p => p.Name == dto.Position);
                    if (position == null)
                    {
                        position = new Position
                        {
                            Name = dto.Position
                        };

                        positions.Add(position);
                    }

                    var employee = new Employee
                    {
                        Name = dto.Name,
                        Age = dto.Age,
                        Position = position
                    };

                    employees.Add(employee);
                    sb.AppendLine(string.Format(SuccessMessage, dto.Name));
                }
                else
                {
                    sb.AppendLine(FailureMessage);
                }
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var deserialized = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var sb = new StringBuilder();
            var categories = new List<Category>();
            var items = new List<Item>();
            foreach (var dto in deserialized)
            {
                var item = items.FirstOrDefault(p => p.Name == dto.Name);

                if (IsValid(dto) && item == null)
                {
                    var category = categories.FirstOrDefault(p => p.Name == dto.Category)
                                   ?? new Category { Name = dto.Category };

                    item = new Item
                    {
                        Name = dto.Name,
                        Price = dto.Price,
                        Category = category
                    };

                    items.Add(item);
                    categories.Add(category);
                    sb.AppendLine(string.Format(SuccessMessage, dto.Name));
                }
                else
                {
                    sb.AppendLine(FailureMessage);
                }
            }

            context.Items.AddRange(items);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var deserialised = (OrderDto[])serializer.Deserialize(new StringReader(xmlString));

            var employees = context.Employees.ToArray();
            var items = context.Items.ToArray();

            var sb = new StringBuilder();

            var allOrderItems = new List<OrderItem>();
            foreach (var dto in deserialised)
            {
                if (IsValid(dto))
                {
                    var dateIsValid = DateTime.TryParseExact(dto.DateTime, "dd/MM/yyyy HH:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

                    if (!dateIsValid)
                    {
                        continue;
                    }

                    var employee = employees.FirstOrDefault(e => e.Name == dto.Employee);
                    if (employee == null)
                    {
                        continue;
                    }

                    if (!dto.Items.All(i => items.Any(x => x.Name == i.Name)))
                    {
                        continue;
                    }

                    var order = new Order
                    {
                        Customer = dto.Customer,
                        DateTime = date,
                        Employee = employee,
                        Type = Enum.Parse<OrderType>(dto.Type)
                    };

                    var orderItems = new List<OrderItem>();
                    foreach (var dtoOrderItem in dto.Items)
                    {
                        var orderItem = new OrderItem
                        {
                            Order = order,
                            Item = items.FirstOrDefault(i => i.Name == dtoOrderItem.Name),
                            Quantity = dtoOrderItem.Quantity
                        };

                        orderItems.Add(orderItem);
                    }

                    allOrderItems.AddRange(orderItems);
                    sb.AppendLine($"Order for {dto.Customer} on {dto.DateTime} added");
                }
            }

            context.OrderItems.AddRange(allOrderItems);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static bool IsValid(object obj)
            => Validator.TryValidateObject(obj, new ValidationContext(obj), new List<ValidationResult>(), true);
    }
}