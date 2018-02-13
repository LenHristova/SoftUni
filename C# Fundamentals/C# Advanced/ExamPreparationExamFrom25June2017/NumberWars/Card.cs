namespace NumberWars
{
    class Card
    {
        public int NumberScore { get; set; }
        public int LetterScore { get; set; }

        public Card(int numberScore, int letterScore)
        {
            NumberScore = numberScore;
            LetterScore = letterScore;
        }
    }
}