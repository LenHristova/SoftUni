using System;
using System.Linq;

public class Engine
{
    private readonly ICalculator _calculator;
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public Engine(ICalculator calculator, IReader reader, IWriter writer)
    {
        _calculator = calculator;
        _reader = reader;
        _writer = writer;
    }
    //public Engine(IServiceProvider serviceProvider)
    //{
    //    _calculator = (ICalculator)serviceProvider.GetService(typeof(ICalculator));
    //    _reader = (IReader)serviceProvider.GetService(typeof(IReader));
    //    _writer = (IWriter)serviceProvider.GetService(typeof(IWriter));
    //}

    public void Run()
    {
        string input;
        while ((input = _reader.ReadLine()) != "End")
        {
            var commandArgs = input?.Split();
            if (commandArgs?[0] == "mode")
            {
                _calculator.ChangeStrategy(commandArgs[1].First());
            }
            else
            {
                var firstOperand = int.Parse(commandArgs?[0]);
                var secondOperand = int.Parse(commandArgs?[1]);
                var result = _calculator.PerformCalculation(firstOperand, secondOperand);
                _writer.WriteLine(result);
            }
        }
    }
}