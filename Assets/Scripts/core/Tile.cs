using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Tile
    {

        public float X;
        public float Y;
        public int Index;

        private int _localX;
        private int _localY;

        private List<GameObject> _objects;
        private List<string> _types;

        private bool _isWall = true;//for pathfinder
        private bool _isDear = false;//for pathfinder  for mill

        public Tile(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            Index = index;
            _objects = new List<GameObject>();
            _types = new List<string>();
        }

        public GameObject Add(string type, Component container, Tile[] grid = null)
        {
            _types.Add(type);
            GameObject dObject = this.GetAlignedGameObject(type, container);

            _objects.Add(dObject);

            //everything by default
            if (type != ImagesRes.GRASS && type != ImagesRes.WATER && type.IndexOf(ImagesRes.DECOR) == -1)
            {
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                dObject.GetComponent<SpriteRenderer>().sortingOrder = Index;
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
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(type);

            dObject.name = type;
            //Debug.Log("+++add " + type + "  " + index + "  " + x + "  " + y);
            switch (type)
            {
                case ImagesRes.GRASS:
                    this.IsWall = false;
                    dObject.isStatic = true;
                    break;

                case ImagesRes.WATER:
                    dObject.isStatic = true;
                    break;

                case string x when x.StartsWith(ImagesRes.PINE):
                case string y when y.StartsWith(ImagesRes.STONE):
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X - 0.03f, this.Y + 0.07f, 0);
                    this.IsWall = true;
                    break;

                case ImagesRes.STUMP:
                    dObject.isStatic = true;
                    this.IsWall = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y + 0.06f, 0);
                    break;

                case ImagesRes.BRIDGE + "0":
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y, 0);
                    this.IsWall = false;
                    break;
                case ImagesRes.BRIDGE + "1":
                    dObject.isStatic = true;
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X - 0.03f, this.Y + 0.04f, 0);
                    this.IsWall = false;
                    break;

                case ImagesRes.SPIKES + "0":
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y + 0.06f);
                    break;

                case ImagesRes.BOULDER:
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y + 0.13f);
                    break;
                case ImagesRes.TOWER:
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y + 0.16f);
                    break;

                case ImagesRes.EXIT:
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y + 0.13f);
                    break;

                case string y when y.StartsWith(ImagesRes.DECOR):
                    dObject.transform.SetParent(container.gameObject.transform);
                    var point = GridUtils.GetUnityPoint(_localX, _localY);
                    dObject.transform.localPosition = new Vector3(point.x - 0.15f, point.y + 0.19f);
                    break;

                default:
                    Debug.Log(type + " ==================== default in Tile ============================");
                    dObject.transform.SetParent(container.gameObject.transform);
                    dObject.transform.localPosition = new Vector3(this.X, this.Y);
                    break;
            }

            return dObject;
        }


        public void Remove(string type)
        {
            int index = Types.IndexOf(type);

            if (index != -1)
            {
                GameObject dObject = this.Objects[index];

                _objects.RemoveAt(index);
                _types.RemoveAt(index);
                UnityEngine.Object.Destroy(dObject);
            }
        }

        public void AddType(string type)
        {
            int index = _types.IndexOf(type);

            if (index == -1)
            {
                _types.Add(type);
            }
        }

        public void AddObject(GameObject gameObject)
        {
            int index = _objects.IndexOf(gameObject);

            if (index == -1)
            {
                _objects.Add(gameObject);
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

        public GameObject GetObject(string type)
        {
            int index = _types.IndexOf(type);

            if (index != -1)
            {
                return _objects[index];
            }

            Debug.Log("UNKNOWN TYPE:" + type);
            return null;
        }

        public bool IsContainType(string type)
        {
            return _types.Contains(type);
        }

        public bool IsContainTypes(string type)
        {
            string str = String.Join(",", _types);
            return str.Contains(type);
        }

        public string GetConcreteType(string type)
        {

            if (IsContainType(type + 0))
            {
                return type + 0;
            }
            else if (IsContainType(type + 1))
            {
                return type + 1;
            }
            else if (IsContainType(type + 2))
            {
                return type + 2;
            }
            else if (IsContainType(type + 3))
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

        public List<GameObject> Objects { get => _objects; set => _objects = value; }
        public List<string> Types { get => _types; set => _types = value; }
        public bool IsWall { get => _isWall; set => _isWall = value; }
        public bool IsDear { get => _isDear; set => _isDear = value; }
    }
}
