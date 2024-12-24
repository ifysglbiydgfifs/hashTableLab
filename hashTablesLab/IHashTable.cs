namespace HashTables
{
    public interface IHashTable
    {
        string Search(string key);
        void Insert(string key, string value);
        bool Delete(string key);
        void Print();
    }
}