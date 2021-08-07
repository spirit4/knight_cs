
using Assets.Scripts.consts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.core
{
    public class ManagerBg
    {
        //    private _typeBDs: string[] = [
        //        ImagesRes.GAME_BG,
        //        ImagesRes.UI_LEVEL_BOARD,
        //        ImagesRes.LEVEL_SELECT_TITLE,
        //        ImagesRes.ACHIEVEMENS_TITLE,
        //        ImagesRes.LEVELS_BG,
        //        ImagesRes.VICTORY_BOTTOM,
        //        ImagesRes.VICTORY_TOP,
        //        ImagesRes.SPEAR_UI + '0',
        //        ImagesRes.SPEAR_UI + '1',
        //        ImagesRes.CREDITS_TITLE
        //    ];

        //    private _cacheBDs: { [type: string]: createjs.Bitmap; } = { };
        private List<GameObject> _levelBitmaps;
        private List<GameObject> _decorBitmaps;

        public ManagerBg()
        {
            //super();
            //this.snapToPixel = true;
            //this.mouseEnabled = false;
            //this.mouseChildren = false;
        }

        //public init(): void
        //{
        //    var bitmap: createjs.Bitmap;
        //    for (var i: number = 0; i < this._typeBDs.length; i++)
        //        {
        //        this.addToCache(this._typeBDs[i]);
        //    }
        //}

        //public addToCache(type: string, res ?: HTMLImageElement): void
        //{
        //    //console.log("[addToCache]", type, res);
        //    var bd: HTMLImageElement;
        //    var bitmap: createjs.Bitmap;

        //    if (!res)
        //    {
        //        bd = ImagesRes.getImage(type);
        //    }
        //    else
        //    {
        //        bd = res;
        //    }

        //    bitmap = new createjs.Bitmap(bd);
        //    bitmap.snapToPixel = true;
        //    this.addChild(bitmap);
        //    bitmap.visible = false;

        //    this._cacheBDs[type] = bitmap;
        //}

        ///**[{type, x, y}]*/
        //public addBitmaps(imgs: Object[]): void
        //{
        //    this.clear();

        //    var bitmap: createjs.Bitmap;
        //    for (var i: number = 0; i < imgs.length; i++)
        //        {
        //        bitmap = this._cacheBDs[imgs[i]['type']];
        //        bitmap.visible = true;
        //        this.addChild(bitmap);//order indexes
        //        bitmap.x = imgs[i]['x'];
        //        bitmap.y = imgs[i]['y'];
        //    }

        //    this.update();
        //}

        //private clear(): void
        //{
        //        for (var type in this._cacheBDs)
        //    {
        //        this._cacheBDs[type].visible = false;
        //    }

        //    this.update();
        //}

        //public dispose(types: string[]): void
        //{
        //    var bitmap: createjs.Bitmap;
        //    for (var i: number = 0; i < types.length; i++)
        //        {
        //        bitmap = this._cacheBDs[types[i]];
        //        this.removeChild(bitmap);
        //        delete this._cacheBDs[types[i]];
        //    }

        //    this.update();
        //}


        //public update(): void
        //{
        //    this.getStage().update();
        //}

        public void addTiles(Component containter, List<GameObject> bitmaps, bool isDecor = false)
        {
            float dx = Config.STAGE_W - Config.WIDTH * Config.SIZE_W >> 1;
            float dy = Config.MARGIN_TOP + Config.PADDING;
            if (isDecor)
            {
                this._decorBitmaps = new List<GameObject> { };
            }
            else
            {
                this._levelBitmaps = new List<GameObject> { };
            }
            
            for (int i = 0; i < 1; i++)
            {
                //if (bitmaps[i].name == ImagesRes.WATER && i - Config.WIDTH >= 0 && bitmaps[i - Config.WIDTH].name != ImagesRes.WATER)
                //{
                //    this.addChildAt(bitmaps[i], this.getChildIndex(bitmaps[i - Config.WIDTH]));
                //}
                //else if (bitmaps[i].name == ImagesRes.WATER && i - 1 >= 0)
                //{
                //    this.addChildAt(bitmaps[i], this.getChildIndex(bitmaps[i - 1]));
                //}
                //else
                //{
                //    this.addChild(bitmaps[i]);
                //}

                //bitmaps[i].transform.position.x += dx;
                //bitmaps[i].y += dy;



                // add a "SpriteRenderer" component to the newly created object
                bitmaps.Add(new GameObject());
                bitmaps[i].AddComponent<SpriteRenderer>();
                bitmaps[i].GetComponent<SpriteRenderer>().sprite = Game.sprite;
                bitmaps[i].name = "Test_Tile";
                bitmaps[i].isStatic = true;
                bitmaps[i].GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = 2;


                bitmaps[i].transform.SetParent(containter.gameObject.transform);
                bitmaps[i].transform.position = new Vector3(dx/100, dy/100, 0);
                Debug.Log("mbg: " + dx/100 + "x: " + dy/100 + "y " + bitmaps[i].GetComponent<SpriteRenderer>().sortingLayerName);
                Debug.Log("mbg: " + dx + "x: " + dy + "y ");
                if (isDecor)
                {
                    this._decorBitmaps.Add(bitmaps[i]);
                }
                else
                {
                    this._levelBitmaps.Add(bitmaps[i]);
                }

            }
            //    this.update();
        }

        //public removeTiles(): void
        //{
        //    for (var i: number = 0; i < this._levelBitmaps.length; i++)
        //        {
        //        this.removeChild(this._levelBitmaps[i]);
        //    }
        //    for (var i: number = 0; i < this._decorBitmaps.length; i++)
        //        {
        //        this.removeChild(this._decorBitmaps[i]);
        //    }
        //    this._levelBitmaps.length = 0;
        //    this._levelBitmaps = null;
        //    this._decorBitmaps.length = 0;
        //    this._decorBitmaps = null;
        //    this.update();
        //}
    }
}
