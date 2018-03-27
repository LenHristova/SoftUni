using System;
using System.Collections.Immutable;

public class CommandInterpreter
{
    private readonly ICustomList<string> customList;

    public CommandInterpreter()
    {
        this.customList = new CustomList<string>();
    }

    public string ParseCommand(string input)
    {
        string result = null;

        var commandArgs = input.Split();
        var command = commandArgs[0];

        switch (command)
        {
            case "Add":
                {
                    var element = commandArgs[1];
                    this.customList.Add(element);
                    break;
                }
            case "Remove":
                {
                    var index = int.Parse(commandArgs[1]);
                    this.customList.Remove(index);
                    break;
                }
            case "Contains":
                {
                    var element = commandArgs[1];
                    bool contains = this.customList.Contains(element);
                    result = contains.ToString();
                    break;
                }
            case "Swap":
                {
                    var index1 = int.Parse(commandArgs[1]);
                    var index2 = int.Parse(commandArgs[2]);
                    this.customList.Swap(index1, index2);
                    break;
                }
            case "Greater":
                {
                    var element = commandArgs[1];
                    result = this.customList.CountGreaterThan(element).ToString();
                    break;
                }
            case "Max":
                {
                    result = this.customList.Max();
                    break;
                }
            case "Min":
                {
                    result = this.customList.Min();
                    break;
                }
            case "Print":
                {
                    result = string.Join(Environment.NewLine, this.customList);
                    break;
                }
            case "Sort":
            {
                Sorter<string>.Sort(customList);
                //this.customList.Sort();
                break;
            }
        }

        return result;
    }
}