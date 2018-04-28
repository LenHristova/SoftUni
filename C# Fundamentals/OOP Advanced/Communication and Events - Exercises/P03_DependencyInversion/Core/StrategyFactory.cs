using System;

public class StrategyFactory : IStrategyFactory
{
    public IStrategy CreateStrategy(char @operator)
    {
        switch (@operator)
        {
            case '+':
                return new AdditionStrategy();
            case '-':
                return new SubtractionStrategy();
            case '*':
                return new MultiplicationStrategy();
            case '/':
                return new DivisionStrategy();
            default:
                throw new NotSupportedException();
        }
    }
}