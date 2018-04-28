namespace P01_EventImplementation.Contracts
{
    public delegate void NameChangeEventHandler(object sender, NameChangeEventArgs args);

    public interface INameChangeable : INameable
    {
        event NameChangeEventHandler NameChange;

        void OnNameChange(NameChangeEventArgs args);
    }
}
