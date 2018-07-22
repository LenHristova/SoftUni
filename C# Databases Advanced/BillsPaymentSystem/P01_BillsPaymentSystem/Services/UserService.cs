namespace P01_BillsPaymentSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Contracts.Models;
    using Contracts.Services;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ViewModels;

    public class UserService : IUserService
    {
        private readonly BillsPaymentSystemContext context;
        private readonly ISession session;

        public UserService(BillsPaymentSystemContext context, ISession session)
        {
            this.context = context;
            this.session = session;
        }

        public bool TrySignUpUser(string firstName, string lastName, string email, string password)
        {
            var userExists = this.context.Users
                .Any(u => u.Email == email);

            if (userExists)
            {
                throw new InvalidOperationException("This email is already registered!");
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            this.AddValid(user);

            this.TryLogInUser(email, password);

            return true;
        }

        public bool TryLogInUser(string email, string password)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            this.session.Reset();
            this.session.LogIn(user.UserId, this.GetUserViewModel(user.UserId));
            return true;
        }

        public bool TryToPayBills(decimal amount)
        {
            if (this.session.UserViewModel.AllBalance < amount)
            {
                throw new ArgumentException("Insufficient funds!");
            }

            var bankAccounts = this.GetBankAccounts();

            foreach (var bankAccount in bankAccounts)
            {
                if (bankAccount.Balance >= amount)
                {
                    bankAccount.Withdraw(amount);
                    amount = 0;
                }
                else
                {
                    amount -= bankAccount.Balance;
                    bankAccount.Withdraw(bankAccount.Balance);
                }

                if (amount == 0)
                {
                    this.context.SaveChanges();
                    return true;
                }
            }

            var creaditCards = this.GetCreaditCards();

            foreach (var creditCard in creaditCards)
            {
                if (creditCard.LimitLeft >= amount)
                {
                    creditCard.Withdraw(amount);
                    amount = 0;
                }
                else
                {
                    amount -= creditCard.LimitLeft;
                    creditCard.Withdraw(creditCard.LimitLeft);
                }

                if (amount == 0)
                {
                    this.context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public UserViewModel GetUserViewModel() => this.GetUserViewModel(this.session.UserId);

        private UserViewModel GetUserViewModel(int userId) =>
            this.context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserViewModel
                {
                    FullName = $"{u.FirstName} {u.LastName}",
                    BankAccounts = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                        .Select(pm => pm.BankAccount)
                        .ToList(),
                    CreditCards = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                        .Select(pm => pm.CreditCard)
                        .ToList()
                })
                .FirstOrDefault();

        public int AddNewCreditCard(DateTime expirationDate)
        {
            var creditCard = new CreditCard
            {
                ExpirationDate = expirationDate
            };

            var paymentMethod = new PaymentMethod
            {
                UserId = this.session.UserId,
                Type = PaymentMethodType.CreditCard,
                CreditCard = creditCard
            };

            this.AddValid(paymentMethod);

            return creditCard.CreditCardId;
        }

        public int AddNewBankAccount(string bankName, string swift)
        {
            var bankAccount = new BankAccount
            {
                BankName = bankName,
                SwiftCode = swift
            };

            var paymentMethod = new PaymentMethod
            {
                UserId = this.session.UserId,
                Type = PaymentMethodType.BankAccount,
                BankAccount = bankAccount
            };

            this.AddValid(paymentMethod);

            return bankAccount.BankAccountId;
        }

        public bool TryToDeposit(PaymentMethodType type, int id, decimal amount)
        {
            var paymentMethod = EnsurePaymentMethod(type, id);

            var hasSuccess = false;
            switch (type)
            {
                case PaymentMethodType.BankAccount:
                    hasSuccess = paymentMethod.BankAccount.Deposit(amount);
                    break;
                case PaymentMethodType.CreditCard:
                    hasSuccess = paymentMethod.CreditCard.Deposit(amount);
                    break;
                default:
                    throw new ArgumentException($"{type} type not found.");
            }

            this.context.SaveChanges();
            return hasSuccess;
        }

        public bool TryToWithdraw(PaymentMethodType type, int id, decimal amount)
        {
            var paymentMethod = EnsurePaymentMethod(type, id);

            switch (type)
            {
                case PaymentMethodType.BankAccount:
                    if (paymentMethod.BankAccount.Balance < amount)
                    {
                        throw new ArgumentException("Insufficient funds!");
                    }

                    paymentMethod.BankAccount.Withdraw(amount);
                    break;
                case PaymentMethodType.CreditCard:
                    if (paymentMethod.CreditCard.LimitLeft < amount)
                    {
                        throw new ArgumentException("Insufficient funds!");
                    }

                    paymentMethod.CreditCard.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentException($"{type} type not found.");
            }

            this.context.SaveChanges();
            return true;
        }

        private PaymentMethod EnsurePaymentMethod(PaymentMethodType type, int id)
        {
            var paymentMethod = this.context.PaymentMethods
                .FirstOrDefault(pm => pm.UserId == this.session.UserId &&
                                      pm.Type == type &&
                                      (pm.BankAccountId == id || pm.CreditCardId == id));

            if (paymentMethod == null)
            {
                throw new ArgumentException($"There is no payment method of type \"{type.ToString()}\" with id {id}");
            }

            return paymentMethod;
        }

        private IEnumerable<BankAccount> GetBankAccounts() =>
            this.context.BankAccounts
                .Where(ba => ba.PaymentMethod.UserId == this.session.UserId)
                .OrderBy(ba => ba.BankAccountId)
                .ToList();

        private IEnumerable<CreditCard> GetCreaditCards() =>
            this.context.CreditCards
                .Where(cc => cc.PaymentMethod.UserId == this.session.UserId && cc.ExpirationDate >= DateTime.Today)
                .OrderBy(ba => ba.CreditCardId)
                .ToList();

        private void AddValid<TEntity>(TEntity entity)
            where TEntity : class
        {
            var validationContext = new ValidationContext(entity);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, validationContext, results, validateAllProperties: true);

            if (!isValid)
            {
                var errorString = string.Join(Environment.NewLine, results);
                throw new ArgumentException(errorString);
            }

            this.context.Add(entity);
            this.context.SaveChanges();
        }

        public void Reset()
        {
            this.session.Reset();
            this.session.RefreshUserViewModel(this.GetUserViewModel(this.session.UserId));
        }
    }
}
