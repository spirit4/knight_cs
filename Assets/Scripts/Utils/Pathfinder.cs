using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
   public class Pathfinder
    {
        //        private _grid: PathNode[] = [];
        //    private _openList: PathNode[] = [];
        //    private _closeList: PathNode[] = [];
        //    private _path number[] = [];

        //    private _currentIndex number;
        //    private _endIndex number;
        //    private _endX number;
        //    private _endY number;

        //    private SIZE_X number;
        //    private SIZE_Y number;

        //    private NORMAL_COST number = 10;
        //    private DIAGONAL_COST number = 10001;
        //    private WALL_COST number = 10002;
        //    private DEAR_COST number = 10003;

        public Pathfinder()
        {

        }

        //        public init(sizeX number, sizeY number, grid: Tile[]) : void
        //    {
        //        this.SIZE_X = sizeX;
        //        this.SIZE_Y = sizeY;

        //        this._grid.Length = 0;
        //        for (int i = 0; i<grid.Length; i++)
        //        {
        //            this._grid.push(new PathNode());
        //            this._grid[i].index = i;

        //            if (grid[i].isWall)
        //            {
        //                this._grid[i].cost = this.WALL_COST;    
        //            } 
        //            else if (grid[i].isDear)
        //            {
        //                this._grid[i].cost = this.DEAR_COST;
        //                //console.log("dear " + i + grid[i].isDear)
        //            }
        //}
        //    }



        //    public findPath(start number, end number): void
        //{
        //    //console.log("findPath ", start, end);

        //    this._path = [];

        //    this._openList.push(this._grid[start]);
        //    this._currentIndex = start;
        //    this._endIndex = end;

        //    this._endY = Math.floor(end / this.SIZE_X);
        //    this._endX = end - this._endY * this.SIZE_X;

        //    this.iterate();
        //}

        //private iterate(): void
        //{
        //    var startNode: PathNode = this._grid[this._currentIndex];
        //    var y0 number = Math.floor(this._currentIndex / this.SIZE_X);
        //    var x0 number = this._currentIndex - y0 * this.SIZE_X;

        //    var xMin number = ((x0 - 1) >= 0) ? (x0 - 1) : 0;
        //    var yMin number = ((y0 - 1) >= 0) ? (y0 - 1) : 0;

        //    var xMax number = ((x0 + 1) < this.SIZE_X) ? (x0 + 1) : this.SIZE_X - 1;
        //    var yMax number = ((y0 + 1) < this.SIZE_Y) ? (y0 + 1) : this.SIZE_Y - 1;

        //    var index number = 0;
        //    var node: PathNode;
        //    for (var y number = yMin; y <= yMax; y++)
        //        {
        //        for (var x number = xMin; x <= xMax; x++)
        //            {
        //            index = y * this.SIZE_X + x;
        //            node = this._grid[index];
        //            //console.log("----------------cost ", node.costToStart, node.costToEnd, index);
        //            if (node.cost != this.WALL_COST && this._closeList.indexOf(node) == -1 && ((x0 == x || y0 == y)))
        //            {
        //                if (this._openList.indexOf(node) == -1)
        //                {
        //                    this._openList.push(node);
        //                    node.parent = startNode;
        //                    node.costToStart = startNode.costToStart + this.getCost(x0, y0, x, y) + node.cost;
        //                    node.costToEnd = this.getCostToEnd(x, y);
        //                    //console.log("----------------cost ", node.costToStart, node.costToEnd, this._currentIndex);
        //                    node.calculateTotalCost();
        //                }
        //                //else if (node.costToStart > startNode.cost + this.getCost(x0, y0, x, y))
        //                //{
        //                //    node.parentNode = startNode;
        //                //    node.weightFromStart = getWeightFromStart(startNode.weightFromStart, x0, y0, j, i);
        //                //    node.weightToFinish = getWeightToFinish(j, i);
        //                //}
        //            }
        //        }
        //    }

        //    this.check();
        //}

        //private check(): void
        //{
        //    var node: PathNode;
        //    var openNode: PathNode;
        //    for (int i = 0; i < this._openList.Length; i++)
        //        {
        //        openNode = this._openList[i];
        //        if (this._openList.indexOf(this._grid[this._endIndex]) != -1)
        //        {
        //            this._closeList.push(this._grid[this._endIndex]);
        //            this._openList.Length = 0;

        //            this.setPath();

        //            this._closeList.Length = 0;

        //            return;
        //        }
        //    }

        //    //console.log("before", this._openList);
        //    this._openList.sort((node1, node2) => node1.totalCost - node2.totalCost);
        //    //console.log("after", this._openList);

        //    if (this._openList.Length == 0)
        //    {
        //        return;//a path don't exist
        //    }

        //    this._currentIndex = this._openList[0].index;
        //    this._closeList.push(this._openList.shift());

        //    //if (_currentCell != -1)
        //    this.iterate();
        //}

        //private setPath(): void
        //{
        //    this._path.Length = 0;
        //    for (var node: PathNode = this._grid[this._endIndex]; node.parent; node = node.parent)
        //        {
        //        this._path.unshift(node.index);
        //    }
        //}

        //private getCostToEnd(x number, y number) number
        //{
        //    var  numberX number = Math.abs(x - this._endX);
        //    var  numberY number = Math.abs(y - this._endY);

        //    return ( numberX +  numberY) * this.NORMAL_COST;
        //}

        //private getCost(x0 number, y0 number, x number, y number) number
        //{
        //    if (x0 != x && y0 != y)
        //        return this.DIAGONAL_COST;

        //    return this.NORMAL_COST;
        //}

        //public get path(): number[]
        //{
        //    return this._path;
        //}

        //public destroy(): void
        //{
        //    this._grid.Length = 0;
        //    this._openList.Length = 0;
        //    this._path.Length = 0;
        //    this._closeList.Length = 0;

        //    this._grid = null;
        //    this._openList = null;
        //    this._path = null;
        //    this._closeList = null;
        //}

    }
}
