using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class GridUtils
    {

        public static int getIndex(float localX, float localY)
        {
            int cellX = (int)((localX + Config.SIZE_W / 2) / Config.SIZE_W);
            int cellY = (int)((localY - Config.SIZE_H / 2) / Config.SIZE_H);
            return -cellY * Config.WIDTH + cellX;
        }

        public static Vector2 GetUnityPoint(int x, int y)
        {
            float cellX = Config.SIZE_W * x / Config.PIXEL_SIZE;
            float cellY = -Config.SIZE_H * y / Config.PIXEL_SIZE;
            return new Vector2(cellX, cellY);
        }

        //public const string getPoint(i number) : createjs.Point
        //    {
        //        var cellY number = (Math.floor(i / Config.WIDTH)) * Config.SIZE_W;
        //    var cellX number = (i - Math.floor(i / Config.WIDTH) * Config.WIDTH) * Config.SIZE_H;
        //        return new createjs.Point(cellX, cellY);
        //    }

        //public static isNeighbours(index1 number, index2 number): boolean
        //{
        //    if (index1 + 1 == index2 ||
        //        index1 - 1 == index2 ||
        //        index1 + Config.WIDTH == index2 ||
        //        index1 - Config.WIDTH == index2)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public static findAround(grid: Tile[], index number, type string) number
        //{
        //    if (index + 1 < Config.WIDTH * Config.HEIGHT && grid[index + 1].isContainType(type))
        //    {
        //        return index + 1;
        //    }
        //    else if (index - 1 >= 0 && grid[index - 1].isContainType(type))
        //    {
        //        return index - 1;
        //    }
        //    else if (index + Config.WIDTH < Config.WIDTH * Config.HEIGHT && grid[index + Config.WIDTH].isContainType(type))
        //    {
        //        return index + Config.WIDTH;
        //    }
        //    else if (index - Config.WIDTH >= 0 && grid[index - Config.WIDTH].isContainType(type))
        //    {
        //        return index - Config.WIDTH;
        //    }

        //    return -1;
        //}

        //public static addBitmap(x number, y number, type string, container: createjs.Container, mouseEnabled ?: boolean, rot ? number, isRegCenter ?: boolean): void;
        //public static addBitmap(x number, y number, bd: HTMLImageElement, container: createjs.Container, mouseEnabled ?: boolean, rot ? number, isRegCenter ?: boolean): void;

        //public static addBitmap(x number, y number, value: any, container: createjs.Container, mouseEnabled: boolean = false, rot number = 0, isRegCenter: boolean = false): void
        //{
        //    var bitmapGameObject
        //    var bd: HTMLImageElement;
        //    if (value && typeof value == "string")
        //    {
        //        bd = ImagesRes.getImage(value);
        //        bitmap = new createjs.Bitmap(bd);
        //    }
        //    else if (value && value instanceof HTMLImageElement)
        //        {
        //        bd = < HTMLImageElement > value;
        //        bitmap = new createjs.Bitmap(bd);
        //    }
        //        else
        //    {
        //        alert("WTF addBitmap");
        //    }
        //    bitmap.snapToPixel = true;
        //    bitmap.mouseEnabled = mouseEnabled;
        //    container.addChild(bitmap);
        //    bitmap.x = x;
        //    bitmap.y = y;
        //    bitmap.rotation = rot;

        //    if (isRegCenter)
        //    {
        //        bitmap.regX = bd.width >> 1;
        //        bitmap.regY = bd.height >> 1;
        //    }
        //}

        //public static getLength(object:Object) number
        //{
        //    var count number = 0;
        //        for (var i in object)
        //    {
        //        count++;
        //    }
        //    return count;
        //}


    }

}
