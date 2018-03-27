public class Threeuple<TItem1, TItem2, TItem3> : Tuple<TItem1, TItem2>, IThreeuple<TItem1, TItem2, TItem3>
{
    public Threeuple(TItem1 item1, TItem2 item2, TItem3 item3) : base(item1, item2)
    {
        this.Item3 = item3;
    }

    public TItem3 Item3 { get; set; }

    public override string ToString()
    {
        return $"{base.ToString()} -> {this.Item3}";
    }
}