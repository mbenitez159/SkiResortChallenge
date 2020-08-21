using SkiResortChallenge.Helper;
using System;
namespace SkiResortChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            /*****IMPORTANT******/
            /* Change your File Path in app.config */

            int[][] skiResortMatriz = new int[][]
           {
                new int[] { 4, 8, 7, -1  },
                new int[] { 2, 5, 9 ,0  },
                new int[] { 6, 3, 2, 1  },
                new int[] { 4, 4, 1, 6  },
           };
            var result1 = SkiResortMatrix.FindLargestRoute(skiResortMatriz);
            Console.WriteLine($"Length of calculated path : {result1.Path.Count}");
            Console.WriteLine($"Drop of calculated path : {result1.PathDrop}");
            Console.WriteLine($"Calculated path  {string.Join(" - ", result1.Path)}");

            Console.WriteLine("\n");

            var result2 = SkiResortMatrix.FindLargestRoute(Settings.Matrix_4_4);
            Console.WriteLine($"Length of calculated path : {result2.Path.Count}");
            Console.WriteLine($"Drop of calculated path : {result2.PathDrop}");
            Console.WriteLine($"Calculated path  {string.Join(" - ", result2.Path)}");


            Console.WriteLine("\n");

            var result3 = SkiResortMatrix.FindLargestRoute(Settings.Matrix_1000_1000);
            Console.WriteLine($"Length of calculated path : {result3.Path.Count}");
            Console.WriteLine($"Drop of calculated path : {result3.PathDrop}");
            Console.WriteLine($"Calculated path  {string.Join(" - ", result3.Path)}");

            Console.ReadLine();
        }
    }
}
