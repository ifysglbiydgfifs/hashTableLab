namespace HashFunctions
{
    public interface IHashFunction
    {
        int Hash(string key, int tableSize);
    }
}