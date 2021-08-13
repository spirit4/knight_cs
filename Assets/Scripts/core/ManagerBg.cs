using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ManagerBg
    {
            //private string[]  = [
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

        //    private _cacheBDs: { [type string]GameObject } = { };
        private List<GameObject> _levelBitmaps;
        private List<GameObject> _decorBitmaps;

        private readonly Game _game;

        /** <summary>Game is a script of GameContainer (Sprite on Unity GameScene)</summary> */
        public ManagerBg(Game game)
        {
            this._game = game;
            //super();
            //this.snapToPixel = true;
            //this.mouseEnabled = false;
            //this.mouseChildren = false;
        }

        public void init()
        {
            //for (int i = 0; i < this._typeBDs.Length; i++)
            //{
            //   // this.addToCache(this._typeBDs[i]);
            //}

            ////this is gui //unessasery
        }

        //public addToCache(type string, res ?: HTMLImageElement): void
        //{
        //    //console.log("[addToCache]", type, res);
        //    var bd: HTMLImageElement;
        //    var bitmapGameObject

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

        //    var bitmapGameObject
        //    for (int i = 0; i < imgs.Length; i++)
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

        //public dispose(types string[]): void
        //{
        //    var bitmapGameObject
        //    for (int i = 0; i < types.Length; i++)
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


        /** <summary>Adding grass, water, decor to Background, Game is container</summary> Component containter,*/
        public void addTiles(List<GameObject> bitmaps, Tile[] grid, bool isDecor = false)
        {

            if (isDecor)
            {
                this._decorBitmaps = new List<GameObject> { };
            }
            else
            {
                this._levelBitmaps = new List<GameObject> { };
            }

            int waterIndex = 0;
            for (int i = 0; i < bitmaps.Count; i++)
            {
                bitmaps[i].GetComponent<SpriteRenderer>().sortingLayerName = "Back";
                if(!isDecor) //decor set in Tile
                {
                    bitmaps[i].transform.SetParent(_game.gameObject.transform);
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = i + 1;
                    bitmaps[i].transform.localPosition = new Vector3(grid[i].x, grid[i].y, 0);
                }

                //Debug.Log("TILE i: " + i + "   " + bitmaps[i].name);

                if (bitmaps[i].name == ImagesRes.WATER && i - Config.WIDTH >= 0 && bitmaps[i - Config.WIDTH].name != ImagesRes.WATER)
                {
                    waterIndex++;
                    // this.addChildAt(bitmaps[i], this.getChildIndex(bitmaps[i - Config.WIDTH]));
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = waterIndex + 1;
                }
                else if (bitmaps[i].name == ImagesRes.WATER && i - 1 >= 0)
                {
                    waterIndex++;
                    //this.addChildAt(bitmaps[i], this.getChildIndex(bitmaps[i - 1]));
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = waterIndex + 1;
                }
                //else
                //{
                //    Debug.Log("ManagerBg: " + i + "   " + bitmaps[i].name);
                    
                //}

                //Debug.Log("mbg: " + grid[i].x + "x: " + grid[i].y + "y " + bitmaps[i].name);
                //Debug.Log("mbg i: " + i +  "   name: " + bitmaps[i].name);
                if (isDecor)
                {
                    this._decorBitmaps.Add(bitmaps[i]);
                    //putting on the top of any grass/water
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = Config.WIDTH * Config.HEIGHT + 10;
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
        //    for (int i = 0; i < this._levelBitmaps.Length; i++)
        //        {
        //        this.removeChild(this._levelBitmaps[i]);
        //    }
        //    for (int i = 0; i < this._decorBitmaps.Length; i++)
        //        {
        //        this.removeChild(this._decorBitmaps[i]);
        //    }
        //    this._levelBitmaps.Length = 0;
        //    this._levelBitmaps = null;
        //    this._decorBitmaps.Length = 0;
        //    this._decorBitmaps = null;
        //    this.update();
        //}
    }
}
