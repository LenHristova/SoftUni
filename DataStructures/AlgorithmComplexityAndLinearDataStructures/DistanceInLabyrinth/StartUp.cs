using System;
using System.Collections.Generic;

class StartUp
{
    //private static string[,] labyrinth =
    //{
    //    { "0", "0", "0", "x", "0", "x" },
    //    { "0", "x", "0", "x", "0", "x" },
    //    { "0", "*", "x", "0", "x", "0" },
    //    { "0", "x", "0", "0", "0", "0" },
    //    { "0", "0", "0", "x", "x", "0" },
    //    { "0", "0", "0", "x", "0", "x" }
    //};

    private static string[,] labyrinth;

    private static readonly Queue<Cell> VisitedCells = new Queue<Cell>();

    static void Main()
    {
        labyrinth = GetLabyrinth();
        var startingPosition = GetStartingPosition();

        VisitedCells.Enqueue(startingPosition);

        //BFS
        while (VisitedCells.Count > 0)
        {
            var currentCell = VisitedCells.Dequeue();

            var col = currentCell.Col;
            var row = currentCell.Row;
            var value = currentCell.Value + 1;

            //Check available steps and mark them with their sequence
            if (HasStepTo(row + 1, col))
            {
                VisitedCells.Enqueue(new Cell(row + 1, col, value));
                labyrinth[row + 1, col] = value.ToString();
            }
            if (HasStepTo(row - 1, col))
            {
                VisitedCells.Enqueue(new Cell(row - 1, col, value));
                labyrinth[row - 1, col] = value.ToString();
            }
            if (HasStepTo(row, col + 1))
            {
                VisitedCells.Enqueue(new Cell(row, col + 1, value));
                labyrinth[row, col + 1] = value.ToString();
            }
            if (HasStepTo(row, col - 1))
            {
                VisitedCells.Enqueue(new Cell(row, col - 1, value));
                labyrinth[row, col - 1] = value.ToString();
            }
        }

        PrintLabyrinth();
    }

    //Get matrix from the console
    private static string[,] GetLabyrinth()
    {
        
        var dimension = int.Parse(Console.ReadLine());
        var lab = new string[dimension, dimension];
        for (int i = 0; i < dimension; i++)
        {
            var signs = Console.ReadLine();
            for (int j = 0; j < dimension; j++)
            {
                lab[i, j] = signs[j].ToString();
            }
        }

        return lab;
    }

    //Print result matrix
    private static void PrintLabyrinth()
    {
        for (int i = 0; i < labyrinth.GetLongLength(0); i++)
        {
            for (int j = 0; j < labyrinth.GetLongLength(1); j++)
            {
                Console.Write(labyrinth[i, j] == "0" ? "u" : labyrinth[i, j]);
            }

            Console.WriteLine();
        }
    }

    //Check if cells next to the current is possible step,
    //check if they are in the matrix and if their value is "0" (possible step)
    private static bool HasStepTo(int row, int col)
    {
        return row >= 0 &&
               row < labyrinth.GetLongLength(0) &&
               col >= 0 &&
               col < labyrinth.GetLongLength(1) &&
               labyrinth[row, col] == "0";
    }

    //Traverse matrix for starting possition, marked with "*"
    private static Cell GetStartingPosition()
    {
        for (int i = 0; i < labyrinth.GetLongLength(0); i++)
        {
            for (int j = 0; j < labyrinth.GetLongLength(1); j++)
            {
                if (labyrinth[i, j] == "*")
                {
                    return new Cell(i, j, 0);
                }
            }
        }

        throw new AccessViolationException("Missing starting point!");
    }
}