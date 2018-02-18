using System;

namespace P07_FamilyTree
{
    class StartUp
    {
        static void Main()
        {
            FamilyTreeBulder.AddMainPerson(Console.ReadLine());
            FamilyTreeBulder.Build();
            FamilyTreeBulder.DisplayInfo();
        }
    }
}
