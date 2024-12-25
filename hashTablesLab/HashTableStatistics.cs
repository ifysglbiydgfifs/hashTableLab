using System;
using HashTables;

namespace HashTablesTester
{
    public static class HashTableStatistics
    {
        public static void CalculateStatistics(IHashTable hashTable, int elementsCount)
        {
            
            if (hashTable is OpenAddressingHashTable openHashTable)
            {
                int tableSize = hashTable.GetTableSize();
                int filledSlots = 0;
                int emptySlots = 0;
                
                int currentClusterLength = 0;
                int maxClusterLength = 0;
                int totalClusterLength = 0;

                for (int i = 0; i < tableSize; i++)
                {
                    var element = openHashTable.Get(i);

                    if (element != null)
                    {
                        filledSlots++;
                        currentClusterLength++;
                        totalClusterLength++;

                        maxClusterLength = Math.Max(maxClusterLength, currentClusterLength);
                    }
                    else
                    {
                        currentClusterLength = 0;
                        emptySlots++;
                    }
                }

                double fillFactor = (double)filledSlots / tableSize;
                double avgClusterLength = (double)totalClusterLength / filledSlots;

                Console.WriteLine($"Размер таблицы: {tableSize}");
                Console.WriteLine($"Заполненные слоты: {filledSlots}");
                Console.WriteLine($"Пустые слоты: {emptySlots}");
                Console.WriteLine($"Коэффициент заполнения: {fillFactor:P2}");
                Console.WriteLine($"Длина самого длинного кластера: {maxClusterLength}");
                Console.WriteLine($"Средняя длина кластера: {avgClusterLength:F2}");
            }
            if (hashTable is ChainHashTable chainHashTable)
            {
                int tableSize = hashTable.GetTableSize();
                int filledSlots = 0;
                int emptySlots = 0;
                int minChainLength = int.MaxValue;
                int maxChainLength = int.MinValue;
                int totalChainLength = 0;

                for (int i = 0; i < tableSize; i++)
                {
                    var chain = chainHashTable.GetChain(i);
                    int chainLength = chain != null ? chain.Count : 0;

                    if (chainLength > 0)
                    {
                        filledSlots++;
                        totalChainLength += chainLength;
                        minChainLength = Math.Min(minChainLength, chainLength);
                        maxChainLength = Math.Max(maxChainLength, chainLength);
                    }
                    else
                    {
                        emptySlots++;
                    }
                }

                double fillFactor = (double)filledSlots / tableSize;
                double avgChainLength = (double)totalChainLength / filledSlots;

                Console.WriteLine($"Размер таблицы: {tableSize}");
                Console.WriteLine($"Заполненные слоты: {filledSlots}");
                Console.WriteLine($"Пустые слоты: {emptySlots}");
                Console.WriteLine($"Коэффициент заполнения: {fillFactor:P2}");
                Console.WriteLine($"Длина самой короткой цепочки: {minChainLength}");
                Console.WriteLine($"Длина самой длинной цепочки: {maxChainLength}");
                Console.WriteLine($"Средняя длина цепочек: {avgChainLength:F2}");
            }
        }
    }
}