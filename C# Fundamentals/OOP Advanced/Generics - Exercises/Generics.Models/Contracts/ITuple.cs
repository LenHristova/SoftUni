public interface ITuple<TItem1, TItem2>
{
    TItem1 Item1 { get; set; }
    TItem2 Item2 { get; set; }

    string ToString();
}