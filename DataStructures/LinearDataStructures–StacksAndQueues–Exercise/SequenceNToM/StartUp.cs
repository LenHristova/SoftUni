using System;
using System.Collections.Generic;

class StartUp
{
    // We are given numbers n and m, and the following operations:
    // a)	n -> n + 1
    // b)	n -> n + 2
    // c)	n -> n * 2
    // This is a program that finds the shortest sequence of operations from the list above 
    // that starts from n and finishes in m.
    // If several shortest sequences exist, we need the first one of them.

    static void Main()
    {
        var input = Console.ReadLine().Split();
        var firstValue = int.Parse(input[0]);
        var searchedValue = int.Parse(input[1]);

        var lastAddedNode = new Node(firstValue, null);

        var traversedNodes = new Queue<Node>();
        traversedNodes.Enqueue(lastAddedNode);

        //Add new nodes whith their values and check for searched value
        while (traversedNodes.Count > 0 && lastAddedNode.Value != searchedValue)
        {
            var currentNode = traversedNodes.Dequeue();

            var newNodesValues = new[]
            {
                currentNode.Value + 1,
                currentNode.Value + 2,
                currentNode.Value * 2
            };

            //Check and add appropriate values
            foreach (var value in newNodesValues)
            {
                if (value > searchedValue)
                {
                    continue;
                }

                lastAddedNode = new Node(value, currentNode);

                if (value == searchedValue)
                {
                    break;
                }

                traversedNodes.Enqueue(lastAddedNode);
            }
        }

        //Print shortest sequence
        if (lastAddedNode.Value == searchedValue)
        {
            var result = new LinkedList<int>();
            var current = lastAddedNode;
            while (current != null)
            {
                result.AddFirst(current.Value);
                current = current.Parent;
            }

            Console.WriteLine(string.Join(" -> ", result));
        }
    }
}