using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    private static Queue<Pump> allPumps = new Queue<Pump>();
    private static Queue<int> possibleStartingPumpIndeces = new Queue<int>();

    static void Main()
    {
        var pumpsCount = int.Parse(Console.ReadLine());

        //Add to allPumps queue all inputed pumps       
        for (int currPumpIndex = 0; currPumpIndex < pumpsCount; currPumpIndex++)
        {
            var currPumpParams = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var petrolAmount = currPumpParams[0];
            var kmToNextPump = currPumpParams[1];

            var pump = new Pump(petrolAmount, kmToNextPump);

            allPumps.Enqueue(pump);

            //If petrol is enogh to go to the next pump, add its index to possibleStartingPumpIndeces queue 
            if (petrolAmount >= kmToNextPump)
            {
                possibleStartingPumpIndeces.Enqueue(currPumpIndex);
            }
        }

        var startIndex = FindStartIndex();
        Console.WriteLine(startIndex != -1 ? startIndex.ToString() : "Impossible tour!");
    }

    //Check possible starting pump indeces one by one to find appropriate one
    //if there is no appropriate starting pump, reurn -1
    private static int FindStartIndex()
    {
        if (possibleStartingPumpIndeces.Count == 0)
            return -1;
       
        var checkedQueue = AddElementsToCheckedQueue();

        var currentPump = checkedQueue.Dequeue();
        var trackPetrolAmount = currentPump.PetrolAmount - currentPump.KmToNextPump;

        if (!IsCurrentPumpGoodStartingPoint(checkedQueue, trackPetrolAmount))
        {
            possibleStartingPumpIndeces.Dequeue();
            FindStartIndex();
        }

        return possibleStartingPumpIndeces.Peek();
    }

    //Copy all elements (pumps) starting from current starting pump index
    //Adding elements before starting element to the tail of the queue
    private static Queue<Pump> AddElementsToCheckedQueue()
    {
        var currStartingPumpIndex = possibleStartingPumpIndeces.Peek();
        var checkedQueue = new Queue<Pump>(allPumps);

        for (int i = 0; i < currStartingPumpIndex; i++)
        {
            checkedQueue.Enqueue(checkedQueue.Dequeue());
        }

        return checkedQueue;
    }

    //Check if starting from that pump index, the track will be able to complete the tour
    private static bool IsCurrentPumpGoodStartingPoint(Queue<Pump> checkedQueue, int trackPetrolAmount)
    { 
        while (checkedQueue.Count > 0)
        {
            var currentPump = checkedQueue.Dequeue();
            trackPetrolAmount += currentPump.PetrolAmount - currentPump.KmToNextPump;

            if (trackPetrolAmount < 0)
            {
                return false;
            }
        }

        return true;
    }
}