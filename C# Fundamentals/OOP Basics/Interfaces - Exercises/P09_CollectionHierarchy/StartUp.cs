using System;
using P09_CollectionHierarchy.Contracts;
using P09_CollectionHierarchy.Models;

namespace P09_CollectionHierarchy
{
    class StartUp
    {
        static void Main()
        {
            var addCollection = new AddCollection<string>();
            var addRemoveCollection = new AddRemoveCollection<string>();
            var myList = new MyList<string>();

            var inputCollection = Console.ReadLine().Split();
            AddItemsAndPrintTheirIndex(addCollection, inputCollection);
            AddItemsAndPrintTheirIndex(addRemoveCollection, inputCollection);
            AddItemsAndPrintTheirIndex(myList, inputCollection);

            var removedCount = int.Parse(Console.ReadLine());
            RemoveItemsAndPrintThem(addRemoveCollection, removedCount);
            RemoveItemsAndPrintThem(myList, removedCount);
        }

        private static void RemoveItemsAndPrintThem(IRemovable<string> collection, int removedCount)
        {
            for (int i = 0; i < removedCount; i++)
            {
                Console.Write(collection.Remove() + " ");
            }

            Console.WriteLine();
        }

        private static void AddItemsAndPrintTheirIndex(IAddable<string> collection, string[] inputCollection)
        {
            foreach (var item in inputCollection)
            {
                Console.Write(collection.Add(item) + " ");
            }

            Console.WriteLine();
        }
    }
}