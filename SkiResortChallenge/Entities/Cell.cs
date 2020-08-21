using System;
using System.Collections.Generic;

namespace SkiResortChallenge
{
    public class Cell : IComparable<Cell>
    {
        public Cell()
        {
            PathDrop = -1;
            Path = new List<int>();
        }
        public Cell(int pathDrop)
        {
            IsLoaded = true;
            PathDrop = pathDrop;
            Path = new List<int>() { pathDrop };
        }
        public bool IsLoaded { get; set; }
        public int PathDrop { get; set; }
        public List<int> Path { get; set; }

        public static bool operator >(Cell c1, Cell c2)
        {
            return c1.CompareTo(c2) == 1;
        }
        public static bool operator <(Cell c1, Cell c2)
        {
            return c1.CompareTo(c2) == -1;
        }
        public int CompareTo(Cell obj)
        {
            if (obj is null) return 1;
            var cell = obj as Cell;
            if (this.Path.Count == cell.Path.Count)
                return BrokeCellPathTie(this, cell);

            return this.Path.Count.CompareTo(cell.Path.Count);
        }
        private int BrokeCellPathTie(Cell c1, Cell c2)
        {
            int steepestCell = 1;
            for (int i = 0; i < c1.Path.Count - 1; i++)
            {
                int c1PathSteep = c1.Path[i] - c1.Path[i + 1];
                int c2PathSteep = c2.Path[i] - c2.Path[i + 1];
                if (c1PathSteep != c2PathSteep)
                {
                    return c1PathSteep.CompareTo(c2PathSteep);
                }
            }
            return steepestCell;
        }
    }
}
