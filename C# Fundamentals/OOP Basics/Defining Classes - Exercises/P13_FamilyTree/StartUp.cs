namespace P13_FamilyTree
{
    using System;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            var serchedPersonTies = Console.ReadLine();

            var familyTree = new FamilyTree();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                if (input == null) continue;

                if (input.Contains('-'))
                {
                    familyTree.UpdateFamilyTieInfo(input);
                }
                else
                {
                    familyTree.UpdatePersonInfo(input);
                }
            }

            familyTree.DisplayInfoFor(serchedPersonTies);
        }
    }
}