﻿using System;
using HashFunctions;
using HashFunctions;
using HashTables;

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

                IHashTable hashTable = null;

                switch (option)
                {
                    case 1:
                        hashTable = ChooseChainHashFunction();
                        break;
                    case 2:
                        hashTable = ChooseOpenHashFunction();
                        break;
                    case 3:
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

        static IHashTable ChooseChainHashFunction()
        {
            IHashFunction hashFunction = null;
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
                        hashFunction = new DivisionHashFunction();
                        break;
                    case 2:
                        hashFunction = new MultiplicationHashFunction();
                        break;
                    case 3:
                        hashFunction = new SummingHashFunction();
                        break;
                    case 0:
                        return null;
                    default:
                        Console.WriteLine("Неверный выбор");
                        continue;
                }
            }

            return new ChainHashTable(hashFunction);
        }
        
        static IHashTable ChooseOpenHashFunction()
        {
            IOpenHashFunction probingFunction = null;
            while (probingFunction == null)
            {
                Console.WriteLine("Выберите метод разрешения коллизий:");
                Console.WriteLine("1 - Линейное пробирование");
                Console.WriteLine("2 - Квадратичное пробирование");
                Console.WriteLine("3 - Двойное хеширование");

                int probingOption = ParseInput();
                switch (probingOption)
                {
                    case 1:
                        probingFunction = new LinearProbing();
                        break;
                    case 2:
                        probingFunction = new QuadraticProbing();
                        break;
                    case 3:
                        Console.WriteLine("Выберите вторую хеш-функцию для двойного хеширования:");
                        IHashFunction secondHashFunction = ChooseSecondaryHashFunction();
                        probingFunction = new DoubleHashing(secondHashFunction);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор метода пробирования.");
                        continue;
                }
            }

            Console.WriteLine("Выберите вспомогательную хеш-функцию");
            IHashFunction hashFunction = ChooseSecondaryHashFunction();
    
            return new OpenAddressingHashTable(hashFunction, probingFunction);
        }

        static IHashFunction ChooseSecondaryHashFunction()
        {
            IHashFunction hashFunction = null;
            while (hashFunction == null)
            {
                Console.WriteLine("1 - Хеш-функция на основе деления");
                Console.WriteLine("2 - Хеш-функция на основе умножения");
                Console.WriteLine("0 - Выход");

                int hashFunctionOption = ParseInput();
                switch (hashFunctionOption)
                {
                    case 1:
                        hashFunction = new DivisionHashFunction();
                        break;
                    case 2:
                        hashFunction = new MultiplicationHashFunction();
                        break;
                    case 0:
                        return null;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        continue;
                }
            }
            return hashFunction;
        }

        static void HandleHashTableActions(IHashTable hashTable)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
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

        static void InsertElement(IHashTable hashTable)
        {
            Console.Write("Введите ключ для вставки: ");
            string keyToInsert = Console.ReadLine();
            Console.Write("Введите значение для вставки: ");
            string valueToInsert = Console.ReadLine();
            hashTable.Insert(keyToInsert, valueToInsert);
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
