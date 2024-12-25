namespace HashFunctions
{
    public interface IChainHashFunction
    {
        int Hash(string key, int tableSize);
    }
}