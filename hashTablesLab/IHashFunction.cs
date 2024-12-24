namespace hashTablesLab
{
    namespace HashTables
    {
        public interface IHashFunction<K>
        {
            int Hash(K key, int tableSize);
        }
    }
}