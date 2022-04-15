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

        private List<GameObject> _objects;
        private List<string> _types;

        private bool _isWall = true;//for pathfinder
        private bool _isExpensive = false;//for pathfinder for mill

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

        private GameObject GetAlignedGameObject(string type, Component container)
        {
            GameObject dObject = new GameObject();
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(type);

            dObject.name = type;

            switch (type)
            {
                //case ImagesRes.GRASS:
                //    this.IsWall = false;
                //    dObject.isStatic = true;
                //    break;

                //case ImagesRes.WATER:
                //    dObject.isStatic = true; 
                //    break;

                //case string x when x.StartsWith(ImagesRes.PINE):
                //case string y when y.StartsWith(ImagesRes.STONE):
                //    dObject.isStatic = true;
                //    dObject.transform.SetParent(container.gameObject.transform);
                //    dObject.transform.localPosition = new Vector3(this.X - 0.03f, this.Y + 0.07f, 0);
                //    this.IsWall = true;
                //    break;

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

                //case string y when y.StartsWith(ImagesRes.DECOR):
                //    dObject.transform.SetParent(container.gameObject.transform);
                //    var point = GridUtils.GetUnityPoint(_localX, _localY);
                //    dObject.transform.localPosition = new Vector3(point.x - 0.15f, point.y + 0.19f);
                //    break;

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

        public List<GameObject> Objects { get => _objects; set => _objects = value; }
        public List<string> Types { get => _types; set => _types = value; }
        public bool IsWall { get => _isWall; set => _isWall = value; }
        public bool IsExpensive { get => _isExpensive; set => _isExpensive = value; }
    }
}
