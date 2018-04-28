using System;

namespace P01_EventImplementation
{
    class StartUp
    {
        static void Main()
        {
            var nameChangeDispatcher = new NameChangeDispatcher("Len");
            var nameChangeHandler = new NameChangeHandler();
            nameChangeDispatcher.NameChange += nameChangeHandler.OnDispatcherNameChange;

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                nameChangeDispatcher.Name = input;
            }
        }
    }
}
