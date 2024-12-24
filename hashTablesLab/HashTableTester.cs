using System;
using System.Collections.Generic;
using HashFunctions;
    
namespace HashTables
{
    
    public class HashTableTester
    {
        private const int TableSize = 1000;
        private const int NumberOfElements = 100000;

        // Generate random keys
        private static List<string> GenerateRandomKeys(int numberOfKeys)
        {
            var keys = new List<string>();
            var random = new Random();

            for (int i = 0; i < numberOfKeys; i++)
            {
                // Create random string of length 10 for each key
                var key = Guid.NewGuid().ToString("N").Substring(0, 10); // Random key of 10 characters
                keys.Add(key);
            }

            return keys;
        }

        // Function to run the tests on a specific hash table
        private static void RunTests(IHashTable hashTable, List<string> keys)
        {
            // Insert all elements
            foreach (var key in keys)
            {
                hashTable.Insert(key, key);
            }

            // Calculate load factor
            double loadFactor = CalculateLoadFactor(hashTable);
            Console.WriteLine($"Load Factor: {loadFactor}");

            // Calculate longest and shortest chain lengths
            var (longest, shortest) = CalculateChainLengths(hashTable);
            Console.WriteLine($"Longest Chain Length: {longest}");
            Console.WriteLine($"Shortest Chain Length: {shortest}");
        }

        // Calculate load factor
        private static double CalculateLoadFactor(IHashTable hashTable)
        {
            // Count the number of non-empty slots
            int filledSlots = 0;
            for (int i = 0; i < TableSize; i++)
            {
                //var chain = hashTable.GetChain(i); // Implement GetChain method in IHashTable interface
                /*if (chain != null && chain.Count > 0)
                {
                    filledSlots++;
                }*/
            }

            return (double)filledSlots / TableSize;
        }

        // Calculate the longest and shortest chain lengths
        private static (int longest, int shortest) CalculateChainLengths(IHashTable hashTable)
        {
            int longest = 0;
            int shortest = int.MaxValue;

            for (int i = 0; i < TableSize; i++)
            {
                /*var chain = hashTable.GetChain(i); // Implement GetChain method in IHashTable interface
                if (chain != null && chain.Count > 0)
                {
                    longest = Math.Max(longest, chain.Count);
                    shortest = Math.Min(shortest, chain.Count);
                }*/
            }

            return (longest, shortest);
        }

        // Compare multiple hash functions
        public static void CompareHashFunctions()
        {
            var keys = GenerateRandomKeys(NumberOfElements);

            // Test each hash function
            IHashFunction[] hashFunctions = new IHashFunction[]
            {
                new DivisionHashFunction(),
                new MultiplicationHashFunction(),
                new XORHashFunction(),
                new SHA256HashFunction(),
                new FNV1HashFunction(),
                new MurmurHashFunction()
            };

            foreach (var hashFunction in hashFunctions)
            {
                Console.WriteLine($"Testing hash function: {hashFunction.GetType().Name}");

                var hashTable = new ChainHashTable(hashFunction); // ChainHashTable using the current hash function
                RunTests(hashTable, keys);
                Console.WriteLine();
            }
        }
    }
}