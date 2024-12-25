using System;
using HashTables;

namespace HashTablesTester
{
    public class OpenAddressingTester : IHashTableTester
    {
        public void RunTests(IHashTable hashTable)
        {
            TestInsertion(hashTable);
            TestTableStatistics(hashTable);
        }

        public void TestInsertion(IHashTable hashTable)
        {
            Console.WriteLine("Запуск теста вставки...");
            
            var generator = new RandomDataGenerator();
            for (int i = 0; i < 10000; i++)
            {
                var (key, value) = generator.GenerateInt();
                key = i;
                hashTable.Insert(key.ToString(), value.ToString());
            }
        }
        public void TestTableStatistics(IHashTable hashTable)
        {
            Console.WriteLine("Запуск теста статистики таблицы...");
            int elementsCount = 100000;
            HashTableStatistics.CalculateStatistics(hashTable, elementsCount);
        }
    }
}