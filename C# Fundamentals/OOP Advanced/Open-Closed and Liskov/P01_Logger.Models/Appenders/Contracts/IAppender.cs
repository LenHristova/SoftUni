using P01_Logger.Models.Contracts;
using P01_Logger.Models.Errors.Contracts;
using P01_Logger.Models.Layouts.Contracts;

namespace P01_Logger.Models.Appenders.Contracts
{
    public interface IAppender : ILevelable
    {
        ILayout Layout { get; }

        int MessageCount { get; }

        void AppendLine(IError error);
    }
}
