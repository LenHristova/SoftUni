namespace P06_Sneaking
{
    abstract class Hero
    {
        public int RowPosition { get; set; }
        public int ColPosition { get; set; }

        protected Hero(int rowPosition, int colPosition)
        {
            RowPosition = rowPosition;
            ColPosition = colPosition;
        }

        public abstract void Move(string direction);
    }
}
