public interface IThreeuple<TItem1, TItem2, TItem3> :ITuple<TItem1, TItem2>
{
    TItem3 Item3 { get; set; }
}