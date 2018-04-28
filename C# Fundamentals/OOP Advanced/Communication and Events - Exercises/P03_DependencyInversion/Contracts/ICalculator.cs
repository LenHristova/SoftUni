public interface ICalculator
{
    void ChangeStrategy(char @operator);

    int PerformCalculation(int firstOperand, int secondOperand);
}