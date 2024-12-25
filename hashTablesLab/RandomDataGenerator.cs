using System;

public class RandomDataGenerator
{
    private Random _random = new Random();
    public string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var randomString = new char[length];
        for (int i = 0; i < length; i++)
        {
            randomString[i] = chars[_random.Next(chars.Length)];
        }
        return new string(randomString);
    }
    public (int Key, string Value) GenerateString()
    {
        int key = _random.Next();
        string value = GenerateRandomString(15);
        return (key, value);
    }
    public (int Key, int Value) GenerateInt()
    {
        int key = _random.Next();
        int value = _random.Next();
        return (key, value);
    }
}