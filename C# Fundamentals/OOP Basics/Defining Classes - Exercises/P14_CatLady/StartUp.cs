namespace P14_CatLady
{
    using System;

    class StartUp
    {
        static void Main()
        {
            var catalogCats = new CatalogCats();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                if(input == null) continue;
                catalogCats.AddCat(input);
            }

            var searchedCatName = Console.ReadLine();
            catalogCats.DisplayCatCharacteristics(searchedCatName);
        }
    }
}