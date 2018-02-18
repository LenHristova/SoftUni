namespace P01_DefineClassPerson
{
    class StartUp
    {
        static void Main()
        {
            var pesho = new Person { Name = "Pesho", Age = 20 };
            var gosho = new Person();
            gosho.Name = "Gosho";
            gosho.Age = 18;
            var stamat = new Person { Name = "Stamat", Age = 43 };
        }
    }
}