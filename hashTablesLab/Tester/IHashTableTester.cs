using System.Collections.Generic;
using HashTables;

namespace HashTablesTester
{
    public interface IHashTableTester
    {
        void RunTests(IHashTable hashTable);
        void TestInsertion(IHashTable hashTable);
        void TestTableStatistics(IHashTable hashTable);
    }
}