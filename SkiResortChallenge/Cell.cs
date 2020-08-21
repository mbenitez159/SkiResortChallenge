using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortChallenge
{
    public class Cell
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
    }
}
