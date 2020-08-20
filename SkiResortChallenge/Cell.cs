using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortChallenge
{
    public class Cell
    {
        public bool IsLoaded { get; set; }
        public int PathDrop { get; set; }
        public List<int> Path { get; set; }
    }
}
