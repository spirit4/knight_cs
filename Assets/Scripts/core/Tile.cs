using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using System;
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

            this.objects.Add(dObject);

            //everything by default
            if (type != ImagesRes.GRASS && type != ImagesRes.WATER && type.IndexOf(ImagesRes.DECOR) == -1)
            {
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                dObject.GetComponent<SpriteRenderer>().sortingOrder = index;//TODO???
            }

            return dObject;
        }

        public void AddLocalCoordinates(int localX, int localY)
        {
            _localX = localX;
            _localY = localY;
        }

        private GameObject GetAlignedGameObject(string type, Component container)
        {
            GameObject dObject = new GameObject();
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(type);

            dObject.name = type;
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

                case string x when x.StartsWith(ImagesRes.PINE):
                case string y when y.StartsWith(ImagesRes.STONE):
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x - 0.03f, this.y + 0.07f, 0);
                    this.isWall = true;
                    break;

                case ImagesRes.STUMP:
                    dObject.isStatic = true;
                    this.isWall = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.06f, 0);
                    break;

                case ImagesRes.BRIDGE + "0":
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y, 0);
                    this.isWall = false;
                    break;
                case ImagesRes.BRIDGE + "1":
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x - 0.03f, this.y + 0.04f, 0);
                    this.isWall = false;
                    break;

                case ImagesRes.SPIKES + "0":
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.06f);
                    break;

                case ImagesRes.BOULDER:
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.13f);
                    break;
                case ImagesRes.TOWER:
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.16f);
                    break;

                case ImagesRes.EXIT:
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.13f);
                    break;

                case string y when y.StartsWith(ImagesRes.DECOR):
                    //dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    var point = GridUtils.GetUnityPoint(_localX, _localY);
                    //Debug.Log(type + "  is Decor x: " + point.x + "  y: " + point.y);

                    dObject.transform.localPosition = new Vector3(point.x - 0.15f, point.y + 0.19f);
                    break;

                default:
                    Debug.Log(type + " ==================== default in Tile ============================");
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.x, this.y);
                    break;
            }

            return dObject;
        }


        public void remove(string type)
        {
            int index = types.IndexOf(type);

            if (index != -1)
            {
                GameObject dObject = this.objects[index];

                this.objects.RemoveAt(index);
                this.types.RemoveAt(index);
                UnityEngine.Object.Destroy(dObject);
            }
        }

        public void AddType(string type)
        {
            int index = this.types.IndexOf(type);

            if (index == -1)
            {
                this.types.Add(type);
            }
        }

        public void AddObject(GameObject gameObject)
        {
            int index = this.objects.IndexOf(gameObject);

            if (index == -1)
            {
                this.objects.Add(gameObject);
            }
        }

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

        public GameObject getObject(string type)
        {
            int index = this.types.IndexOf(type);

            if (index != -1)
            {
                return this.objects[index];
            }

            Debug.Log("UNKNOWN TYPE:" + type);
            return null;
        }

        public bool isContainType(string type)
        {
            return types.Contains(type);
        }

        public bool isContainTypes(string type)
        {
            string str = String.Join(",", types);
            return str.Contains(type);
        }

        public string getConcreteType(string type)
        {

            if (isContainType(type + 0))
            {
                return type + 0;
            }
            else if (isContainType(type + 1))
            {
                return type + 1;
            }
            else if (isContainType(type + 2))
            {
                return type + 2;
            }
            else if (isContainType(type + 3))
            {
                return type + 3;
            }
            else
            {
                Debug.Log("[INCORRECT TYPE] getConcreteType" + type);
                return null;
            }
        }

        //public void clear()
        //{
        //    for (int i = 0; i < this.objects.Count; i++)
        //    {
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
