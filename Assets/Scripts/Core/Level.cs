using Assets.Scripts.Data;
using Assets.Scripts.Units;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Level
    {
        private Model _model;
        private Component _container;

        private Hero _hero;
        private MillPress _mill;
        private Dictionary<int, ICollidable> _units;
        private List<IActivatable> _items;

        /** <summary>Static Sprites</summary> */
        private List<GameObject> _tilesBg;
        /** <summary>Static Sprites</summary> */
        private List<GameObject> _decorBg;

        public Level(Component container, Model model)
        {
            _container = container;
            _model = model;

            _units = new Dictionary<int, ICollidable>();
            _items = new List<IActivatable>();
            _tilesBg = new List<GameObject>();
            _decorBg = new List<GameObject>();

            var cells = JSON.Parse(JSONRes.Levels[Progress.CurrentLevel]);

            int index;
            List<string> types;
            for (int i = 0; i < cells.Count; i++)
            {
                index = cells[i]["index"];

                types = (List<string>)cells[i]["types"];
                //Debug.Log(types);

                for (int j = 0; j < types.Count; j++)
                {
                    CheckCell(index, types[j], types, cells[i]);
                }
            }
            Controller.Instance.bg.AddTiles(_tilesBg, _model.Grid);
            Controller.Instance.bg.AddTiles(_decorBg, _model.Grid, true);
            _tilesBg = null;
            _decorBg = null;

            int len = _items.Count;
            for (int i = 0; i < len; i++)
            {
                _items[i].Init(i, _model.Grid, _units);
            }
        }

        //node has local coordinates of decor
        private void CheckCell(int index, string type, List<string> types, JSONNode node)
        {
            GameObject gameObject;

            Tile[] grid = _model.Grid;

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
                    grid[index].IsDear = true;
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

                case string x when x.StartsWith(ImagesRes.DECOR):
                    grid[index].AddLocalCoordinates(node[type][0], node[type][1]);
                    gameObject = grid[index].Add(type, _container, grid);
                    _decorBg.Add(gameObject);
                    break;

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

                case ImagesRes.GRASS:
                case ImagesRes.WATER:
                    gameObject = grid[index].Add(type, _container, grid);
                    _tilesBg.Add(gameObject);
                    break;

                default:
                    grid[index].Add(type, _container, grid);
                    break;
            }

        }

        public void Destroy()
        {
            //    Core.instance.bg.removeTiles();

            //    var len number = _items.Length;
            //    for (int i = 0; i < len; i++)
            //        {
            //        _items[i].destroy();
            //    }
            foreach (ICollidable item in _items)
            {
                item.Destroy();
            }

            foreach (KeyValuePair<int, ICollidable> pair in _units)
            {
                pair.Value.Destroy();
            }

            //    _model = null;
            //    _container = null;
            //    _hero = null;
            //    _units = null;
            //    _mill = null;
            //    _items.Length = 0;
            //    _items = null;
        }

        public Hero Hero
        {
            get
            {
                return _hero;
            }

        }

        public MillPress Mill
        {
            get
            {
                return _mill;
            }

        }

        public Dictionary<int, ICollidable> Units
        {
            get
            {
                return _units;
            }
        }

        public List<IActivatable> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
