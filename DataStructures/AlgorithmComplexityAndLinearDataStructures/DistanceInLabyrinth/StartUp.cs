using System;
using System.Collections.Generic;

class StartUp
{
    private static readonly int Size = int.Parse(Console.ReadLine());
    private static readonly string[,] Labyrinth = new string[Size, Size];
    private static Cell _startingPosition;
    private static readonly Queue<Cell> VisitedCells = new Queue<Cell>();

    static void Main()
    {
        GetLabyrinthParams();

        VisitedCells.Enqueue(_startingPosition);

        //BFS
        while (VisitedCells.Count > 0)
        {
            var currentCell = VisitedCells.Dequeue();

            FindAvailableSteps(currentCell);
        }

        PrintLabyrinth();
    }

    private static void FindAvailableSteps(Cell currentCell)
    {
        var row = currentCell.Row;
        var col = currentCell.Col;
        var value = currentCell.Value + 1;

        //Check available steps and mark them with their sequence
        if (row + 1 < Size && Labyrinth[row + 1, col] == "0")
        {
            VisitedCells.Enqueue(new Cell(row + 1, col, value));
            Labyrinth[row + 1, col] = value.ToString();
        }
        if (row - 1 >= 0 && Labyrinth[row - 1, col] == "0")
        {
            VisitedCells.Enqueue(new Cell(row - 1, col, value));
            Labyrinth[row - 1, col] = value.ToString();
        }
        if (col + 1 < Size && Labyrinth[row, col + 1] == "0")
        {
            VisitedCells.Enqueue(new Cell(row, col + 1, value));
            Labyrinth[row, col + 1] = value.ToString();
        }
        if (col - 1 >= 0 && Labyrinth[row, col - 1] == "0")
        {
            VisitedCells.Enqueue(new Cell(row, col - 1, value));
            Labyrinth[row, col - 1] = value.ToString();
        }
    }

    //Get matrix from the console and find starting position
    private static void GetLabyrinthParams()
    {
        for (int i = 0; i < Size; i++)
        {
            var signs = Console.ReadLine();
            for (int j = 0; j < Size; j++)
            {
                if (signs != null)
                {
                    Labyrinth[i, j] = signs[j].ToString();
                }
                if (Labyrinth[i, j] == "*")
                {
                    _startingPosition = new Cell(i, j, 0);
                }
            }
        }

        if (_startingPosition == null)
        {
            throw new AccessViolationException("Missing starting point!");
        }
    }

    //Print result matrix
    private static void PrintLabyrinth()
    {
        for (int i = 0; i < Labyrinth.GetLongLength(0); i++)
        {
            for (int j = 0; j < Labyrinth.GetLongLength(1); j++)
            {
                Console.Write(Labyrinth[i, j] == "0" ? "u" : Labyrinth[i, j]);
            }

            Console.WriteLine();
        }
    }
}