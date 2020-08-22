using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SkiResortChallenge
{
    public class SkiResortMatrix
    {
        public static int Size = 0;
        // jagged arrays
        public static int[][] Matrix;
        public static Cell[][] Cell;

        public static Cell FindLargestRoute(string filePath)
        {
            var matrix = FilePathToIntMatrix(filePath);
            return FindLargestRoute(matrix);
        }
        public static Cell FindLargestRoute(int[][] matrix)
        {
            Cell largestPath = new Cell();
            InitializateMatrix(matrix);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Cell[i][j].IsLoaded)
                        FindLongestRoute(new Coordinate(i, j));

                    largestPath = largestPath > Cell[i][j] ?
                        largestPath : Cell[i][j];
                }
            }
            return largestPath;
        }
        private static void InitializateMatrix(int[][] matrix)
        {
            Matrix = matrix;
            Size = Matrix.GetLength(0);
            InizializateMatrizCellRoute();
        }
        private static void InizializateMatrizCellRoute()
        {
            Cell = CreateMatrizCellRoute();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cell[i][j] = new Cell();
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

            if (Cell[cdt.X][cdt.Y].IsLoaded)
                return Cell[cdt.X][cdt.Y];

            var cellDirections = GetCellsDirections();

            CheckCellAllPosibleDirections(cdt, cellDirections);

            Cell[cdt.X][cdt.Y] = GetBestRoute(cellDirections, Matrix[cdt.X][cdt.Y]);

            return Cell[cdt.X][cdt.Y];
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
                directions["up"] = FindLongestRoute(new Coordinate(cdt.X, cdt.Y - 1));

            if (cdt.Y < (Size - 1) && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X][cdt.Y + 1]))
                directions["down"] = FindLongestRoute(new Coordinate(cdt.X, cdt.Y + 1));

            if (cdt.X > 0 && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X - 1][cdt.Y]))
                directions["left"] = FindLongestRoute(new Coordinate(cdt.X - 1, cdt.Y));

            if (cdt.X < (Size - 1) && (Matrix[cdt.X][cdt.Y] > Matrix[cdt.X + 1][cdt.Y]))
                directions["right"] = FindLongestRoute(new Coordinate(cdt.X + 1, cdt.Y));
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

        #region Helper
        private static int[][] FilePathToIntMatrix(string filePath)
        {
            int[][] FileMatrix = new int[0][];
            string[] lines = GetFileLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                //If Line is 0 => the dimmension Line
                if (i > 0)
                    FileMatrix[i - 1] = GetNumbersFromLine(lines[i]);
                else
                    FileMatrix = InitializateMatrixFromLineSize(lines[0]);
            }
            return FileMatrix;
        }
        private static string[] GetFileLines(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("Please provide a valid file path");
            return File.ReadAllLines(filePath);
        }
        private static int[] GetNumbersFromLine(string line)
        {
            return line.Split(' ')
                    .Select(l => int.Parse(l))
                    .ToArray();
        }

        private static int[][] InitializateMatrixFromLineSize(string line)
        {
            int size = GetSize(line);
            return new int[size][];
        }
        private static int GetSize(string line)
        {
            return int.Parse(line.Split(' ')?[0]);
        }

        #endregion
    }
}
