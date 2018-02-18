namespace P06_Sneaking
{
    class Enemy:Hero
    {
        public string Direction { get; set; }
        public Enemy(int rowPosition, int colPosition) : base(rowPosition, colPosition)
        {
        }

        public Enemy(int rowPosition, int colPosition, string direction) : this(rowPosition, colPosition)
        {
            Direction = direction;
        }

        public override void Move(string direction)
        {
            switch (direction)
            {
                case "right":
                    if (IsInRoom(RowPosition, ColPosition + 1))
                    {
                        Room.Matrix[RowPosition][ColPosition] = '.';
                        Room.Matrix[RowPosition][ColPosition + 1] = 'b';
                        ColPosition++;
                    }
                    else
                    {
                        Room.Matrix[RowPosition][ColPosition] = 'd';
                        Direction = "left";
                    }
                    break;
                case "left":
                    if (IsInRoom(RowPosition, ColPosition - 1))
                    {
                        Room.Matrix[RowPosition][ColPosition] = '.';
                        Room.Matrix[RowPosition][ColPosition - 1] = 'd';
                        ColPosition--;
                    }
                    else
                    {
                        Room.Matrix[RowPosition][ColPosition] = 'b';
                        Direction = "right";
                    }
                    break;
            }
        }

        private static bool IsInRoom(int row, int col)
        {
            return row >= 0 && row < Room.Matrix.Length &&
                   col >= 0 && col < Room.Matrix[row].Length;
        }
    }
}
