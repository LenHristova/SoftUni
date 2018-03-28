using System.Collections;
using System.Collections.Generic;

namespace P09_LinkedListTraversal
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Value { get; }

            public Node Prev { get; set; }

            public Node Next { get; set; }

            public Node(T value)
            {
                this.Value = value;
            }
        }

        private Node _head;
        private Node _tail;

        public LinkedList()
        {
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public void Add(T item)
        {
            var newNode = new Node(item);

            if (IsEmpty)
            {
                _head = _tail = newNode;
            }
            else
            {
                newNode.Prev = _tail;
                _tail.Next = newNode;
                _tail = newNode;
            }
        
            Count++;
        }

        public bool Remove(T item)
        {
            var currentNode = _head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    if (currentNode == _head)
                    {
                        _head = currentNode.Next;
                        _head.Prev = null;
                    }
                    else if (currentNode == _tail)
                    {
                        _tail = currentNode.Prev;
                        _tail.Next = null;
                    }
                    else
                    {
                        currentNode.Prev.Next = currentNode.Next;
                        currentNode.Next.Prev = currentNode.Prev;
                    }

                    Count--;
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}