using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public class PathNode
    {
        public int Cost = 0;
        public int CostToStart = 0;
        public int CostToEnd = 0;
        public int TotalCost = 0;

        public int Index = 0;

        public PathNode parent = null;

        public PathNode()
        {

        }
        public void CalculateTotalCost()
        {
            TotalCost = CostToStart + CostToEnd;
        }

    }
}
