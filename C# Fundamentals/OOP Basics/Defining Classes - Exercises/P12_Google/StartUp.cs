namespace P12_Google
{
    using System;

    class StartUp
    {
        static void Main()
        {
            var google = new Google();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var personalInfo = input?
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                if (personalInfo == null) continue;

                var personName = personalInfo[0];
                var currentPerson = google.GetPersonInfo(personName);

                if (currentPerson == null)
                {
                    currentPerson = new Person(personName);
                    google.AddPerson(currentPerson);
                }

                AddInfo(personalInfo, currentPerson);
            }

            var searchedPerson = google.GetPersonInfo(Console.ReadLine());
            Console.WriteLine(searchedPerson);
        }

        private static void AddInfo(string[] personalInfo, Person currentPerson)
        {
            var infoType = personalInfo[1];

            switch (infoType.ToLower())
            {
                case "company":
                    AddCompanyInfo(personalInfo, currentPerson);
                    break;
                case "pokemon":
                    AddPokemonInfo(personalInfo, currentPerson);
                    break;
                case "parents":
                    AddParentsInfo(personalInfo, currentPerson);
                    break;
                case "children":
                    AddChildrenInfo(personalInfo, currentPerson);
                    break;
                case "car":
                    AddCarInfo(personalInfo, currentPerson);
                    break;
            }
        }

        private static void AddCompanyInfo(string[] personalInfo, Person person)
        {
            var name = personalInfo[2];
            var department = personalInfo[3];
            var salary = decimal.Parse(personalInfo[4]);

            person.Company = new Company(name, department, salary);
        }

        private static void AddPokemonInfo(string[] personalInfo, Person person)
        {
            var name = personalInfo[2];
            var type = personalInfo[3];
            person.Pokemons.Add(new Pokemon(name, type));
        }

        private static void AddParentsInfo(string[] personalInfo, Person person)
        {
            var parent = GetRelative(personalInfo);
            person.Parents.Add(parent);
        }

        private static void AddChildrenInfo(string[] personalInfo, Person person)
        {
            var child = GetRelative(personalInfo);
            person.Children.Add(child);
        }

        private static Relative GetRelative(string[] personalInfo)
        {
            var name = personalInfo[2];
            var birthday = personalInfo[3];
            return new Relative(name, birthday);
        }

        private static void AddCarInfo(string[] personalInfo, Person person)
        {
            var model = personalInfo[2];
            var speed = int.Parse(personalInfo[3]);

            person.Car = new Car(model, speed);
        }
    }
}