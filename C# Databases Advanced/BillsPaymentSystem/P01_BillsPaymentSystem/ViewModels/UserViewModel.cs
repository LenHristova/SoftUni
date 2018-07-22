namespace P01_BillsPaymentSystem.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data.Models;

    public class UserViewModel
    {
        public UserViewModel()
        {
            this.BankAccounts = new List<BankAccount>();
            this.CreditCards = new List<CreditCard>();
        }
        public string FullName { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; }

        public decimal AllBalance => this.BankAccounts.Sum(ba => ba.Balance) + this.CreditCards.Sum(cc => cc.LimitLeft);

        public string GetDetailedViewToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"User name: {this.FullName}")
                .AppendLine()
                .AppendLine($"All balance: {this.AllBalance:F2}");

            sb.Append("Bank Accounts: ");
            if (this.BankAccounts.Any())
            {
                sb.AppendLine();
                foreach (var bankAccount in this.BankAccounts)
                {
                    sb.AppendLine($"-- ID: {bankAccount.BankAccountId}")
                        .AppendLine($"--- Balance: {bankAccount.Balance:F2}")
                        .AppendLine($"--- Bank: {bankAccount.BankName}")
                        .AppendLine($"--- SWIFT: {bankAccount.SwiftCode}");
                }
            }
            else
            {
                sb.AppendLine("[NONE]");
            }

            sb.Append("Credit Cards:");
            if (this.CreditCards.Any())
            {
                sb.AppendLine();
                foreach (var creditCard in this.CreditCards)
                {
                    sb.AppendLine($"-- ID: {creditCard.CreditCardId}")
                        .AppendLine($"--- Limit: {creditCard.Limit:F2}")
                        .AppendLine($"--- Money Owed: {creditCard.MoneyOwed:F2}")
                        .AppendLine($"--- Limit Left: {creditCard.LimitLeft:F2}")
                        .AppendLine(
                            $"--- Expiration Date: {creditCard.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                }
            }
            else
            {
                sb.AppendLine("[NONE]");
            }

            return sb.ToString().Trim();
        }

        public string GetBaseViewToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"User name: {this.FullName}")
                .AppendLine($"All balance: {this.AllBalance:F2}");

            sb.Append("Bank Accounts: ");
            if (this.BankAccounts.Any())
            {
                sb.AppendLine();
                foreach (var bankAccount in this.BankAccounts)
                {
                    sb.AppendLine($"-- ID: {bankAccount.BankAccountId};   Balance: {bankAccount.Balance:F2}");
                }
            }
            else
            {
                sb.AppendLine("[NONE]");
            }

            sb.Append("Credit Cards:");
            if (this.CreditCards.Any())
            {
                sb.AppendLine();
                foreach (var creditCard in this.CreditCards)
                {
                    sb.AppendLine($"-- ID: {creditCard.CreditCardId};   Limit Left: {creditCard.LimitLeft:F2}");
                }
            }
            else
            {
                sb.AppendLine("[NONE]");
            }

            return sb.ToString().Trim();
        }
    }
}
