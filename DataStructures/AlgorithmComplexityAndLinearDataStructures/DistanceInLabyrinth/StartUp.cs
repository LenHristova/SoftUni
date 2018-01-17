using System;
using System.Collections.Generic;

class StartUp
{
    class Cell
    {
        public int Row { get; }
        public int Col { get; }
        public int Value { get; }

        public Cell(int row, int col, int value)
        {
            this.Row = row;
            this.Col = col;
            this.Value = value;
        }
    }

    //private static string[,] labyrinth =
    //{
    //    { "0", "0", "0", "x", "0", "x" },
    //    { "0", "x", "0", "x", "0", "x" },
    //    { "0", "*", "x", "0", "x", "0" },
    //    { "0", "x", "0", "0", "0", "0" },
    //    { "0", "0", "0", "x", "x", "0" },
    //    { "0", "0", "0", "x", "0", "x" }
    //};

    private static string[,] _labyrinth;
    private static Cell startingPosition;

    private static readonly Queue<Cell> VisitedCells = new Queue<Cell>();

    static void Main()
    {
        GetLabyrinthParams();

        VisitedCells.Enqueue(startingPosition);

        //BFS
        while (VisitedCells.Count > 0)
        {
            var currentCell = VisitedCells.Dequeue();

            var col = currentCell.Col;
            var row = currentCell.Row;
            var value = currentCell.Value + 1;

            //Check available steps and mark them with their sequence
            if (row + 1 < _labyrinth.GetLongLength(0) && _labyrinth[row + 1, col] == "0")
            {
                VisitedCells.Enqueue(new Cell(row + 1, col, value));
                _labyrinth[row + 1, col] = value.ToString();
            }
            if (row - 1 >= 0 && _labyrinth[row - 1, col] == "0")
            {
                VisitedCells.Enqueue(new Cell(row - 1, col, value));
                _labyrinth[row - 1, col] = value.ToString();
            }
            if (col + 1 < _labyrinth.GetLongLength(1) && _labyrinth[row, col + 1] == "0")
            {
                VisitedCells.Enqueue(new Cell(row, col + 1, value));
                _labyrinth[row, col + 1] = value.ToString();
            }
            if (col - 1 >= 0 && _labyrinth[row, col - 1] == "0")
            {
                VisitedCells.Enqueue(new Cell(row, col - 1, value));
                _labyrinth[row, col - 1] = value.ToString();
            }
        }

        PrintLabyrinth();
    }

    //Get matrix from the console and find starting position
    private static void GetLabyrinthParams()
    {
        var size = int.Parse(Console.ReadLine());
        _labyrinth = new string[size, size];
        for (int i = 0; i < size; i++)
        {
            var signs = Console.ReadLine();
            for (int j = 0; j < size; j++)
            {
                _labyrinth[i, j] = signs[j].ToString();
                if (_labyrinth[i, j] == "*")
                {
                    startingPosition = new Cell(i, j, 0);
                }
            }
        }

        if (startingPosition == null)
        {
            throw new AccessViolationException("Missing starting point!");
        }
    }

    //Print result matrix
    private static void PrintLabyrinth()
    {
        for (int i = 0; i < _labyrinth.GetLongLength(0); i++)
        {
            for (int j = 0; j < _labyrinth.GetLongLength(1); j++)
            {
                Console.Write(_labyrinth[i, j] == "0" ? "u" : _labyrinth[i, j]);
            }

            Console.WriteLine();
        }
    }
}