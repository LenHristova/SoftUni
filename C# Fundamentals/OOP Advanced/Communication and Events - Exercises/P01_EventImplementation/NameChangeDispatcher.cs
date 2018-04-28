using P01_EventImplementation.Contracts;

namespace P01_EventImplementation
{
    public class NameChangeDispatcher : INameable, INameChangeable
    {
        public event NameChangeEventHandler NameChange;

        private string _name;

        public NameChangeDispatcher(string name)
        {
            _name = name;
        }

        public string Name
        {
            get => _name;
            set
            {
                OnNameChange(new NameChangeEventArgs(value));
                _name = value;
            }
        }

        public void OnNameChange(NameChangeEventArgs args)
        {
            NameChange?.Invoke(this, args);
        }
    }
}
