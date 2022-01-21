using System;
using System.Collections.Generic;

namespace asd2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>(10);
            Random random = new Random();
            for (int i = 1; i <= 1000; i++)
            {
                tree.Insert(random.Next(-100, 100), i);
            }

            Console.WriteLine(tree);
            tree.Delete(1);
            Console.WriteLine(tree);


            tree.Insert(102, 2288);
            Console.WriteLine(tree);

            Console.WriteLine(tree.Search(78));

            Console.WriteLine(tree.SearchData(78));
        }
    }
}