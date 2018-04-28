public interface ICommandInterpreter
{
    IExecutable InterpretCommand(string commandType, string[] data);
}