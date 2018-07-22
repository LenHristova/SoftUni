using Microsoft.EntityFrameworkCore;

namespace P01_BillsPaymentSystem.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.Models.Enums;

    public class DatabaseInitializer
    {
        public static void ResetDatabase()
        {
            using (var context = new BillsPaymentSystemContext())
            {
                context.Database.EnsureDeleted();

                context.Database.Migrate();

                Seed(context);
            }
        }

        private static void Seed(BillsPaymentSystemContext context)
        {
            var users = GenerateUsers();
            AddValid(context, users);

            var bankAccounts = GenerateBankAccounts();
            AddValid(context, bankAccounts);

            var creditCards = GenerateCreditCards();
            AddValid(context, creditCards);

            var paymentMethods = GeneratePaymentMethods(users, bankAccounts, creditCards);
            AddValid(context, paymentMethods);
        }

        private static PaymentMethod[] GeneratePaymentMethods(IList<User> users, IReadOnlyList<BankAccount> bankAccounts, IReadOnlyList<CreditCard> creditCards)
        {
            return new PaymentMethod[]
            {
                new PaymentMethod{User = users[4], Type = PaymentMethodType.BankAccount, BankAccount = bankAccounts[0], CreditCard = null},
                new PaymentMethod{User = users[3], Type = PaymentMethodType.BankAccount, BankAccount = bankAccounts[2], CreditCard = null},
                new PaymentMethod{User = users[0], Type = PaymentMethodType.CreditCard,  BankAccount = null, CreditCard = creditCards[0]},
                new PaymentMethod{User = users[0], Type = PaymentMethodType.CreditCard,  BankAccount = null, CreditCard = creditCards[3]},
                new PaymentMethod{User = users[2], Type = PaymentMethodType.CreditCard,  BankAccount = null, CreditCard = creditCards[1]},
                new PaymentMethod{User = users[1], Type = PaymentMethodType.BankAccount, BankAccount = bankAccounts[1], CreditCard = null},
                new PaymentMethod{User = users[8], Type = PaymentMethodType.BankAccount, BankAccount = bankAccounts[3], CreditCard = null},
                new PaymentMethod{User = users[3], Type = PaymentMethodType.CreditCard,  BankAccount = null, CreditCard = creditCards[2]},
                new PaymentMethod{User = users[2], Type = PaymentMethodType.BankAccount, BankAccount = bankAccounts[4], CreditCard = null},
                //Invalid data --> 
                new PaymentMethod{User = users[5], Type = PaymentMethodType.BankAccount, BankAccount = null, CreditCard = creditCards[4]},
                new PaymentMethod{User = users[5], Type = PaymentMethodType.CreditCard,  BankAccount = bankAccounts[3], CreditCard = null},
                new PaymentMethod{User = users[2], Type = PaymentMethodType.BankAccount, BankAccount = bankAccounts[4], CreditCard = creditCards[2]},
                new PaymentMethod{User = users[2], Type = PaymentMethodType.BankAccount, BankAccount = null, CreditCard = null},
            };
        }

        private static CreditCard[] GenerateCreditCards()
        {
            var creditCards = new CreditCard[]
            {
                new CreditCard { ExpirationDate = new DateTime(2019, 12, 31)},
                new CreditCard {ExpirationDate = new DateTime(2019, 5, 31)},
                new CreditCard { ExpirationDate = new DateTime(2025, 12, 31)},
                new CreditCard { ExpirationDate = new DateTime(2020, 6, 30)},
                new CreditCard {ExpirationDate = new DateTime(2019, 1, 31)},
                new CreditCard {ExpirationDate = new DateTime(2018, 7, 14)},
            };

            var limits = new[] { 1000, 300, 500000, 3000, 1000, 100 };
            var moneyOweds = new[] { 200, 300, 1000, 500, 953, 0 };
            for (int i = 0; i < creditCards.Length; i++)
            {
                creditCards[i].ChangeLimit(limits[i]);
                creditCards[i].Withdraw(moneyOweds[i]);
            }

            return creditCards;
        }

        private static BankAccount[] GenerateBankAccounts()
        {
            var bankAccounts = new BankAccount[]
            {
                new BankAccount{ BankName = "DSK", SwiftCode = "STSABGSF"},
                new BankAccount{ BankName = "DSK", SwiftCode = "STSABGSF"},
                new BankAccount{ BankName = "UBB", SwiftCode = "UBBSBGSF"},
                new BankAccount{ BankName = "UBB", SwiftCode = "UBBSBGSF"},
                new BankAccount{ BankName = "UBB", SwiftCode = "UBBSBGSF"},
            };

            var deposits = new[] { 5000, 500, 253.2M, 3000, 0 };
            for (int i = 0; i < bankAccounts.Length; i++)
            {
                bankAccounts[i].Deposit(deposits[i]);
            }

            return bankAccounts;
        }

        private static IList<User> GenerateUsers()
        {
            var names = new[]
            {
                new[] {"Len", "Hristova"},
                new[] {"Jes", "Burr"},
                new[] {"Ivan", "Petrov"},
                new[] {"Maria", "Ivanova"},
                new[] {"Kosta", "Hristov"},
                new[] {"Megi", "Hristova"},
                new[] {"Kalin", "Tonev"},
                new[] {"Boris", "Borisov"},
                new[] {"Lora", "Vankova"},
                new[] {"Len", "Hr"},
            };

            var rnd = new Random();
            var users = names
                .Select(name => new User
                {
                    FirstName = name[0],
                    LastName = name[1],
                    Email = $"{name[0].ToLower()}_{name[1].ToLower()}@gmail.com",
                    Password = rnd.Next(int.MaxValue).ToString()
                })
                .ToList();
            return users;
        }

        private static void AddValid<TEntity>(BillsPaymentSystemContext context, IEnumerable<TEntity> entities)
            where TEntity
            : class
        {
            foreach (var entity in entities)
            {
                if (IsValid(entity))
                {
                    context.Add(entity);
                }
            }

            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
        }
    }
}
