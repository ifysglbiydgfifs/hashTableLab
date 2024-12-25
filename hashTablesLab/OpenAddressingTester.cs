using System;
using HashTables;

namespace HashTablesTester
{
    public class OpenAddressingTester : IHashTableTester
    {
        public void RunTests(IHashTable hashTable)
        {
            TestInsertSearchDelete(hashTable);
            TestTableStatistics(hashTable);
        }

        public void TestInsertSearchDelete(IHashTable hashTable)
        {
            Console.WriteLine("Запуск теста вставки, поиска и удаления...");
            InsertRandomElements(hashTable);
        }

        public void TestTableStatistics(IHashTable hashTable)
        {
            Console.WriteLine("Запуск теста статистики таблицы...");
            int elementsCount = 100000;
            HashTableStatistics.CalculateStatistics(hashTable, elementsCount);
        }

        static void InsertRandomElements(IHashTable hashTable)
        {
            var generator = new RandomDataGenerator();
            for (int i = 0; i < 10000; i++)
            {
                var (key, value) = generator.GenerateInt();
                hashTable.Insert(key.ToString(), value.ToString());
            }
        }
    }
}