using System;

using P01_EventImplementation.Contracts;

namespace P01_EventImplementation
{
    public class NameChangeHandler : INameChangeHandler
    {

        public void OnDispatcherNameChange(object sender, NameChangeEventArgs args)
        {
            Console.WriteLine($"Dispatcher's name changed to {args.Name}.");
        }
    }
}
