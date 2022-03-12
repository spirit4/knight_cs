using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Pathfinder
    {
        private List<PathNode> _grid = new List<PathNode>();
        private List<PathNode> _openList = new List<PathNode>();
        private List<PathNode> _closeList = new List<PathNode>();
        private List<int> _path = new List<int>();

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
                _grid[i].Index = i;

                if (grid[i].IsWall)
                {
                    _grid[i].Cost = WALL_COST;
                }
                else if (grid[i].IsDear)
                {
                    _grid[i].Cost = DEAR_COST;
                }
            }
        }



        public void FindPath(int start, int end)
        {
            _openList.Add(_grid[start]);
            _currentIndex = start;
            _endIndex = end;

            _endY = end / _sizeX;
            _endX = end - _endY * _sizeX;

            Iterate();
        }

        private void Iterate()
        {
            PathNode startNode = _grid[_currentIndex];
            int y0 = _currentIndex / _sizeX;
            int x0 = _currentIndex - y0 * _sizeY;

            int xMin = ((x0 - 1) >= 0) ? (x0 - 1) : 0;
            int yMin = ((y0 - 1) >= 0) ? (y0 - 1) : 0;

            int xMax = ((x0 + 1) < _sizeX) ? (x0 + 1) : _sizeX - 1;
            int yMax = ((y0 + 1) < _sizeY) ? (y0 + 1) : _sizeY - 1;

            int index;
            PathNode node;
            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    index = y * _sizeX + x;
                    node = _grid[index];

                    if (node.Cost != WALL_COST && !_closeList.Contains(node) && ((x0 == x || y0 == y)))
                    {
                        if (!_openList.Contains(node))
                        {
                            _openList.Add(node);
                            node.parent = startNode;
                            node.CostToStart = startNode.CostToStart + GetCost(x0, y0, x, y) + node.Cost;
                            node.CostToEnd = GetCostToEnd(x, y);

                            node.CalculateTotalCost();
                        }
                    }
                }
            }

            Check();
        }

        private void Check()
        {
            PathNode openNode;
            for (int i = 0; i < _openList.Count; i++)
            {
                openNode = _openList[i];

                if (_openList.Contains(_grid[_endIndex]))
                {
                    _closeList.Add(_grid[_endIndex]);
                    _openList.Clear();

                    SetPath();

                    _closeList.Clear(); //clear for the next search

                    return;
                }
            }

            _openList.Sort((node1, node2) => node1.TotalCost - node2.TotalCost);

            if (_openList.Count == 0)
            {
                return;//path doesn't exist
            }
            _currentIndex = _openList[0].Index;
            _closeList.Add(_openList[0]);
            _openList.RemoveAt(0);

            Iterate();
        }

        private void SetPath()
        {
            _path.Clear();
            PathNode node = _grid[_endIndex];
            do
            {
                _path.Insert(0, node.Index);
                node = node.parent;
            }
            while (node.parent != null);
        }

        private int GetCostToEnd(int x, int y)
        {
            int numberX = Math.Abs(x - _endX);
            int numberY = Math.Abs(y - _endY);

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
            _grid.Clear();
            _openList.Clear();
            _path.Clear();
            _closeList.Clear();

            _grid = null;
            _openList = null;
            _path = null;
            _closeList = null;
        }

    }
}
