using System;

using P01_Logger.Models.Contracts;

namespace P01_Logger.Models.Errors.Contracts
{
    public interface IError : ILevelable
    {
        DateTime DateTime { get; }

        string Message { get; }
    }
}
