using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortChallenge
{
    public class SkiResortMatrix
    {
        public static int Size = 0;

        public static Cell FindLargestRoute(int[][] matriz)
        {
            return new Cell();
        }

        private static Cell GetBestRoute(Cell cell, int pathDrop)
        {
            return new Cell();
        }
        public static Cell[][] InizializateMatrizCellRoute()
        {
            Cell[][] newArray = new Cell[Size][];
            for (int array1 = 0;
                 array1 < Size; array1++)
            {
                newArray[array1] = new Cell[Size];
            }
            return newArray;
        }

    }
}
