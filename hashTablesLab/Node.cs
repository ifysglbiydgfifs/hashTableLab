namespace LinkedListUtil
{
    public class Node<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node<K, V> Next { get; set; }

        public Node(K key, V value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }
}