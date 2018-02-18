namespace P02_CreatingConstructors
{
    class StartUp
    {
        static void Main()
        {
            var pesho = new Person { Name = "Pesho", Age = 20 };
            var gosho = new Person();
            gosho.Name = "Gosho";
            gosho.Age = 18;
            var stamat = new Person("Stamat", 43);
        }
    }
}