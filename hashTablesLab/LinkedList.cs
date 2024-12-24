using System;

namespace LinkedListUtil
{
    public class LinkedList<K, V>
    {
        public Node<K, V> Head { get; private set; }

        public LinkedList()
        {
            Head = null;
        }
        public void Insert(K key, V value)
        {
            var newNode = new Node<K, V>(key, value)
            {
                Next = Head
            };
            Head = newNode;
        }
        public V Search(K key)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return default(V);
        }
        public bool Delete(K key)
        {
            if (Head == null) return false;

            if (Head.Key.Equals(key))
            {
                Head = Head.Next;
                return true;
            }

            var current = Head;
            while (current.Next != null)
            {
                if (current.Next.Key.Equals(key))
                {
                    current.Next = current.Next.Next;
                    return true;
                }

                current = current.Next;
            }

            return false;
        }
        public void Print()
        {
            var current = Head;
            while (current != null)
            {
                Console.Write($"[{current.Key}: {current.Value}] ");
                current = current.Next;
            }
        }
    }
}