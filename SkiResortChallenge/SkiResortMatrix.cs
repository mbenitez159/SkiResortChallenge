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
        // jagged arrays
        public static int[][] Matrix;
        public static Cell[][] CellMatrix;

        public static Cell FindLargestRoute(string filePath)
        {
            var matrix = FilePathToIntMatrix(filePath);
            return FindLargestRoute(matrix);
        }
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
                    largestPath = GetSteepestRoutePath(largestPath, CellMatrix[i][j]);
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

        private static Cell GetSteepestRoutePath(Cell c1, Cell c2)
        {
            if (c1.Path.Count == c2.Path.Count)
                return BrokeCellPathTie(c1, c2);

            return c1.Path.Count > c2.Path.Count ?
                        c1 : c2;
        }

        private static Cell BrokeCellPathTie(Cell c1, Cell c2)
        {
            Cell steepestCell = c1;
            for (int i = 0; i < c1.Path.Count - 1; i++)
            {
                int c1PathSteep = c1.Path[i] - c1.Path[i + 1];
                int c2PathSteep = c2.Path[i] - c2.Path[i + 1];
                if (c1PathSteep != c2PathSteep)
                {
                    return c1PathSteep > c2PathSteep ?
                        c1 : c2;
                }
            }
            return steepestCell;
        }
        #region Helper
        private static int[][] FilePathToIntMatrix(string filePath)
        {
            int size = 0;
            int[][] FileMatrix = new int[size][];
            string[] lines = GetFileLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                //0 is dimmension Line
                if (i != 0)
                {
                    FileMatrix[i - 1] = lines[i].Split(' ').Select(l => int.Parse(l)).ToArray();
                }
                else
                {
                    size = int.Parse(lines[i].Split(' ')?[0]);
                    FileMatrix = new int[size][];
                }
            }
            return FileMatrix;
        }
        private static string[] GetFileLines(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath);
        }
        #endregion
    }
}
