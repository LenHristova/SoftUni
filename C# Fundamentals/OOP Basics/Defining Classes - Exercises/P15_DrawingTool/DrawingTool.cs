using System;
using System.Text;

public class DrawingTool
{
    public void Draw(int sideA, int sideB)
    {
        for (int row = 0; row < sideB; row++)
        {
            if (row == 0 || row == sideB - 1)
            {
                Console.WriteLine($"|{DrawRow("-", sideA)}|");
            }
            else
            {
                Console.WriteLine($"|{DrawRow(" ", sideA)}|");
            }
        }
    }

    private string DrawRow(string symbols, int sideA)
    {
        var sb = new StringBuilder();
        for (int col = 0; col < sideA; col++)
        {
            sb.Append(symbols);
        }

        return sb.ToString();
    }
}