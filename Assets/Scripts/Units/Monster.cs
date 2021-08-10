///using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Monster : Unit// : IActivatable
    {
        //private _directionX: number = 0;
        //private _directionY: number = 0;

        //private _grid: Tile[];
        //private _pontIndex1: number = 0;
        //private _pontIndex2: number = 0;

        //private SPEED: number = 1150;

        //public id: number = -1;

        //constructor(type: string, index: number, id: number)
        //{
        //    super(index, type);
        //    this._grid = Core.instance.model.grid;
        //    this.id = id;

        //    this._pontIndex1 = index;

        //    this.view = new createjs.Sprite(ImagesRes.A_MONSTER["atlas"], ImagesRes.A_MONSTER["animation"]);
        //    this.view.snapToPixel = true;
        //    this.mc.framerate = 26;

        //    this.view.regX = this.view.getBounds().width >> 1;
        //    this.view.regY = this.view.getBounds().height >> 1;

        //    this.x = this._grid[index].x;
        //    this.y = this._grid[index].y;
        //    this.view.x = Config.SIZE_W * 0.5 - 7;
        //    this.view.y = Config.SIZE_H * 0.5 - 7;

        //    this.addChild(this.view);
        //}

        //private move(x: number, y: number): void
        //{
        //    var time: number = 0;
        //    if (this._directionX == 1 || this._directionX == -1)
        //    {
        //        time = this.SPEED * Math.abs(this._pontIndex1 - this._pontIndex2);
        //    }
        //    else if (this._directionY == 1 || this._directionY == -1)
        //    {
        //        time = this.SPEED * Math.abs(this._pontIndex1 - this._pontIndex2) / Config.WIDTH;
        //    }

        //    createjs.Tween.get(this).to({ x: x, y: y }, time, createjs.Ease.linear).call(this.setDirection, [], this);
        //}

        //private setDirection(): void
        //{
        //    var directionIndex: number = (this.index == this._pontIndex1) ? this._pontIndex2 : this._pontIndex1;
        //    var tile: Tile = this._grid[directionIndex];
        //    if (tile.y == this.y && tile.x > this.x)
        //    {
        //        this._directionX = 1;
        //        this._directionY = 0;
        //    }
        //    else if (tile.y == this.y && tile.x < this.x)
        //    {
        //        this._directionX = -1;
        //        this._directionY = 0;
        //    }
        //    else if (tile.x == this.x && tile.y > this.y)
        //    {
        //        this._directionX = 0;
        //        this._directionY = 1;
        //    }
        //    else if (tile.x == this.x && tile.y < this.y)
        //    {
        //        this._directionX = 0;
        //        this._directionY = -1;
        //    }

        //    this.changeView();
        //    this.move(tile.x, tile.y);
        //    this.index = directionIndex;
        //}

        //private changeView(): void
        //{
        //    if (this._directionX == 1)
        //    {
        //        this.view.scaleX = -1;
        //    }
        //    else if (this._directionX == -1)
        //    {
        //        this.view.scaleX = 1;
        //    }

        //}

        //public setPointIndex2(index: number): void
        //{
        //    this._pontIndex2 = index;
        //    this.setDirection();
        //}

        //public activate(): void
        //{
        //     //empty
        //}

        //public init(): void
        //{
        //    if (this._directionX == 1 || this._directionY == 1)
        //    {
        //        this._grid[this._pontIndex2].setIndex(this);
        //    }
        //}

        //public destroy(): void
        //{
        //    super.destroy();

        //    this._grid = null;
        //}
    }
}