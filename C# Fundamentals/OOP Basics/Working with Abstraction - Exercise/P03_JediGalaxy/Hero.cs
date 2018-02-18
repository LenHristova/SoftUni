using System;
using System.Linq;

public class Hero
{
    public int RowPosition { get; set; }
    public int ColPosition { get; set; }

    public Hero(string input)
    {
        int[] evil = input
            .Split(new [] { " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        RowPosition = evil[0];
        ColPosition = evil[1];
    }

    public void Move()
    {
        RowPosition--;
    }
}