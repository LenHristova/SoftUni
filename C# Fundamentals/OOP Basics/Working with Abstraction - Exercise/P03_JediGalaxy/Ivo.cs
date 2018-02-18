public class Ivo : Hero
{
    public Ivo(string input) : base(input)
    {
    }

    public new void Move()
    {
        base.Move();
        ColPosition++;
    }
}