using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortChallenge
{
    public class SkiResortMatrix
    {
        //Square matrix
        public static int Size = 0;

        public static Cell FindLargestRoute(int[][] matrix)
        {
            SetMatrixSize(matrix);
            Cell[][] cellsMatrix = InizializateMatrizCellRoute();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!cellsMatrix[i][j].IsLoaded)
                    {
                        FindLongestRoute(new Coordinate(i,j), matrix, cellsMatrix);
                    }
                }
            }
            return new Cell();
        }
        private static void SetMatrixSize(int[][] matriz)
        {
            Size = matriz.GetLength(0);
        }
        private static Cell[][] InizializateMatrizCellRoute()
        {
            Cell[][] newArray = new Cell[Size][];
            for (int array1 = 0;
                 array1 < Size; array1++)
            {
                newArray[array1] = new Cell[Size];
            }
            return newArray;
        }
        private static Cell FindLongestRoute(Coordinate coordinate,
                                            int[][] matriz,
                                            Cell[][] cellsMatrix)
        {
            return new Cell();
        }

        private static Cell GetBestRoute(Cell cell, int pathDrop)
        {
            return new Cell();
        }


    }
}
