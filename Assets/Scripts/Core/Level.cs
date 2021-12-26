﻿using Assets.Scripts.Data;
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
            this._container = container;
            this._model = model;

            this._units = new Dictionary<int, ICollidable>();
            this._items = new List<IActivatable>();
            this._tilesBg = new List<GameObject>();
            this._decorBg = new List<GameObject>();

            var cells = JSON.Parse(JSONRes.levels[Progress.currentLevel]);

            int index;
            List<string> types;
            for (int i = 0; i < cells.Count; i++)
            {
                index = cells[i]["index"];

                types = (List<string>)cells[i]["types"];
                //Debug.Log(types);

                for (int j = 0; j < types.Count; j++)
                {
                    this.checkCell(index, types[j], types, cells[i]);
                }
            }
            Controller.instance.bg.addTiles(_tilesBg, _model.grid);
            Controller.instance.bg.addTiles(_decorBg, _model.grid, true);
            this._tilesBg = null;
            this._decorBg = null;

            int len = _items.Count;
            for (int i = 0; i < len; i++)
            {
                _items[i].init(i, _model.grid, _units);
            }
        }

        //node has local coordinates of decor
        private void checkCell(int index, string type, List<string> types, JSONNode node)
        {
            GameObject gameObject;

            Tile[] grid = _model.grid;

            switch (type)
            {
                case ImagesRes.HERO:
                    var gObject = new GameObject("HeroContainer");
                    gameObject = GameObject.Instantiate(ImagesRes.prefabs["Hero"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(gObject.transform);
                    gObject.transform.SetParent(_container.gameObject.transform);
                    _hero = new Hero(index, gameObject, gObject);
                    break;

                case ImagesRes.MILL:
                    gameObject = new GameObject();
                    _mill = new MillPress(type, index, gameObject, _container);
                    grid[index].isDear = true;
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                //TODO fix animations pivots all 3
                case string x when x.StartsWith(ImagesRes.STAR + 0): //helmet
                    gameObject = GameObject.Instantiate(ImagesRes.prefabs["Helmet"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Star(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].x + 0.04f, grid[index].y - 0.07f);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case string x when x.StartsWith(ImagesRes.STAR + 1): //shield
                    gameObject = GameObject.Instantiate(ImagesRes.prefabs["Shield"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Star(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].x + 0.03f, grid[index].y + 0.05f, 0);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case string x when x.StartsWith(ImagesRes.STAR + 2): //sword
                    gameObject = GameObject.Instantiate(ImagesRes.prefabs["Sword"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Star(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].x + 0.01f, grid[index].y + 0.15f, 0);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case ImagesRes.TRAP:
                    //                sprite = < createjs.Sprite > grid[index].add(type, this._container, grid);
                    //    dObject.transform.SetParent(container.gameObject.transform);
                    //    dObject.transform.localPosition = new Vector3(this.x, this.y + 0.06f);
                    //Trap trap = new Trap(type, index, grid[index].add(type, this._container, grid));

                    gameObject = GameObject.Instantiate(ImagesRes.prefabs["Trap"], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    gameObject.transform.SetParent(_container.gameObject.transform);
                    new Trap(type, index, gameObject);
                    gameObject.transform.localPosition = new Vector3(grid[index].x, grid[index].y + 0.03f);
                    grid[index].AddType(type);
                    grid[index].AddObject(gameObject);
                    break;

                case ImagesRes.BOULDER:
                    Boulder boulder = new Boulder(type, index, grid[index].add(type, this._container, grid), grid[index]);
                    _items.Add(boulder);
                    break;

                case ImagesRes.TOWER:
                    Tower tower = new Tower(type, index, grid[index].add(type, this._container, grid), grid[index], _container);
                    //_units[index] = tower;
                    _items.Add(tower);
                    break;

                case ImagesRes.BOULDER_MARK:
                case ImagesRes.ARROW:
                    grid[index].AddType(type);//for setting the direction
                    break;

                case ImagesRes.SPIKES + "0":
                    Spikes spikes = new Spikes(type, index, grid[index].add(type, this._container, grid), grid[index]);
                    _items.Add(spikes);
                    break;

                case string x when x.StartsWith(ImagesRes.DECOR):
                    grid[index].AddLocalCoordinates(node[type][0], node[type][1]);
                    gameObject = grid[index].add(type, this._container, grid);
                    this._decorBg.Add(gameObject);
                    break;

                case ImagesRes.MONSTER:
                    Monster monster;
                    int id = 0;

                    foreach (KeyValuePair<int, ICollidable> pair in _units)
                    {
                        //Debug.Log("[case ImagesRes.MONSTER2: ]" + pair.Value.type + pair.Key);
                        if (pair.Value.type == type)
                        {
                            monster = pair.Value as Monster;

                            if (monster.type == type && (monster.x == grid[index].x || monster.y == grid[index].y))
                            {
                                monster.setPointIndex2(index);
                                return;
                            }
                            id++;
                        }
                    }

                    GameObject view = GameObject.Instantiate(ImagesRes.prefabs[ImagesRes.MONSTER_ANIMATION], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    monster = new Monster(type, index, id, view, this._container);

                    this._units.Add(index, monster);
                    //this._items.Add(monster);//to set index position ?
                    //TODO it's in Game Update
                    break;

                case ImagesRes.GRASS:
                case ImagesRes.WATER:
                    gameObject = grid[index].add(type, this._container, grid);
                    this._tilesBg.Add(gameObject);
                    break;

                default:
                    grid[index].add(type, this._container, grid);
                    break;
            }

        }

        public void destroy()
        {
            //    Core.instance.bg.removeTiles();

            //    var len number = this._items.Length;
            //    for (int i = 0; i < len; i++)
            //        {
            //        this._items[i].destroy();
            //    }
            foreach (ICollidable item in _items)
            {
                item.destroy();
            }

            foreach (KeyValuePair<int, ICollidable> pair in _units)
            {
                pair.Value.destroy();
            }

            //    this._model = null;
            //    this._container = null;
            //    this._hero = null;
            //    this._units = null;
            //    this._mill = null;
            //    this._items.Length = 0;
            //    this._items = null;
        }

        public Hero hero
        {
            get
            {
                return this._hero;
            }

        }

        public MillPress mill
        {
            get
            {
                return _mill;
            }

        }

        //private Dictionary<int, ICollidable> _units;
        //public get units(): {[index number]: ICollidable; }
        //{
        //    return this._units;
        //}

        public Dictionary<int, ICollidable> units
        {
            get
            {
                return _units;
            }
        }

        public List<IActivatable> items
        {
            get
            {
                return _items;
            }
        }
    }
}
