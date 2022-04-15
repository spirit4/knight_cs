using Assets.Scripts.Data;
using Assets.Scripts.Units;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Level : IDestroyable
    {
        private Component _container;

        private Hero _hero;
        private MillPress _mill;
        private Dictionary<int, ICollidable> _units;
        private List<IActivatable> _items;

        /** <summary>Static Sprites</summary> */
        private List<GameObject> _tilesBg;
        /** <summary>Static Sprites</summary> */
        //private List<GameObject> _decorBg;

        private Creator _creator;

        public Hero Hero { get => _hero; }
        public MillPress Mill { get => _mill;  }
        public Dictionary<int, ICollidable> Units { get => _units; }
        public List<IActivatable> Items { get => _items;  }
        public Creator Creator { get => _creator; }

        public Level(Transform container, EntityConfig config)
        {
            _container = container;

            _units = new Dictionary<int, ICollidable>();
            _items = new List<IActivatable>();

            var cells = JSON.Parse(JSONRes.Levels[Progress.CurrentLevel]);
            _creator = new Creator(config);

            Tile[] grid = Model.Grid;
            Entity entity;
            Entity.Type entityType;

            string digitString;
            int digitInt;
            string generalType;

            int index;
            List<string> types;
            Vector3 position;
            for (int i = 0; i < cells.Count; i++)
            {
                index = cells[i]["index"];
                types = cells[i]["types"];

                for (int j = 0; j < types.Count; j++)
                {
                    digitString = Regex.Match(types[j], @"\d+").Value;
                    digitInt = digitString == "" ? 0 : Int32.Parse(digitString);
                    generalType = digitString == "" ? types[j] : types[j].Remove(types[j].Length - 1);

                    //Debug.Log($"-digitString {digitString} -- {digitInt} ---- {generalType}");


                    if (Enum.TryParse(generalType, out entityType))//TODO temp
                    {
                        entity = _creator.GetTileObject(entityType, grid[index], digitInt);
                        if(entity == null)
                        {
                            CheckCell(index, types[j]); //TEMP
                            continue;
                        }

                        grid[index].AddType(generalType);//TODO general or specific?
                        grid[index].AddObject(entity.View);
                        

                        if (cells[i][types[j]] == null)
                            position = new Vector3(grid[index].X, grid[index].Y);//tile's coordinates
                        else
                            position = new Vector3(cells[i][types[j]][0], cells[i][types[j]][1]);//specified coordinates

                        entity.Deploy(container, position);
                    }
                    else
                        CheckCell(index, types[j]);//, types, cells[i]);
                }
            }


            int len = _items.Count;
            for (int i = 0; i < len; i++)
            {
                _items[i].Init(i, grid, _units);
            }
        }

        //node has local coordinates of decor
        private void CheckCell(int index, string type)//, List<string> types, JSONNode node)
        {
            GameObject gameObject;

            Tile[] grid = Model.Grid;

            switch (type)
            {
                case ImagesRes.HERO:
                    var gObject = new GameObject("HeroContainer");
                    gameObject = GameObject.Instantiate(ImagesRes.Prefabs["Hero"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(gObject.transform);
                    gObject.transform.SetParent(_container.gameObject.transform);
                    _hero = new Hero(index, gameObject, gObject);
                    break;

                case ImagesRes.MILL:
                    gameObject = new GameObject();
                    _mill = new MillPress(type, index, gameObject, _container);
                    grid[index].IsExpensive = true;
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case string x when x.StartsWith(ImagesRes.STAR + 0): //helmet
                    gameObject = GameObject.Instantiate(ImagesRes.Prefabs["Helmet"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Star(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].X + 0.03f, grid[index].Y - 0.07f);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case string x when x.StartsWith(ImagesRes.STAR + 1): //shield
                    gameObject = GameObject.Instantiate(ImagesRes.Prefabs["Shield"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Star(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].X + 0.03f, grid[index].Y + 0.05f, 0);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case string x when x.StartsWith(ImagesRes.STAR + 2): //sword
                    gameObject = GameObject.Instantiate(ImagesRes.Prefabs["Sword"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Star(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].X + 0.01f, grid[index].Y + 0.15f, 0);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case ImagesRes.TRAP:
                    gameObject = GameObject.Instantiate(ImagesRes.Prefabs["Trap"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Trap(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].X, grid[index].Y + 0.03f);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case ImagesRes.BOULDER:
                    Boulder boulder = new Boulder(type, index, grid[index].Add(type, _container, grid), grid[index]);
                    _items.Add(boulder);
                    break;

                case ImagesRes.TOWER:
                    Tower tower = new Tower(type, index, grid[index].Add(type, _container, grid), grid[index], _container);
                    _items.Add(tower);
                    break;

                case ImagesRes.BOULDER_MARK:
                case ImagesRes.ARROW:
                    grid[index].AddType(type);//for setting the direction
                    break;

                case ImagesRes.SPIKES + "0":
                    Spikes spikes = new Spikes(type, index, grid[index].Add(type, _container, grid), grid[index]);
                    _items.Add(spikes);
                    break;

                //case string x when x.StartsWith(ImagesRes.DECOR):
                    //grid[index].AddLocalCoordinates(node[type][0], node[type][1]);
                    //gameObject = grid[index].Add(type, _container, grid);
                    //_decorBg.Add(gameObject);


                  //  break;

                case ImagesRes.MONSTER:
                    Monster monster;
                    int id = 0;

                    foreach (KeyValuePair<int, ICollidable> pair in _units)
                    {
                        if (pair.Value.Type == type)
                        {
                            monster = pair.Value as Monster;

                            if (monster.Type == type && (monster.X == grid[index].X || monster.Y == grid[index].Y))
                            {
                                monster.SetPointIndex2(index);
                                return;
                            }
                            id++;
                        }
                    }

                    GameObject view = GameObject.Instantiate(ImagesRes.Prefabs[ImagesRes.MONSTER_ANIMATION], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    monster = new Monster(type, index, id, view, _container);
                    _units.Add(index, monster);
                    break;

                //case ImagesRes.GRASS:
                //    break;
                //case ImagesRes.WATER:
                //    gameObject = grid[index].Add(type, _container, grid);
                //    _tilesBg.Add(gameObject);
                //    break;

                default:
                    Debug.Log(type + " ========= default in Level ============");
                    grid[index].Add(type, _container, grid);
                    break;
            }

        }

        public void Destroy()
        {
            foreach (IDestroyable item in _items)
            {
                item.Destroy();
            }

            foreach (KeyValuePair<int, ICollidable> pair in _units)
            {
                (pair.Value as IDestroyable).Destroy();
            }

            _creator.Destroy();
            _creator = null;
            _container = null;
        }
    }
}
