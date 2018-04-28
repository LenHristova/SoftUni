public class PrimitiveCalculator : ICalculator
{
    private const char DEFAULT_STRATEGY_OPERATOR = '+';

    private IStrategy _strategy;
    private readonly IStrategyFactory _strategyFactory;

    public PrimitiveCalculator(IStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
        ChangeStrategy(DEFAULT_STRATEGY_OPERATOR);
    }

    public void ChangeStrategy(char @operator)
    {
        _strategy = _strategyFactory.CreateStrategy(@operator);
    }

    public int PerformCalculation(int firstOperand, int secondOperand)
    {
        return _strategy.Calculate(firstOperand, secondOperand);
    }
}