namespace HashFunctions
{
    public interface IOpenHashFunction
    {
        int Hash(string key, int i, int tableSize);
    }
}