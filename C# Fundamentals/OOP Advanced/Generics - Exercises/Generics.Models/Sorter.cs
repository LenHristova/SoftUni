using System;

public static class Sorter<T>
where T : IComparable<T>
{
    public static void Sort(ICustomList<T> customList)
    {
        for (int pos = 0; pos < customList.Count - 1; pos++)
        {
            if (customList[pos].CompareTo(customList[pos + 1]) > 0)
            {
                customList.Swap(pos, pos + 1);
            }
        }
    }
}