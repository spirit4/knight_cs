using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils.Display
{
    public class TargetMark : MonoBehaviour
    {
        //private _centerGameObject
        //private _outerGameObject

        public TargetMark()
        {
            //    super();

            //    this.snapToPixel = true;
            //    this.visible = false;

            //    var bitmapGameObject
            //    var bd: HTMLImageElement;
            //    bd = ImagesRes.getImage(ImagesRes.TARGET_MARK + 0);
            //    bitmap = new createjs.Bitmap(bd);
            //    bitmap.snapToPixel = true;
            //    this.addChild(bitmap);
            //    bitmap.regX = bd.width >> 1;
            //    bitmap.regY = bd.height * 0.5 - 3;
            //    this._center = bitmap;

            //    bd = ImagesRes.getImage(ImagesRes.TARGET_MARK + 1);
            //    bitmap = new createjs.Bitmap(bd);
            //    bitmap.snapToPixel = true;
            //    this.addChild(bitmap);
            //    bitmap.regX = bd.width >> 1;
            //    bitmap.regY = bd.height * 0.5 - 3;
            //    this._outer = bitmap;

        }

        //public placeByTap(index: number) : void
        //{
        //    var grid: Tile[] = Core.instance.model.grid;
        //    this.x = grid[index].x + Config.SIZE_W * 0.5 + 2;
        //    this.y = grid[index].y + Config.SIZE_H * 0.5 + 3;

        //    this.visible = true;
        //    this._outer.scaleX = this._outer.scaleY = 0.9;
        //    this._center.alpha = 0;
        //    this._outer.alpha = 0;

        //    this.appear();
        // }

        //private appear() : void
        //{
        //    createjs.Tween.get(this._center).to({ alpha: 0.8 }, 80, createjs.Ease.quartIn);
        //createjs.Tween.get(this._outer).to({ alpha: 0.8, scaleX: 1, scaleY: 1 }, 80, createjs.Ease.quartIn).call(this.extend, [], this);
        //}

        //private extend() : void
        //{
        //    createjs.Tween.get(this._center).to({ scaleX: 1.1, scaleY: 1.1 }, 80, createjs.Ease.quartOut);
        //createjs.Tween.get(this._outer).to({ scaleX: 1.2, scaleY: 1.2 }, 80, createjs.Ease.quartOut).call(this.disappear, [], this);
        //}

        //private disappear() : void
        //{
        //    createjs.Tween.get(this._center).to({ alpha: 0, scaleX: 0.9, scaleY: 0.9 }, 120, createjs.Ease.quartIn);
        //createjs.Tween.get(this._outer).to({ alpha: 0, scaleX: 0.9, scaleY: 0.9 }, 120, createjs.Ease.quartIn).call(this.hide, [], this);
        //}

        //private hide() : void
        //{
        //    this.visible = false;
        //}

    }
}
