using System;
using hashTablesLab.HashTables;
using LinkedListUtil;

namespace HashTables
{
    class ChainHashTable<K, V> : IHashTable<K, V>
    {
        private const int _tableSize = 1000;
        private LinkedList<K, V>[] table = new LinkedList<K, V>[_tableSize];
        private IHashFunction<K> _hashFunction;
        public ChainHashTable(IHashFunction<K> hashFunction)
        {
            _hashFunction = hashFunction;

            for (int i = 0; i < _tableSize; i++)
            {
                table[i] = new LinkedList<K, V>();
            }
        }
        private int GetHash(K key)
        {
            return _hashFunction.Hash(key, _tableSize);
        }
        public void Insert(K key, V value)
        {
            // Проверка, что ключ в пределах допустимого диапазона
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

        public V Search(K key)
        {
            int hash = GetHash(key);
            return table[hash].Search(key);
        }
        public bool Delete(K key)
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