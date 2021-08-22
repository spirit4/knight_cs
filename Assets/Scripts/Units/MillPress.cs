using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class MillPress : Unit
    {
        //private _vane: createjs.Bitmap;
        //private _grid: Tile[];

        public MillPress(string type, int index, GameObject view) : base(index, type, view)
        {
            
            //    super(index, type);

            //    this.state = Unit.OFF;

            //    var bd: HTMLImageElement = ImagesRes.getImage(type);
            //    var bitmap:createjs.Bitmap = new createjs.Bitmap(bd);
            //    bitmap.snapToPixel = true;
            //    this.addChild(bitmap);
            //    this.view = bitmap;

            //    this._grid = Core.instance.model.grid;
            //    this.x = this._grid[index].x;
            //    this.y = this._grid[index].y;
            //    bitmap.x = 7;
            //    bitmap.y = -30;

            //    this.addMill();
            }

            //public activate(): void
            //{
            //    if (this.state != Unit.OFF)
            //    {
            //        return;
            //    }

            //    this.state = Unit.STARTED;

            //    this.scaleX = this.scaleY = 1.2;
            //    this.x = this._grid[this.index].x - 6;
            //    this.y = this._grid[this.index].y - 12;
            //    createjs.Tween.get(this).wait(300).call(this.comeback, [], this);
            //}

            //private comeback(): void
            //{
            //    this.scaleX = this.scaleY = 1;
            //    this.x = this._grid[this.index].x;
            //    this.y = this._grid[this.index].y;
            //}

            //private addMill(): void
            //{
            //    var bd: HTMLImageElement = <HTMLImageElement> ImagesRes.getImage(ImagesRes.MILL_VANE);
            //    this._vane = new createjs.Bitmap(bd);
            //    this._vane.snapToPixel = true;
            //    this._vane.x = 34;
            //    this._vane.y = -15;
            //    this._vane.rotation = 35;
            //    this._vane.regX = bd.width >> 1;
            //    this._vane.regY = bd.height >> 1;
            //    this.addChild(this._vane);
            //}

            //public startRotateMill(): void
            //{
            //    if (this.state == Unit.ON)
            //    {
            //        return;
            //    }

            //    AchController.instance.addParam(AchController.MILL_LAUNCHED);

            //    this.state = Unit.ON;
            //    this.rotateMill();
            //}

            //private rotateMill(): void
            //{
            //    this._vane.rotation = 35;
            //    createjs.Tween.get(this._vane).to({ rotation: 395 }, 1700, createjs.Ease.linear).call(this.rotateMill, [], this);
            //}

            //public destroy(): void
            //{
            //    super.destroy();
            //    createjs.Tween.removeTweens(this._vane);

            //    this._vane = null;
            //    this._grid = null;
            //}
        }
}