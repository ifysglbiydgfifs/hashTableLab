using System;
using HashFunctions;
using LinkedListUtil;

namespace HashTables
{
    class ChainHashTable : IHashTable
    {
        private const int _tableSize = 1000;
        private LinkedList<string, string>[] table = new LinkedList<string, string>[_tableSize];
        private IHashFunction _hashFunction;
        
        public ChainHashTable(IHashFunction hashFunction)
        {
            _hashFunction = hashFunction;

            for (int i = 0; i < _tableSize; i++)
            {
                table[i] = new LinkedList<string, string>();
            }
        }
        private int GetHash(string key)
        {
            return _hashFunction.Hash(key, _tableSize);
        }
        public void Insert(string key, string value)
        {
            if (Convert.ToInt32(key) < 0 || Convert.ToInt32(key) >= _tableSize)
            {
                Console.WriteLine($"Ключ {key} должен быть в пределах от 0 до {_tableSize - 1}. Вставка не выполнена.");
                return;
            }

            int hash = GetHash(key);
            if (table[hash].Search(key) != null)
            {
                Console.WriteLine($"Элемент с ключом {key} уже существует. Вставка не выполнена.");
                return;
            }
            table[hash].Insert(key, value);
            Console.WriteLine($"Элемент с ключом {key} успешно вставлен.");
        }

        public string Search(string key)
        {
            int hash = GetHash(key);
            return table[hash].Search(key);
        }

        public bool Delete(string key)
        {
            int hash = GetHash(key);
            return table[hash].Delete(key);
        }

        public void Print()
        {
            bool isPrinted = false;

            for (int i = 0; i < _tableSize; i++)
            {
                var current = table[i].Head;
                if (current != null)
                {
                    isPrinted = true;
                    Console.Write($"{i}: ");
                    table[i].Print();
                    Console.WriteLine();
                }
            }

            if (!isPrinted)
            {
                Console.WriteLine("Таблица пуста.");
            }
        }
    }
}
