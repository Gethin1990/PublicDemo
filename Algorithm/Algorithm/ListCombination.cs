using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    public static class ListCombination
    {
        public static void Invoke()
        {
            Console.WriteLine("Hello World!");
            var abcString = "abcde";
            var charArray = abcString.ToCharArray();

            Processing(charArray, 0, charArray.Length);

            Console.ReadKey();
        }

        public static void Processing<T>(T obj, int begin, int end)
        {
            if (begin == end)
            {
                return;
            }
            else
            {

                if (obj is char[])
                {
                    var str = obj as char[];

                    Console.Write(new string(str) + ",");
                    for (int j = begin + 1; j < end; j++)
                    {
                        if (str[begin] == str[j] && begin != j) continue;
                        Swap(str, begin, j);
                        Processing(obj, begin + 1, end);
                        Swap(str, begin, j);
                    }
                }
            }
        }

        public static void Swap(char[] str, int i, int j)
        {
            char tmp = str[i];
            str[i] = str[j];
            str[j] = tmp;
        }
    }
}
