using System.Collections.Generic;
using HashTables;

namespace HashTablesTester
{
    public interface IHashTableTester
    {
        void RunTests(IHashTable hashTable);
        void TestInsertSearchDelete(IHashTable hashTable);
        void TestTableStatistics(IHashTable hashTable);
    }
}