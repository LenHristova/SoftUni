namespace P01_BillsPaymentSystem.Models
{
    using System;
    using Contracts.Models;

    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}