using System;
using HashTables;

namespace HashTablesTester
{
    public class ChainTester : IHashTableTester
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
            for (int i = 0; i < 100000; i++)
            {
                var (key, value) = generator.GenerateInt();
                hashTable.Insert(key.ToString(), value.ToString());
            }
        }

        static void SearchElement(IHashTable hashTable)
        {
            Console.Write("Введите ключ для поиска: ");
            string keyToSearch = Console.ReadLine();
            string foundValue = hashTable.Search(keyToSearch);
            Console.WriteLine(foundValue != null ? $"Элемент найден: {foundValue}" : "Элемент не найден.");
        }

        static void DeleteElement(IHashTable hashTable)
        {
            Console.Write("Введите ключ для удаления: ");
            string keyToDelete = Console.ReadLine();
            bool isDeleted = hashTable.Delete(keyToDelete);
            Console.WriteLine(isDeleted ? "Элемент удален." : "Элемент не найден.");
        }
    }
}