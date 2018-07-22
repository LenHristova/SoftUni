using P01_BillsPaymentSystem.ViewModels;

namespace P01_BillsPaymentSystem.Contracts.Models
{
    public interface ISession
    {
        UserViewModel UserViewModel { get; }

		int UserId { get; }

        void LogIn(int userId, UserViewModel userViewModel);

        void LogOut();

        void RefreshUserViewModel(UserViewModel userViewModel);

		IMenu Back();

		bool PushView(IMenu view);

		void Reset();
	}
}
