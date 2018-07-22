namespace P01_BillsPaymentSystem.Contracts.Services
{
    using System;
    using Data.Models.Enums;
    using ViewModels;

    public interface IUserService
    {
		bool TrySignUpUser(string firstName, string lastName, string email, string password);

		bool TryLogInUser(string email, string password);

        bool TryToPayBills(decimal amount);

        UserViewModel GetUserViewModel();

        void Reset();

        int AddNewCreditCard(DateTime expirationDate);

        int AddNewBankAccount(string bankName, string swift);

        bool TryToDeposit(PaymentMethodType type, int id, decimal amount);

        bool TryToWithdraw(PaymentMethodType type, int id, decimal amount);
    }
}
