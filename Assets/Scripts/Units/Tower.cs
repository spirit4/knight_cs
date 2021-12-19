using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Tower : Unit, IActivatable
    {
        //private _directionIndex: number;
        //private _grid: Tile[];
        //private _arrow: TowerArrow;

        //private _speedTime: number = 0;
        //private SPEED: number = 80;

        //private _count: number = 0;

        public Tower(string type, int index, GameObject view, Tile tile) : base(index, type, view, tile)
        {
            //constructor(type: string, index: number, view: createjs.DisplayObject, tile: Tile)
            //{
            //    super(index, type, view, tile);
            //    tile.isWall = true;
            //    view.x += 10;
            //    view.y -= 15;
        }

        //public init(i: number, grid: Tile[], units: { [index: number]: ICollidable; }): void
        //{
        //    this._grid = grid;
        //    this._directionIndex = Utils.findAround(grid, this.index, ImagesRes.ARROW);
        //    this._count = i;
        //    //console.log("checkInit", i);
        //    this.activate();
        //    units[this.index] = this._arrow;
        //}

        public void activate()
        {
        //    this._arrow = new TowerArrow(ImagesRes.ARROW, this.index);
        //    this._arrow.snapToPixel = true;
        //    this.view.parent.addChildAt(this._arrow, this.view.parent.getChildIndex(this.view) - 1);
        //    this._arrow.visible = false;

        //    //console.log("IOOOEIWIEIWE1", this._grid[0].x, this._grid[Config.WIDTH - 1].x);
        //    //console.log("IOOOEIWIEIWE2", this._grid[this.index].x / Config.SIZE_W, Utils.getPoint(this.index));

        //    createjs.Tween.get(this).wait(this._speedTime * 7 + 700 * this._count).call(this.shoot, [], this);
        }

        //private shoot(): void
        //{
        //    this._arrow.x = this.view.x + 3;
        //    this._arrow.y = this.view.y + 20;
        //    var localIndex: number = this._grid[this.index].x / Config.SIZE_W;

        //    this._arrow.visible = true;
        //    if (this.index > this._directionIndex)
        //    {
        //        this._speedTime = this.SPEED + localIndex * this.SPEED;
        //        this._arrow.shoot(-1, this._speedTime);
        //    }
        //    else
        //    {
        //        this._speedTime = (Config.WIDTH - localIndex) * this.SPEED;
        //        this._arrow.shoot(1, this._speedTime);
        //    }

        //    createjs.Tween.get(this).wait(this._speedTime * 7 + 700 * this._count).call(this.shoot, [], this);
        //}

        //public destroy(): void
        //{
        //    super.destroy();

        //    this._grid = null;
        //    this._arrow.destroy();
        //    this._arrow = null;
        //}
    }
}