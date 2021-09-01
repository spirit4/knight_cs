using Assets.Scripts.Core;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
    public class Pathfinder
    {
        private List<PathNode> _grid = new List<PathNode>();
        private List<PathNode> _openList = new List<PathNode>();
        private List<PathNode> _closeList = new List<PathNode>();
        private List<int> _path;

        private int _currentIndex;
        private int _endIndex;
        private int _endX;
        private int _endY;

        private readonly int _sizeX;
        private readonly int _sizeY;

        private const int NORMAL_COST = 10;
        private const int DIAGONAL_COST = 10001;
        private const int WALL_COST = 10002;
        private const int DEAR_COST = 10003;

        public List<int> Path
        {
            get
            {
                return _path;
            }
        }

        public Pathfinder(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
        }

        public void Init(Tile[] grid)
        {
            _grid.Clear();
            for (int i = 0; i < grid.Length; i++)
            {
                _grid.Add(new PathNode());
                _grid[i].index = i;

                if (grid[i].isWall)
                {
                    _grid[i].cost = WALL_COST;
                }
                else if (grid[i].isDear)
                {
                    this._grid[i].cost = DEAR_COST;
                    //console.log("dear " + i + grid[i].isDear)
                }
            }
        }



        public void FindPath(int start, int end)
        {
            //console.log("findPath ", start, end);

            _path = new List<int>();

            _openList.Add(_grid[start]);
            _currentIndex = start;
            _endIndex = end;

            _endY = end / _sizeX;
            _endX = end - _endY * _sizeX;

            Iterate();
        }

        private void Iterate()
        {
            PathNode startNode = this._grid[this._currentIndex];
            int y0  = _currentIndex / _sizeX;
            int x0  = _currentIndex - y0 * _sizeY;

            int xMin  = ((x0 - 1) >= 0) ? (x0 - 1) : 0;
            int yMin  = ((y0 - 1) >= 0) ? (y0 - 1) : 0;

            int xMax  = ((x0 + 1) < _sizeX) ? (x0 + 1) : _sizeX - 1;
            int yMax  = ((y0 + 1) < _sizeY) ? (y0 + 1) : _sizeY - 1;

            int index;
            PathNode node ;
            for (int y  = yMin; y <= yMax; y++)
                {
                for (int x  = xMin; x <= xMax; x++)
                    {
                    index = y * _sizeX + x;
                    node = this._grid[index];
                    //console.log("----------------cost ", node.costToStart, node.costToEnd, index);
                    if (node.cost != WALL_COST && this._closeList.Contains(node) && ((x0 == x || y0 == y)))
                    {
                        if (this._openList.Contains(node))
                        {
                            this._openList.Add(node);
                            node.parent = startNode;
                            node.costToStart = startNode.costToStart + GetCost(x0, y0, x, y) + node.cost;
                            node.costToEnd = GetCostToEnd(x, y);
                            //console.log("----------------cost ", node.costToStart, node.costToEnd, this._currentIndex);
                            node.CalculateTotalCost();
                        }
                    }
                }
            }

            this.Check();
        }

        private void Check()
        {
            PathNode openNode;
            for (int i = 0; i < this._openList.Count; i++)
            {
                openNode = this._openList[i];
                if (this._openList.Contains(this._grid[this._endIndex]))
                {
                    _closeList.Add(this._grid[this._endIndex]);
                    _openList.Clear();

                    SetPath();

                    this._closeList.Clear(); //???

                    return;
                }
            }

            //console.log("before", this._openList);
            this._openList.Sort((node1, node2) => node1.totalCost - node2.totalCost);
            //console.log("after", this._openList);

            if (this._openList.Count == 0)
            {
                return;//a path don't exist
            }

            this._currentIndex = this._openList[0].index;
            this._closeList.Add(this._openList[0]);
            this._openList.RemoveAt(0);

            this.Iterate();
        }

        private void SetPath()
        {
            this._path.Clear();
            PathNode node = this._grid[this._endIndex];
            while (node.parent != null)
            {
                node = node.parent;
                this._path.Insert(0, node.index);
            }
        }

        private int GetCostToEnd(int x, int y)
        {
            int numberX = Math.Abs(x - this._endX);
            int numberY = Math.Abs(y - this._endY);

            return (numberX + numberY) * NORMAL_COST;
        }

        private int GetCost(int x0, int y0, int x, int y)
        {
            if (x0 != x && y0 != y)
                return DIAGONAL_COST;

            return NORMAL_COST;
        }


        public void Destroy()
        {
            this._grid.Clear();
            this._openList.Clear();
            this._path.Clear();
            this._closeList.Clear();

            this._grid = null;
            this._openList = null;
            this._path = null;
            this._closeList = null;
        }

}
}
