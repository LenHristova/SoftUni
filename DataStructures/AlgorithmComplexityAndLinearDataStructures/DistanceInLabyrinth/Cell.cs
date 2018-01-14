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