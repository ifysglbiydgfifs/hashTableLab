using System;
using HashFunctions;
using LinkedListUtil;

namespace HashTables
{
    class ChainHashTable : IHashTable
    {
        private const int _tableSize = 1000;
        private int _elementCount = 0;
        private LinkedList<string, string>[] _table = new LinkedList<string, string>[_tableSize];
        private IHashFunction _hashFunction;
        
        public ChainHashTable(IHashFunction hashFunction)
        {
            _hashFunction = hashFunction;
            for (int i = 0; i < _tableSize; i++)
            {
                _table[i] = new LinkedList<string, string>();
            }
        }

        public object GetTable() => _table;
        public int GetTableSize() => _tableSize;
        
        public LinkedList<string, string> GetChain(int index)
        {
            if (index < 0 || index >= _tableSize)
                throw new IndexOutOfRangeException("Неверный индекс.");
            
            return _table[index];
        }
        private int GetHash(string key)
        {
            return _hashFunction.Hash(key, _tableSize);
        }
        public void Insert(string key, string value)
        {
            if (_elementCount >= _tableSize)
            {
                Console.WriteLine("Таблица заполнена, вставка невозможна.");
                return;
            }

            int hash = GetHash(key);
            if (_table[hash].Search(key) != null)
            {
                Console.WriteLine($"Элемент с хешкодом {hash} [{key}:{value}] уже существует. Вставка не выполнена.");
                return;
            }
        
            _table[hash].Insert(key, value);
            _elementCount++;
            Console.WriteLine($"Элемент с хешкодом {hash} [{key}:{value}] успешно вставлен.");
        }


        public string Search(string key)
        {
            int hash = GetHash(key);
            return _table[hash].Search(key);
        }

        public bool Delete(string key)
        {
            int hash = GetHash(key);
            return _table[hash].Delete(key);
        }

        public void Print()
        {
            bool isPrinted = false;

            for (int i = 0; i < _tableSize; i++)
            {
                var current = _table[i].Head;
                if (current != null)
                {
                    Console.Write($"{i} ");
                    isPrinted = true;
                    _table[i].Print();
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
