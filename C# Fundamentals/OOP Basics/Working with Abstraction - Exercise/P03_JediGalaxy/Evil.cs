public class Evil : Hero
{
    public Evil(string input) : base(input)
    {
    }

    public new void Move() 
    {
        base.Move();
        ColPosition--;
    }
}