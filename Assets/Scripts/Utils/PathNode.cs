using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public class PathNode
    {
        public int cost = 0;
        public int costToStart = 0;
        public int costToEnd = 0;
        public int totalCost = 0;

        public int index = 0;

        public PathNode parent = null;

        public PathNode()
        {

        }
        public void CalculateTotalCost()
        {
            this.totalCost = this.costToStart + this.costToEnd;
        }

    }
}
