using Assets.Scripts.Data;
using Assets.Scripts.Units;
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

        private List<Entity> _entities;
        private List<GameObject> _objects;//TODO remove it
        private List<string> _types;
        //private List<Entity.Type> _eTypes;

        private bool _isWall = true;//for pathfinder
        private bool _isExpensive = false;//for pathfinder for mill

        public Tile(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            Index = index;
            _entities = new List<Entity>();
            _objects = new List<GameObject>();
            _types = new List<string>();
            //_eTypes = new List<Entity.Type>();
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
            if (!_objects.Contains(gameObject))
                _objects.Add(gameObject);
        }

        public void AddEntity(Entity entity)//TODO keep only this and similar
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);
        }

        public Entity GetEntity(Entity.Type type)
        {
            return _entities.Find(entity => entity.Kind == type);
        }

        public Entity GetEntity(string type)//TODO make it enum
        {
            int index = _types.IndexOf(type);

            if (index != -1)
                return _entities[index];

            Debug.Log("UNKNOWN TYPE:" + type);
            return null;
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

        public bool IsContainType(Entity.Type type)
        {
            if (_entities.Find(entity => entity.Kind == type) != null)
                return true;

            return false;
        }

        public bool IsContainType(string type)
        {
            return _types.Contains(type);
        }

        public bool IsContainDirection()
        {
            if (_entities.Find(entity => entity is Direction) != null)
                return true;

            return false;
        }

        public bool IsContainTypes(string type)//TODO delete it
        {
            string str = String.Join(",", _types);
            return str.Contains(type);
        }
        public bool IsContainTypes(Entity.Type type)
        {
            string str = String.Join(",", _types);
            return str.Contains(type.ToString());
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
