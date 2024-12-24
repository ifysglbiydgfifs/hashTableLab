using System;
using HashFunctions;
using HashTables;

public class OpenAddressingHashTable : IHashTable
{
    private const int _tableSize = 10000;
    private (string Key, string Value)[] _table;
    private IHashFunction _hashFunction;
    private IOpenHashFunction _probingFunction;

    public OpenAddressingHashTable(IHashFunction hashFunction, IOpenHashFunction probingFunction)
    {
        _hashFunction = hashFunction;
        _probingFunction = probingFunction;
        _table = new (string, string)[_tableSize];

        for (int i = 0; i < _tableSize; i++)
        {
            _table[i] = (null, null);
        }
    }

    private int GetHash(string key)
    {
        return _hashFunction.Hash(key, _tableSize);
    }

    public void Insert(string key, string value)
    {
        int hash = GetHash(key);
        int i = 0;
        int index;

        while (i < _tableSize)
        {
            index = (hash + _probingFunction.Hash(key, i, _tableSize)) % _tableSize;

            if (Convert.ToInt32(key) < 0 || Convert.ToInt32(key) >= _tableSize)
            {
                Console.WriteLine($"Ключ {key} должен быть в пределах от 0 до {_tableSize - 1}. Вставка не выполнена.");
                return;
            }
            if (_table[index].Key == null || _table[index].Key.Equals("[deleted]"))
            {
                _table[index] = (key, value);  // Вставляем элемент
                Console.WriteLine($"Элемент с ключом {key} успешно вставлен.");
                return;
            }

            i++;
        }

        Console.WriteLine("Таблица заполнена, вставка невозможна.");
    }

    public string Search(string key)
    {
        int hash = GetHash(key);
        int i = 0;
        int index;

        while (i < _tableSize)
        {
            index = (hash + _probingFunction.Hash(key, i, _tableSize)) % _tableSize;

            if (_table[index].Key == null)
                return null;

            if (_table[index].Key.Equals(key) && _table[index].Key != "[deleted]")
                return _table[index].Value;
            i++;
        }

        return null;
    }

    public bool Delete(string key)
    {
        int hash = GetHash(key);
        int i = 0;
        int index;

        while (i < _tableSize)
        {
            index = (hash + _probingFunction.Hash(key, i, _tableSize)) % _tableSize;

            if (_table[index].Key == null)
                return false;

            if (_table[index].Key.Equals(key) && _table[index].Key != "[deleted]")
            {
                _table[index] = ("[deleted]", null);
                return true;
            }
            i++;
        }

        return false;
    }

    public void Print()
    {
        bool isEmpty = true;

        for (int i = 0; i < _tableSize; i++)
        {
            if (_table[i].Key != null && !_table[i].Key.Equals("[deleted]"))
            {
                Console.WriteLine($"{i}: [{_table[i].Key}, {_table[i].Value}]");
                isEmpty = false;
            }
        }

        if (isEmpty)
        {
            Console.WriteLine("Таблица пуста");
        }
    }
}
