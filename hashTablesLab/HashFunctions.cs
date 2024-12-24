using System;
using hashTablesLab.HashTables;

namespace HashTables
{
    public class DivisionHashFunction<K> : IHashFunction<K>
    {
        public int Hash(K key, int tableSize)
        {
            return key.GetHashCode() % tableSize;
        }
    }
    public class MultiplicationHashFunction<K> : IHashFunction<K>
    {
        private const double A = 0.6180339887;

        public int Hash(K key, int tableSize)
        {
            double hash = Convert.ToDouble(key.GetHashCode()) * A;
            return (int)(tableSize * (hash - Math.Floor(hash)));
        }
    }
    public class SummingHashFunction<K> : IHashFunction<K>
    {
        public int Hash(K key, int tableSize)
        {
            int sum = 0;
            foreach (char c in key.ToString())
            {
                sum += c;
            }
            return sum % tableSize;
        }
    }
}