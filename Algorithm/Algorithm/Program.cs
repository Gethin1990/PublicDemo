using System;
using System.Diagnostics.Contracts;

namespace Algorithm
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] unsorted = {
                         4, 1, 5, 2, 6, 3, 7, 9, 8, 10,
                         20, 11, 19, 12, 18, 17, 15, 16, 13, 14
                       };
            //SortAlgorithm<int>.BubblingSort(unsorted);
            //SortAlgorithm<int>.InsertSort(unsorted);
            SortAlgorithm<int>.QuickSort(unsorted, 0, unsorted.Length - 1);
            //GenericIntroSorter<int>.IntrospectiveSort(unsorted, -1, unsorted.Length);

            foreach (var key in unsorted)
            {
                Console.Write("{0} ", key);
            }

            Console.Read();
        }

        internal class GenericIntroSorter<T>
            where T : IComparable<T>
        {
            internal const int IntrosortSwitchToInsertionSortSizeThreshold = 16;

            internal static void IntrospectiveSort(T[] keys, int left, int length)
            {
                Contract.Requires(keys != null);
                Contract.Requires(left >= 0);
                Contract.Requires(length >= 0);
                Contract.Requires(length <= keys.Length);
                Contract.Requires(length + left <= keys.Length);

                if (length < 2)
                    return;

                IntroSort(keys, left, length + left - 1, 2 * FloorLog2(keys.Length));
            }

            private static void IntroSort(T[] keys, int lo, int hi, int depthLimit)
            {
                Contract.Requires(keys != null);
                Contract.Requires(lo >= 0);
                Contract.Requires(hi < keys.Length);

                while (hi > lo)
                {
                    int partitionSize = hi - lo + 1;
                    if (partitionSize <= IntrosortSwitchToInsertionSortSizeThreshold)
                    {
                        if (partitionSize == 1)
                        {
                            return;
                        }
                        if (partitionSize == 2)
                        {
                            SwapIfGreaterWithItems(keys, lo, hi);
                            return;
                        }
                        if (partitionSize == 3)
                        {
                            SwapIfGreaterWithItems(keys, lo, hi - 1);
                            SwapIfGreaterWithItems(keys, lo, hi);
                            SwapIfGreaterWithItems(keys, hi - 1, hi);
                            return;
                        }

                        InsertionSort(keys, lo, hi);
                        return;
                    }

                    if (depthLimit == 0)
                    {
                        Heapsort(keys, lo, hi);
                        return;
                    }
                    depthLimit--;

                    int p = PickPivotAndPartition(keys, lo, hi);

                    // Note we've already partitioned around the pivot 
                    // and do not have to move the pivot again.
                    IntroSort(keys, p + 1, hi, depthLimit);
                    hi = p - 1;
                }
            }

            private static int PickPivotAndPartition(T[] keys, int lo, int hi)
            {
                Contract.Requires(keys != null);
                Contract.Requires(lo >= 0);
                Contract.Requires(hi > lo);
                Contract.Requires(hi < keys.Length);
                Contract.Ensures(Contract.Result<int>() >= lo && Contract.Result<int>() <= hi);

                // Compute median-of-three.  But also partition them.
                int middle = lo + ((hi - lo) / 2);

                // Sort lo, mid and hi appropriately, then pick mid as the pivot.
                SwapIfGreaterWithItems(keys, lo, middle);  // swap the low with the mid point
                SwapIfGreaterWithItems(keys, lo, hi);      // swap the low with the high
                SwapIfGreaterWithItems(keys, middle, hi);  // swap the middle with the high

                T pivot = keys[middle];
                Swap(keys, middle, hi - 1);

                // We already partitioned lo and hi and put the pivot in hi - 1.  
                // And we pre-increment & decrement below.
                int left = lo, right = hi - 1;

                while (left < right)
                {
                    if (pivot == null)
                    {
                        while (left < (hi - 1) && keys[++left] == null) ;
                        while (right > lo && keys[--right] != null) ;
                    }
                    else
                    {
                        while (pivot.CompareTo(keys[++left]) > 0) ;
                        while (pivot.CompareTo(keys[--right]) < 0) ;
                    }

                    if (left >= right)
                        break;

                    Swap(keys, left, right);
                }

                // Put pivot in the right location.
                Swap(keys, left, (hi - 1));

                return left;
            }

            private static void Heapsort(T[] keys, int lo, int hi)
            {
                Contract.Requires(keys != null);
                Contract.Requires(lo >= 0);
                Contract.Requires(hi > lo);
                Contract.Requires(hi < keys.Length);

                int n = hi - lo + 1;
                for (int i = n / 2; i >= 1; i = i - 1)
                {
                    DownHeap(keys, i, n, lo);
                }
                for (int i = n; i > 1; i = i - 1)
                {
                    Swap(keys, lo, lo + i - 1);
                    DownHeap(keys, 1, i - 1, lo);
                }
            }

            private static void DownHeap(T[] keys, int i, int n, int lo)
            {
                Contract.Requires(keys != null);
                Contract.Requires(lo >= 0);
                Contract.Requires(lo < keys.Length);

                T d = keys[lo + i - 1];
                int child;
                while (i <= n / 2)
                {
                    child = 2 * i;
                    if (child < n
                      && (keys[lo + child - 1] == null
                        || keys[lo + child - 1].CompareTo(keys[lo + child]) < 0))
                    {
                        child++;
                    }
                    if (keys[lo + child - 1] == null
                      || keys[lo + child - 1].CompareTo(d) < 0)
                        break;
                    keys[lo + i - 1] = keys[lo + child - 1];
                    i = child;
                }
                keys[lo + i - 1] = d;
            }

            private static void InsertionSort(T[] keys, int lo, int hi)
            {
                Contract.Requires(keys != null);
                Contract.Requires(lo >= 0);
                Contract.Requires(hi >= lo);
                Contract.Requires(hi <= keys.Length);

                int i, j;
                T t;
                for (i = lo; i < hi; i++)
                {
                    j = i;
                    t = keys[i + 1];
                    while (j >= lo && (t == null || t.CompareTo(keys[j]) < 0))
                    {
                        keys[j + 1] = keys[j];
                        j--;
                    }
                    keys[j + 1] = t;
                }
            }

            private static void SwapIfGreaterWithItems(T[] keys, int a, int b)
            {
                Contract.Requires(keys != null);
                Contract.Requires(0 <= a && a < keys.Length);
                Contract.Requires(0 <= b && b < keys.Length);

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

            private static void Swap(T[] a, int i, int j)
            {
                if (i != j)
                {
                    T t = a[i];
                    a[i] = a[j];
                    a[j] = t;
                }
            }

            private static int FloorLog2(int n)
            {
                int result = 0;
                while (n >= 1)
                {
                    result++;
                    n = n / 2;
                }
                return result;
            }
        }


    }
}
