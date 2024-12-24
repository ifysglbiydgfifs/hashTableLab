using System;
using HashFunctions;

namespace HashFunctions
{
    public class StringKeyToInt
    {
        public static int Convert(string key)
        {
            int result = 0;
            int baseValue = 128;

            for (int i = 0; i < key.Length; i++)
            {
                result = result * baseValue + key[i];
            }
            return result;
        }
    }
    public class DivisionHashFunction : IHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            int intKey = StringKeyToInt.Convert(key);

            return Math.Abs(intKey % tableSize);
        }
    }
    public class MultiplicationHashFunction : IHashFunction
    {
        private const double A = 0.6180339887;
        public int Hash(string key, int tableSize)
        {
            int intKey = StringKeyToInt.Convert(key);
            double A = 0.6180339887;

            return Math.Abs((int)(tableSize * (intKey * A % 1)));
        }
    }

    public class SummingHashFunction : IHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            int sum = 0;
            foreach (char c in key)
            {
                sum += c;
            }
            return sum % tableSize;
        }
    }

    public class LinearProbing : IOpenHashFunction
    {
        public int Hash(string key, int i, int tableSize)
        {
            return i;
        }
    }
    public class QuadraticProbing : IOpenHashFunction
    {
        public int Hash(string key, int i, int tableSize)
        {
            return i * i;
        }
    }
    public class DoubleHashing : IOpenHashFunction
    {
        private IHashFunction secondHashFunction;

        public DoubleHashing(IHashFunction secondHashFunction)
        {
            this.secondHashFunction = secondHashFunction;
        }

        public int Hash(string key, int i, int tableSize)
        {
            int hash1 = Math.Abs(key.GetHashCode()) % tableSize;
            int hash2 = secondHashFunction.Hash(key, tableSize);
            return (hash1 + i * hash2) % tableSize;
        }
    }
}
