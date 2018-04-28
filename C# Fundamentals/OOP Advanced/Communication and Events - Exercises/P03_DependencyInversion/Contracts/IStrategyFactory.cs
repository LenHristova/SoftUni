public interface IStrategyFactory
{
    IStrategy CreateStrategy(char @operator);
}