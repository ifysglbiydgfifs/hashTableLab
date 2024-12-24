using System;
using HashTables;
using hashTablesLab.HashTables;

namespace hashTablesLab
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1. Хэш-таблица с цепочками.");
                Console.WriteLine("2. Хэш-таблица c с открытой адресацией ");
                Console.WriteLine("3. Запустить тесты.");
                
                int option = ParseInput();
                if (option == 0) break;

                IHashTable<int, string> hashTable = null;

                switch (option)
                {
                    case 1:
                        hashTable = ChooseHashFunction();
                        break;
                    case 2:
                        //
                        break;
                    case 3:
                        //
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        continue;
                }

                if (hashTable != null)
                {
                    HandleHashTableActions(hashTable);
                }
            }
        }


        static IHashTable<int, string> ChooseHashFunction()
        {
            IHashFunction<int> hashFunction = null;
            while (hashFunction == null)
            {
                Console.WriteLine("Выберите хеш-функцию:");
                Console.WriteLine("1 - Хеш-функция на основе деления");
                Console.WriteLine("2 - Хеш-функция на основе умножения");
                Console.WriteLine("3 - Хеш-функция на основе суммирования");
                Console.WriteLine("0 - Выход");

                int hashFunctionOption = ParseInput();
                switch (hashFunctionOption)
                {
                    case 1:
                        hashFunction = new DivisionHashFunction<int>();
                        break;
                    case 2:
                        hashFunction = new MultiplicationHashFunction<int>();
                        break;
                    case 3:
                        hashFunction = new SummingHashFunction<int>();
                        break;
                    case 0:
                        return null;
                    default:
                        Console.WriteLine("Неверный выбор");
                        continue;
                }
            }

            return new ChainHashTable<int, string>(hashFunction);
        }

        static void HandleHashTableActions(IHashTable<int, string> hashTable)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Вставить элемент");
                Console.WriteLine("2 - Найти элемент");
                Console.WriteLine("3 - Удалить элемент");
                Console.WriteLine("4 - Вывести таблицу");
                Console.WriteLine("0 - Выход");

                int action = ParseInput();
                if (action == 0) break;

                switch (action)
                {
                    case 1:
                        InsertElement(hashTable);
                        break;
                    case 2:
                        SearchElement(hashTable);
                        break;
                    case 3:
                        DeleteElement(hashTable);
                        break;
                    case 4:
                        hashTable.Print();
                        break;
                    default:
                        Console.WriteLine("Неправильное действие.");
                        break;
                }
            }
        }

        static void InsertElement(IHashTable<int, string> hashTable)
        {
            Console.Write("Введите ключ для вставки: ");
            int keyToInsert = ParseInput();
            Console.Write("Введите значение для вставки: ");
            string valueToInsert = Console.ReadLine();
            hashTable.Insert(keyToInsert, valueToInsert);
        }

        static void SearchElement(IHashTable<int, string> hashTable)
        {
            Console.Write("Введите ключ для поиска: ");
            int keyToSearch = ParseInput();
            string foundValue = hashTable.Search(keyToSearch);
            Console.WriteLine(foundValue != null ? $"Элемент найден: {foundValue}" : "Элемент не найден.");
        }

        static void DeleteElement(IHashTable<int, string> hashTable)
        {
            Console.Write("Введите ключ для удаления: ");
            int keyToDelete = ParseInput();
            bool isDeleted = hashTable.Delete(keyToDelete);
            Console.WriteLine(isDeleted ? "Элемент удален." : "Элемент не найден.");
        }
        static int ParseInput()
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите целое число.");
                }
            }
        }
    }
}
