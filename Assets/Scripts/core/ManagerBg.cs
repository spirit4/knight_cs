using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ManagerBg
    {
        private List<GameObject> _levelBitmaps;
        private List<GameObject> _decorBitmaps;

        private readonly GameController _game;

        /** <summary>Game is a script of GameContainer (Sprite on Unity GameScene)</summary> */
        public ManagerBg(GameController game)
        {
            _game = game;
        }

        /** <summary>Adding grass, water, decor to Background, Game is container</summary>*/
        public void AddTiles(List<GameObject> bitmaps, Tile[] grid, bool isDecor = false)
        {

            if (isDecor)
                _decorBitmaps = new List<GameObject> { };
            else
                _levelBitmaps = new List<GameObject> { };


            int waterIndex = 0;
            for (int i = 0; i < bitmaps.Count; i++)
            {
                bitmaps[i].GetComponent<SpriteRenderer>().sortingLayerName = "Back";
                if (!isDecor) //decor set in Tile
                {
                    bitmaps[i].transform.SetParent(_game.gameObject.transform);
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = i + 1;
                    bitmaps[i].transform.localPosition = new Vector3(grid[i].X, grid[i].Y, 0);
                }

                if (bitmaps[i].name == ImagesRes.WATER && i - Config.WIDTH >= 0 && bitmaps[i - Config.WIDTH].name != ImagesRes.WATER)
                {
                    waterIndex++;
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = waterIndex + 1;
                }
                else if (bitmaps[i].name == ImagesRes.WATER && i - 1 >= 0)
                {
                    waterIndex++;
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = waterIndex + 1;
                }

                if (isDecor)
                {
                    _decorBitmaps.Add(bitmaps[i]);
                    //putting on the top of any grass/water
                    bitmaps[i].GetComponent<SpriteRenderer>().sortingOrder = Config.WIDTH * Config.HEIGHT + 10;
                }
                else
                {
                    _levelBitmaps.Add(bitmaps[i]);
                }
            }
        }

    }
}
