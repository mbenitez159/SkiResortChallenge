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
        public static int[][] Matrix;
        public static Cell[][] CellMatrix;

        public static Cell FindLargestRoute(int[][] matrix)
        {
            Cell largestPath = new Cell();
            SetMatrixSize(matrix);
            Matrix = matrix;
            InizializateMatrizCellRoute();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!CellMatrix[i][j].IsLoaded)
                        FindLongestRoute(new Coordinate(i, j));

                    largestPath = CellMatrix[i][j].Path.Count > largestPath.Path.Count ?
                        CellMatrix[i][j] : largestPath;
                }
            }
            return largestPath;
        }
        private static void SetMatrixSize(int[][] matriz)
        {
            Size = matriz.GetLength(0);
        }
        private static void InizializateMatrizCellRoute()
        {
            CellMatrix = CreateMatrizCellRoute();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    CellMatrix[i][j] = new Cell();
                }
            }
        }
        private static Cell[][] CreateMatrizCellRoute()
        {
            Cell[][] newArray = new Cell[Size][];
            for (int array1 = 0;
                 array1 < Size; array1++)
            {
                newArray[array1] = new Cell[Size];
            }
            return newArray;
        }
        private static Cell FindLongestRoute(Coordinate cdt)
        {
            //avoid coordinate out of boundaries
            if (cdt.X < 0 || cdt.X >= Size || cdt.Y < 0 || cdt.Y >= Size)
                return new Cell();

            if (CellMatrix[cdt.X][cdt.Y].IsLoaded)
                return CellMatrix[cdt.X][cdt.Y];

            var cellDirections = GetCellsDirections();

            CheckCellAllPosibleDirections(cdt, cellDirections);

            CellMatrix[cdt.X][cdt.Y] = GetBestRoute(cellDirections, Matrix[cdt.X][cdt.Y]);

            return CellMatrix[cdt.X][cdt.Y];
        }

        private static Dictionary<string, Cell> GetCellsDirections()
        {
            return new Dictionary<string, Cell>
            {
                { "up", new Cell() },
                { "down", new Cell() },
                { "left", new Cell() },
                { "right", new Cell() },
            };
        }

        private static void CheckCellAllPosibleDirections(Coordinate cdt, Dictionary<string, Cell> directions)
        {
            if (cdt.Y > 0 && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X][cdt.Y - 1]))
            {
                directions["up"] = CellMatrix[cdt.X][cdt.Y] = FindLongestRoute(new Coordinate(cdt.X, cdt.Y - 1));
            }

            if (cdt.Y < Size - 1 && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X][cdt.Y + 1]))
            {
                directions["down"] = CellMatrix[cdt.X][cdt.Y] = FindLongestRoute(new Coordinate(cdt.X, cdt.Y + 1));
            }

            if (cdt.X > 0 && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X - 1][cdt.Y]))
            {
                directions["left"] = CellMatrix[cdt.X][cdt.Y] = FindLongestRoute(new Coordinate(cdt.X - 1, cdt.Y));
            }

            if (cdt.X < Size - 1 && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X + 1][cdt.Y]))
            {
                directions["right"] = CellMatrix[cdt.X][cdt.Y] = FindLongestRoute(new Coordinate(cdt.X + 1, cdt.Y));
            }
        }

        private static Cell GetBestRoute(Dictionary<string, Cell> directions, int pathDrop)
        {
            var bestRoute = directions
                .Where(c => c.Value.Path.Count > 0)
                .OrderByDescending(c => c.Value.Path.Count)
                .Select(c => (KeyValuePair<string, Cell>?)c)
                .FirstOrDefault();

            if (bestRoute?.Value is null) return new Cell(pathDrop);

            var newCell = new Cell(pathDrop);
            bestRoute?.Value.Path.ForEach(p => newCell.Path.Add(p));
            return newCell;
        }


    }
}
