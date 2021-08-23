﻿using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Tile
    {

        public float x;
        public float y;
        public int index;

        private int _localX;
        private int _localY;

        public List<GameObject> objects;
        public List<string> types;

        public bool isWall = true;//for pathfinder
        public bool isDear = false;//for pathfinder  for mill

        public Tile(float x, float y, int index)
        {
            this.x = x;
            this.y = y;
            this.index = index;
            this.objects = new List<GameObject>();
            this.types = new List<string>();
        }

        public GameObject add(string type, Component container, Tile[] grid = null)
        {
            this.types.Add(type);
            GameObject dObject = this.GetAlignedGameObject(type, container);


            // Debug.Log("TILE type: " + type + "   index" + index);
            this.objects.Add(dObject);

            //Debug.Log("+++add  " + type + "   " + type.IndexOf(ImagesRes.DECOR));

            //everything by default
            if (type != ImagesRes.GRASS && type != ImagesRes.WATER && type.IndexOf(ImagesRes.DECOR) == -1)
            {
                //dObject.transform.SetParent(container.gameObject.transform);
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                dObject.GetComponent<SpriteRenderer>().sortingOrder = index;
                //dObject.transform.localPosition = new Vector3(x - 0.03f, y + 0.06f, 0);
            }

            return dObject;
        }

        public void AddLocalCoordinates (int localX, int localY)
        {
            _localX = localX;
            _localY = localY;
        }

        private GameObject GetAlignedGameObject(string type, Component container)
        {
            GameObject dObject = new GameObject();
            dObject.AddComponent<SpriteRenderer>();
            //Sprite bd = 
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(type);

            dObject.name = type;// + index; seems I need only water
            //Debug.Log("+++add " + type + "  " + index + "  " + x + "  " + y);
            switch (type)
            {
                case ImagesRes.GRASS:
                    this.isWall = false;
                    dObject.isStatic = true;
                    break;

                case ImagesRes.WATER:
                    dObject.isStatic = true;
                    break;

                //        case ImagesRes.TRAP:
                //            sprite = new createjs.Sprite(JSONRes.atlas1, ImagesRes.A_TRAP);
                //            sprite.framerate = 30;
                //            sprite.x = this.x + 6;
                //            sprite.y = this.y + 9;
                //            dObject = sprite;
                //            break;

                case string x when x.StartsWith(ImagesRes.PINE):
                case string y when y.StartsWith(ImagesRes.STONE):
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x - 0.03f, this.y + 0.07f, 0);
                    this.isWall = true;
                    //Debug.Log(type + " ================================================");
                    break;

                case ImagesRes.STUMP:
                    dObject.isStatic = true;
                    this.isWall = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.06f, 0);
                    //Debug.Log(type + " ================================================");
                    break;

                case string y when y.StartsWith(ImagesRes.BRIDGE + 0):
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y, 0);
                    //dObject.x = this.x + (Config.SIZE_W - bd.width >> 1);
                    //dObject.y = this.y + (Config.SIZE_H - bd.height >> 1);
                    this.isWall = false;
                    break;
                case string y when y.StartsWith(ImagesRes.BRIDGE + 1):
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x - 0.03f, this.y + 0.04f, 0);
                    //dObject.x = this.x + (Config.SIZE_W - bd.width >> 1);
                    //dObject.y = this.y - 10;
                    this.isWall = false;
                    break;

                //        case ImagesRes.SPIKES + 0:
                //            dObject = new createjs.Bitmap(bd);
                //            dObject.x = this.x + 2;
                //            dObject.y = this.y + 2;
                //            break;

                //        case ImagesRes.BOULDER:
                //        case ImagesRes.TOWER:
                //            dObject = new createjs.Bitmap(bd);
                //            dObject.x = this.x;
                //            dObject.y = this.y;
                //            break;

                //        case ImagesRes.EXIT:
                //            dObject = new createjs.Bitmap(bd);
                //            dObject.x = this.x + 3;
                //            dObject.y = this.y - 13;
                //            break;

                case string y when y.StartsWith(ImagesRes.DECOR):
                    //dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                        var point = GridUtils.GetUnityPoint(_localX, _localY);
                   //Debug.Log(type + "  is Decor x: " + point.x + "  y: " + point.y);

                    dObject.transform.localPosition = new Vector3(point.x - 0.15f, point.y + 0.19f, 0);
                    break;

                default:
                    Debug.Log(type + " ==================== default in Tile ============================");
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y, 0);
                    break;
            }

            //    dObject.snapToPixel = true;
            return dObject;
        }


        //public remove(type string): void
        //{
        //    var index number = this.types.indexOf(type);
        //    //console.log("test111", type, index, this.objects[index]);
        //    if (index != -1)
        //    {
        //        var dObject: createjs.DisplayObject = this.objects[index];
        //        dObject.parent.removeChild(dObject);
        //        this.objects.splice(index, 1);
        //        this.types.splice(index, 1);
        //    }
        //}

        //public addType(type string): void
        //{
        //    var index number = this.types.indexOf(type);

        //    if (index == -1)
        //    {
        //        this.types.push(type);
        //    }
        //}

        //public removeType(type string): void
        //{
        //    var index number = this.types.indexOf(type);

        //    if (index != -1)
        //    {
        //        this.types.splice(index, 1);
        //    }
        //}

        //public removeObject(dObject: createjs.DisplayObject): void
        //{
        //    var index number = this.objects.indexOf(dObject);

        //    if (index != -1)
        //    {
        //        this.objects.splice(index, 1);
        //    }
        //}

        //public getObject(type string): createjs.DisplayObject
        //    {
        //    var index number = this.types.indexOf(type);

        //    if (index != -1)
        //    {
        //        return this.objects[index];
        //    }

        //    alert("UNKNOWN TYPE:" + type);
        //    return null;
        //}

        //public isContainType(type string): boolean
        //{
        //    var index number = this.types.indexOf(type);
        //    if (index != -1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public isContainTypes(type string): boolean
        //{
        //    var index0 number = this.types.indexOf(type + 0);
        //    var index1 number = this.types.indexOf(type + 1);
        //    var index2 number = this.types.indexOf(type + 2);
        //    var index3 number = this.types.indexOf(type + 3);
        //    var index4 number = this.types.indexOf(type + 4);
        //    if (index0 != -1 || index1 != -1 || index2 != -1 || index3 != -1 || index4 != -1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public getConcreteType(type string) string
        //{
        //    var index0 number = this.types.indexOf(type + 0);
        //    var index1 number = this.types.indexOf(type + 1);
        //    var index2 number = this.types.indexOf(type + 2);
        //    var index3 number = this.types.indexOf(type + 3);
        //    if (index0 != -1)
        //    {
        //        return type + 0;
        //    }
        //    else if (index1 != -1)
        //    {
        //        return type + 1;
        //    }
        //    else if (index2 != -1)
        //    {
        //        return type + 2;
        //    }
        //    else if (index3 != -1)
        //    {
        //        return type + 3;
        //    }
        //    else
        //    {
        //        //console.log("[INCORRECT TYPE] getConcreteType", type);
        //    }
        //}

        //public clear(): void
        //{
        //    for (int i = 0; i < this.objects.Length; i++)
        //        {
        //        if (this.objects[i].parent)
        //        {
        //            this.objects[i].parent.removeChild(this.objects[i]);
        //        }
        //    }
        //    this.objects.Length = 0;
        //    this.types.Length = 0;
        //    this.isWall = true;
        //    this.isDear = false;
        //}

        //public getFirstIndex() number
        //{
        //    if (this.objects.Length == 0)
        //    {
        //        return -1;
        //    }

        //    return this.objects[0].parent.getChildIndex(this.objects[0]);
        //}


        //public getLastIndex(container: createjs.Container) number
        //{
        //    var grid: Tile[] = Core.instance.model.grid;
        //    var index number = this.index;
        //    var objects: createjs.DisplayObject[] = grid[index].objects;

        //    //console.log("[index000]: ", index, container.getChildIndex(objects[1]));
        //    while (container.getChildIndex(objects[1]) == -1)
        //    {
        //        index--;

        //        if (index == -1)
        //        {
        //            return 0;  //gui + grid
        //        }

        //        objects = grid[index].objects;
        //    }

        //    //console.log("[index1]: ", index, container.getChildIndex(objects[1]), objects.Length);
        //    return container.getChildIndex(objects[1]) + 1;
        //}

        //public getIndex(type string) number //-1 if no exist
        //{
        //    var arrayIndex number = this.types.indexOf(type);

        //    if (arrayIndex == -1)
        //        return -1;

        //    var container: createjs.Container = < createjs.Container > this.objects[arrayIndex].parent;
        //    return container.getChildIndex(this.objects[arrayIndex]);
        //}

        //public setIndex(dObject: createjs.Container, isHeroUnder: boolean = true): void
        //{
        //    //if (!dObject)
        //    //{
        //    //console.log("WTF dObject doesn't exist");
        //    //}

        //    var index number;
        //    //if (this.isContainTypes(ImagesRes.STAR) || this.isContainType(ImagesRes.EXIT))
        //    //{
        //    //    index = this.getIndex(ImagesRes.STAR);

        //    //    if (index == -1)
        //    //        index = this.getIndex(ImagesRes.DARK);

        //    //    if (index == -1)
        //    //    {
        //    //        index = this.getIndex(ImagesRes.EXIT);
        //    //        index +=2; //above the right
        //    //    }

        //    //    if (isHeroUnder)
        //    //        index--;

        //    // //console.log("[index1]: ", index);
        //    //    dObject.parent.addChildAt(dObject, index);

        //    //}
        //    //else
        //    //{
        //    index = this.getLastIndex(dObject.parent);

        //    //console.log("[index2]: ", index, dObject.parent);
        //    dObject.parent.addChildAt(dObject, index);
        //    //console.log("[index3]: ", dObject.parent.getChildIndex(dObject));
        //    //}

        //}


    }
}
