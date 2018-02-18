using System.Linq;

namespace P06_Sneaking
{
    class Sam : Hero
    {
        public Sam(int rowPosition, int colPosition) : base(rowPosition, colPosition)
        {
        }

        public override void Move(string direction)
        {
            Room.Matrix[RowPosition][ColPosition] = '.';

            switch (direction)
            {
                case "U":
                    RowPosition--;
                    break;
                case "D":
                    RowPosition++;
                    break;
                case "L":
                    ColPosition--;
                    break;
                case "R":
                    ColPosition++;
                    break;
            }

            if (Room.Matrix[RowPosition][ColPosition] == 'b' ||
                Room.Matrix[RowPosition][ColPosition] == 'd')
            {
                var killedEnemy =
                    Room.Enemies.FirstOrDefault(x => x.RowPosition == RowPosition && x.ColPosition == ColPosition);
                Room.Enemies.Remove(killedEnemy);
            }
            Room.Matrix[RowPosition][ColPosition] = 'S';
        }

        public bool IsCought(Enemy enemy)
        {
            if (enemy == null)
                return false;
            switch (enemy.Direction)
            {
                case "right":
                    return enemy.ColPosition < ColPosition;
                case "left":
                    return ColPosition < enemy.ColPosition;
                    default:
                        return false;
            }
        }
    }
}
