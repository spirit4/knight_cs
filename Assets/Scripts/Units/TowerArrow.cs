using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    class TowerArrow : Unit
    {
        //private _directionX: number = 0;
        //private _directionY: number = 0;

        //private _grid: Tile[];
        //private _pontIndex1: number = 0;
        //private _pontIndex2: number = 0;

        //public id: number = -1;

        //constructor(type: string, index: number)
        //{
        //    super(index, type);
        //    this._grid = Core.instance.model.grid;

        //    this._pontIndex1 = index;

        //    var bd: HTMLImageElement = ImagesRes.getImage(type);
        //    this.view = new createjs.Bitmap(bd);
        //    this.view.snapToPixel = true;

        //    this.addChild(this.view);
        //}

        //public shoot(direction: number, speed: number): void
        //{
        //    if (direction == 1)
        //    {
        //        this.view.scaleX = -1;
        //        this.view.x = this.view.getBounds().width;
        //        createjs.Tween.get(this).to({ x: Config.STAGE_W + 110 }, speed, createjs.Ease.linear).call(this.endHandler, [], this);
        //    }
        //    else
        //    {
        //        createjs.Tween.get(this).to({ x: -150}, speed, createjs.Ease.linear).call(this.endHandler, [], this);
        //    }

        //}

        //private endHandler(): void
        //{
        //    this.visible = false;
        //}

        //public isShooted(): boolean
        //{
        //    return this.visible;
        //}

        //public destroy(): void
        //{
        //    super.destroy();

        //    this._grid = null;
        //}
    }
}