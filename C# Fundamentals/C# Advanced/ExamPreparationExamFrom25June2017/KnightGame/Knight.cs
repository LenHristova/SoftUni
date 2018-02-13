namespace KnightGame
{
    class Knight
    {
        public int RowPos { get; set; }
        public int ColPos { get; set; }
        public int PossibleTargets { get; set; }

        public Knight(int rowPos, int colPos, int possibleTargets)
        {
            RowPos = rowPos;
            ColPos = colPos;
            PossibleTargets = possibleTargets;
        }
    }
}