using P01_Logger.Models.Errors.Contracts;

namespace P01_Logger.Models.Layouts.Contracts
{
    public interface ILayout
    {
        string FormatedMessage(IError error);
    }
}
