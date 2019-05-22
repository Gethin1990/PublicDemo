using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ParallelTest
{
    class Program
    {
        private static object obj = new object();
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            Parallel.For(0, 100, (i) => {
                lock (obj)
                {
                    Product product = new Product();
                    product.Name = "name" + i;
                    product.Category = "Category" + i;
                    product.SellPrice = i;
                    products.Add(product);
                }
            });
            Console.WriteLine(products.Count);
            Console.ReadLine();
        }
    }

    public class Product
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public int SellPrice { get; set; }
    }
}
