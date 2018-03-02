namespace P09_CollectionHierarchy.Contracts
{
    public interface IAddable<T>
    {
        int Add(T item);
    }
}