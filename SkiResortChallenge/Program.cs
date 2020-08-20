using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] skiResortMatriz = new int[][]
           {
                new int[] { 4, 8, 7, -1  },
                new int[] { 2, 5, 9 ,0  },
                new int[] { 6, 3, 2, 1  },
                new int[] { 4, 4, 1, 6  },
           };
            var cell = SkiResortMatrix.FindLargestRoute(skiResortMatriz);
            Console.WriteLine($"Length of calculated path : {cell.Path.Count}");
            Console.WriteLine($"Drop of calculated path : {cell.PathDrop}");
            Console.WriteLine($"Calculated path  {string.Join(" - ", cell.Path)}");
            Console.ReadLine();
        }
    }
}
