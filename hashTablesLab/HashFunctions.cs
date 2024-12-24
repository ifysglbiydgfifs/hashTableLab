using System;
using System.Security.Cryptography;
using System.Text;
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
    public class XORHashFunction : IHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            uint hash = 0;  // Initialize the hash value

            foreach (char c in key)
            {
                // XOR the current character with the hash
                hash ^= (uint)c;  // Perform XOR with the character's ASCII value
                hash *= 16777619;  // Multiply by a prime number to help with dispersion
            }

            // Return the hash value mod table size to fit the table
            return (int)(hash % (uint)tableSize);
        }
    }
    
    public class SHA256HashFunction : IHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the string to a byte array and compute the hash
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));

                // Take the first 4 bytes of the hash and convert it to an integer
                int hash = BitConverter.ToInt32(hashBytes, 0);

                return Math.Abs(hash % tableSize); // Return the hash mod tableSize to fit the table size
            }
        }
    }
    
    public class FNV1HashFunction : IHashFunction
    {
        private const uint FNV_OFFSET_BASIS = 0x811C9DC5; // FNV-1 offset basis
        private const uint FNV_32_PRIME = 0x01000193;    // FNV-1 prime

        public int Hash(string key, int tableSize)
        {
            uint hash = FNV_OFFSET_BASIS;

            foreach (char c in key)
            {
                // Perform the FNV-1 hash calculation
                hash ^= (uint)c;             // XOR the byte
                hash *= FNV_32_PRIME;        // Multiply by the FNV prime
            }

            // Return the hash mod the table size
            return (int)(hash % (uint)tableSize);
        }
    }

    public class MurmurHashFunction : IHashFunction
    {
        public int Hash(string key, int tableSize)
        {
            byte[] data = Encoding.UTF8.GetBytes(key);
            uint hash = 0xc58f95f4; // MurmurHash3 default seed value
            int length = data.Length;
            int index = 0;

            // Process each 4-byte chunk
            while (length >= 4)
            {
                uint k = (uint)(data[index] | data[index + 1] << 8 | data[index + 2] << 16 | data[index + 3] << 24);
                index += 4;

                k *= 0xcc9e2d51;
                k = (k << 15) | (k >> 17); // Rotate left by 15 bits
                k *= 0x1b873593;

                hash ^= k;
                hash = (hash << 13) | (hash >> 19); // Rotate left by 13 bits
                hash = hash * 5 + 0xe6546b64;

                length -= 4;
            }

            // Process remaining bytes (less than 4)
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

            // Finalization
            hash ^= (uint)data.Length;
            hash ^= hash >> 16;
            hash *= 0x85ebca6b;
            hash ^= hash >> 13;
            hash *= 0xc2b2ae35;
            hash ^= hash >> 16;

            // Return the result mod tableSize
            return Math.Abs((int)(hash % (uint)tableSize));
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
            int hash1 = Math.Abs(StringKeyToInt.Convert(key)) % tableSize;
            int hash2 = secondHashFunction.Hash(key, tableSize);
            return (hash1 + i * hash2) % tableSize;
        }
    }

}
