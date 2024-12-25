using System;
using System.Security.Cryptography;
using System.Text;

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
    public class DivisionHashFunction : IChainHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            int intKey = StringKeyToInt.Convert(key);

            return Math.Abs(intKey % tableSize);
        }
    }
    public class MultiplicationHashFunction : IChainHashFunction
    {
        private const double A = 0.6180339887;
        public int Hash(string key, int tableSize)
        {
            int intKey = StringKeyToInt.Convert(key);
            double A = 0.6180339887;

            return Math.Abs((int)(tableSize * (intKey * A % 1)));
        }
    }
    public class XORHashFunction : IChainHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            uint hash = 0;

            foreach (char c in key)
            {
                hash ^= (uint)c;
                hash *= 16777619;
            }
            return (int)(hash % (uint)tableSize);
        }
    }
    
    public class SHA256HashFunction : IChainHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                int hash = BitConverter.ToInt32(hashBytes, 0);
                return Math.Abs(hash % tableSize);
            }
        }
    }
    
    public class FNV1HashFunction : IChainHashFunction
    {
        private const uint FNV_OFFSET_BASIS = 0x811C9DC5;
        private const uint FNV_32_PRIME = 0x01000193;

        public int Hash(string key, int tableSize)
        {
            uint hash = FNV_OFFSET_BASIS;

            foreach (char c in key)
            {
                hash ^= (uint)c;
                hash *= FNV_32_PRIME;
            }
            return (int)(hash % (uint)tableSize);
        }
    }

    public class MurmurHashFunction : IChainHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            byte[] data = Encoding.UTF8.GetBytes(key);
            uint hash = 0xc58f95f4;
            int length = data.Length;
            int index = 0;
            while (length >= 4)
            {
                uint k = (uint)(data[index] | data[index + 1] << 8 | data[index + 2] << 16 | data[index + 3] << 24);
                index += 4;

                k *= 0xcc9e2d51;
                k = (k << 15) | (k >> 17);
                k *= 0x1b873593;

                hash ^= k;
                hash = (hash << 13) | (hash >> 19);
                hash = hash * 5 + 0xe6546b64;

                length -= 4;
            }
            if (length > 0)
            {
                uint k = 0;
                for (int i = 0; i < length; i++)
                {
                    k |= (uint)(data[index + i] << (i * 8));
                }
                k *= 0xcc9e2d51;
                k = (k << 15) | (k >> 17);
                k *= 0x1b873593;

                hash ^= k;
            }
            hash ^= (uint)data.Length;
            hash ^= hash >> 16;
            hash *= 0x85ebca6b;
            hash ^= hash >> 13;
            hash *= 0xc2b2ae35;
            hash ^= hash >> 16;
            return Math.Abs((int)(hash % (uint)tableSize));
        }
    }
    public class LinearProbing : IOpenHashFunction
    {
        public int Hash(string key, int i, int tableSize)
        {
            return (Math.Abs(key.GetHashCode()) + i) % tableSize;
        }
    }
    public class QuadraticProbing : IOpenHashFunction
    {
        public int Hash(string key, int i, int tableSize)
        {
            int initialIndex = Math.Abs(key.GetHashCode()) % tableSize;
            return (initialIndex + i * i) % tableSize;
        }
    }

    public class DoubleHashing : IOpenHashFunction
    {
        private IChainHashFunction secondHashFunction;

        public DoubleHashing(IChainHashFunction secondHashFunction)
        {
            this.secondHashFunction = secondHashFunction;
        }

        public int Hash(string key, int i, int tableSize)
        {
            int hash1 = Math.Abs(StringKeyToInt.Convert(key)) % tableSize;
            int hash2 = secondHashFunction.Hash(key, tableSize);
            return (hash1 + i * hash2) % tableSize;
        }
    }
    public class ExponentialProbing : IOpenHashFunction
    {
        public int Hash(string key, int i, int tableSize)
        {
            int baseIndex = StringKeyToInt.Convert(key) % tableSize;
            int step = (int)Math.Pow(2, i);
            return (baseIndex + step) % tableSize;
        }
    }

    public class FibonacciProbing : IOpenHashFunction
    {
        private int Fibonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int a = 0;
            int b = 1;
            int fib = 1;

            for (int i = 2; i <= n; i++)
            {
                fib = a + b;
                a = b;
                b = fib;
            }
            return fib;
        }
        public int Hash(string key, int i, int tableSize)
        {
            int fibStep = Fibonacci(i);
            return (StringKeyToInt.Convert(key) + fibStep) % tableSize;
        }
    }

}
