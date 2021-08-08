using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    class Level
    {
        private Model _model;
        private Component _container;

        //private _hero: Hero;
        //private _mill: MillPress;
        //private _units: { [index: number]: ICollidable; };
        //private _items: IActivatable[];

        /** <summary>Static Sprites</summary> */
        private List<GameObject> _tilesBg;
        /** <summary>Static Sprites</summary> */
        private List<GameObject> _decorBg;

        public Level(Component container, Model model)
        {
            this._container = container;
            this._model = model;

            //this._units = { };
            //this._items = [];
            this._tilesBg = new List<GameObject>();
            this._decorBg = new List<GameObject>();

            var cells = JSON.Parse(JSONRes.level0); ;// new ExpandoObject[10];
            // JSONRes.levels[Progress.currentLevel];

            int index;
            List<string> types;
            for (int i = 0; i < cells.Count; i++)
            {
                index = cells[i]["index"];

                types = (List<string>)cells[i]["types"];
                //Debug.Log(types);

                for (int j = 0; j < types.Count; j++)
                {
                    this.checkCell(index, types[j], types);
                }
            }
            Controller.instance.bg.addTiles(this._tilesBg);
            Controller.instance.bg.addTiles(this._decorBg, true);
            this._tilesBg = null;
            this._decorBg = null;

            //var len: number = this._items.length;
            //for (var i: number = 0; i < len; i++)
            //{
            //    this._items[i].init(i, this._model.grid, this._units);
            //}
        }

        private void checkCell(int index, string type, List<string> types)
        {
            GameObject bitmap;
            //GameObject sprite;
            //GameObject container;
            Tile[] grid = this._model.grid;

            switch (type)
            {
                //            case ImagesRes.HERO:
                //                this._hero = new Hero(index);
                //                this._container.addChild(this._hero);
                //                break;

                //            case ImagesRes.MILL:
                //                container = this._mill = new MillPress(type, index);
                //                this._container.addChild(container);
                //                grid[index].isDear = true;
                //                grid[index].addType(type);
                //                grid[index].objects.push(container);//dirty hack
                //                //console.log("[SDSDSDSDSDS: ]", index, type);
                //                break;

                //            case ImagesRes.STAR:
                //                sprite = < createjs.Sprite > grid[index].add(type, this._container, grid);

                //                var star: Star = new Star(sprite, index, type);
                //                break;

                //            case ImagesRes.TRAP:
                //                sprite = < createjs.Sprite > grid[index].add(type, this._container, grid);

                //                var trap: Trap = new Trap(sprite, index, type);
                //                break;

                //            case ImagesRes.BOULDER:
                //                var boulder: Boulder = new Boulder(type, index, grid[index].add(type, this._container, grid), grid[index]);
                //                this._items.push(boulder);
                //                break;

                //            case ImagesRes.TOWER:
                //                var tower: Tower = new Tower(type, index, grid[index].add(type, this._container, grid), grid[index]);
                //                this._units[index] = tower;
                //                this._items.push(tower);
                //                break;

                //            case ImagesRes.BOULDER_MARK:
                //            case ImagesRes.ARROW:
                //                grid[index].addType(type);//for set direction
                //                break;

                //            case ImagesRes.SPIKES + 0:
                //                var spikes: Spikes = new Spikes(type, index, grid[index].add(type, this._container, grid), grid[index]);
                //                this._items.push(spikes);
                //                break;

                //            case ImagesRes.DECOR + 0:
                //            case ImagesRes.DECOR + 1:
                //            case ImagesRes.DECOR + 2:
                //            case ImagesRes.DECOR + 3:
                //            case ImagesRes.DECOR + 4:
                //                bitmap = < createjs.Bitmap > grid[index].add(type, this._container, grid);
                //                bitmap.x = cell[type][0];
                //                bitmap.y = cell[type][1];
                //                this._decorBg.push(bitmap);

                //                break;

                //            case ImagesRes.MONSTER:
                //                var monster: Monster;
                //                var id: number = 0;
                //                for (var key in this._units)
                //                {
                //        monster = < Monster > this._units[key];
                //        if (monster.type == type && (monster.x == grid[index].x || monster.y == grid[index].y))
                //        {
                //            monster.setPointIndex2(index);
                //            return;
                //        }
                //        id++;
                //    }

                //    monster = new Monster(type, index, id);
                //                this._container.addChild(monster);

                //                this._units[index] = monster;
                //                this._items.push(monster);//to set index position
                //                break;

                case ImagesRes.GRASS:
                case ImagesRes.WATER:
                    bitmap = grid[index].add(type, this._container, grid);
                    this._tilesBg.Add(bitmap);
                    break;

                    //default:
                    //    grid[index].pus(type, this._container, grid);
            }

        }

        //public destroy(): void
        //{
        //    Core.instance.bg.removeTiles();

        //    var len: number = this._items.length;
        //    for (var i: number = 0; i < len; i++)
        //        {
        //        this._items[i].destroy();
        //    }

        //    this._model = null;
        //    this._container = null;
        //    this._hero = null;
        //    this._units = null;
        //    this._mill = null;
        //    this._items.length = 0;
        //    this._items = null;
        //}

        //public get hero(): Hero
        //{
        //    return this._hero;
        //}

        //public get mill(): MillPress
        //{
        //    return this._mill;
        //}

        //public get units(): {[index: number]: ICollidable; }
        //{
        //    return this._units;
        //}

        //public get items(): IActivatable[]
        //{
        //    return this._items;
        //}
    }
}
