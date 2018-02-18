namespace P14_CatLady
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CatalogCats
    {
        public List<Cat> Cats { get; set; } = new List<Cat>();

        public void AddCat(string catCharacteristics)
        {
            var characteristics = catCharacteristics.Split();
            var breed = characteristics[0];
            var name = characteristics[1];

            switch (breed)
            {
                case "Siamese":
                    var earSize = int.Parse(characteristics[2]);
                    Cats.Add(new Siamese(name, earSize));
                    break;
                case "Cymric":
                    var furLength = double.Parse(characteristics[2]);
                    Cats.Add(new Cymric(name, furLength));
                    break;
                case "StreetExtraordinaire":
                    var decibelsOfMeows = int.Parse(characteristics[2]);
                    Cats.Add(new StreetExtraordinaire(name, decibelsOfMeows));
                    break;
            }
        }

        public void DisplayCatCharacteristics(string catName)
        {
            var cat = Cats.FirstOrDefault(c => c.Name == catName);
            Console.WriteLine(cat);
        }
    }
}