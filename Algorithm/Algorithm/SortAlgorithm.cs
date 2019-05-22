using System;
using System.Text;

namespace Algorithm
{
    public class SortAlgorithm<T>
            where T : IComparable<T>
    {

        public static void InsertSort(T[] keys)
        {
            var hi = keys.Length;
            for (int i = 1; i < hi; i++)
            {
                int j = i - 1;
                var value = keys[i];
                while (j >= 0 && keys[j].CompareTo(value) > 0)
                {
                    keys[j + 1] = keys[j];
                    j--;
                }
                keys[j + 1] = value;

            }
        }

        public static void BubblingSort(T[] keys)
        {
            var hi = keys.Length;
            for (int i = 0; i < hi; i++)
            {
                for (int j = 1; j < hi; j++)
                {
                    SwapIfGreaterWithItems(keys, j - 1, j);
                }
            }
        }
        
        public static void QuickSort(T[] keys, int lo, int hi)
        {
            if (lo < hi)
            {
                var middle = FindIndex(keys, lo, hi);
                QuickSort(keys, lo, middle - 1);
                QuickSort(keys, middle + 1, hi);
            }
        }


        private static int FindIndex(T[] keys, int lo, int hi)
        {
            T key = keys[lo];
            while (lo < hi)
            {
                while (lo < hi && key.CompareTo(keys[hi]) < 0)
                {
                    hi--;
                }
                if (lo < hi)
                {
                    // T temp = keys[hi];
                    keys[lo] = keys[hi];
                    lo++;
                }
                while (lo < hi && key.CompareTo(keys[lo]) > 0)
                {
                    lo++;
                }
                if (lo < hi)
                {
                    //T temp = keys[lo];
                    keys[hi] = keys[lo];
                    hi--;
                }
                keys[lo] = key;
            }
            return lo;

        }




        private static void SwapIfGreaterWithItems(T[] keys, int a, int b)
        {

            if (a != b)
            {
                if (keys[a] != null && keys[a].CompareTo(keys[b]) > 0)
                {
                    T key = keys[a];
                    keys[a] = keys[b];
                    keys[b] = key;
                }
            }
        }
    }
}
