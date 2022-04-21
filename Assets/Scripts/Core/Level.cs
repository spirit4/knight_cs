using Assets.Scripts.Data;
using Assets.Scripts.Units;
using Assets.Scripts.Utils;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Level : IDestroyable
    {
        private Hero _hero;
        private List<ICollidable> _units; 
        private List<IActivatable> _items;

        private Creator _creator;

        public Hero Hero { get => _hero; }
        public List<ICollidable> Units { get => _units; }
        public List<IActivatable> Items { get => _items; }
        public Creator Creator { get => _creator; }

        public Level(Transform container, EntityConfig config)
        {
            _units = new List<ICollidable>();
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
                    digitInt = digitString == "" ? 0 : int.Parse(digitString);
                    generalType = digitString == "" ? types[j] : types[j].Remove(types[j].Length - 1);

                    Enum.TryParse(generalType, out entityType);
                    entity = _creator.GetTileObject(entityType, grid[index], digitInt);

                    grid[index].AddType(types[j]);//TODO  specific!!!?
                    grid[index].AddObject(entity.View);
                    grid[index].AddEntity(entity); //TODO clear 3 these

                    if (cells[i][types[j]] == null)
                        position = new Vector3(grid[index].X, grid[index].Y);//tile's coordinates
                    else
                        position = new Vector3(cells[i][types[j]][0], cells[i][types[j]][1]);//specified coordinates

                    if (entity is IActivatable)
                        _items.Add(entity as IActivatable);
                    else
                        entity.Deploy(container, position);
                }
            }

            ICollidable unit;
            Monster monster1;
            Monster monster2;
            TileObject item;
            for (int i = _items.Count - 1; i > -1; i--)
            {
                if (_items[i] is Monster)
                {
                    monster1 = _items[i] as Monster;

                    monster2 = _units.Find(monster2 => (monster2 is Monster) &&
                        (monster1.X == (monster2 as Monster).X || monster1.Y == (monster2 as Monster).Y)) as Monster;

                    if (monster2 != null)
                    {
                        monster2.Init(grid[monster1.Index]);
                        monster2.Deploy(container, new Vector3(monster2.X, monster2.Y));
                    }
                    else
                        _units.Add(monster1);

                    _items.RemoveAt(i);
                }
                else if (_items[i] is Hero)
                {
                    _hero = _items[i] as Hero;
                    _items.RemoveAt(i);
                    _hero.Deploy(container, new Vector3(grid[_hero.Index].X, grid[_hero.Index].Y));
                }
                else 
                {
                    unit = _items[i].Init(GridUtils.FindDirection(grid, (_items[i] as TileObject).Index));
                    if (unit != null)
                        _units.Add(unit);// adding arrow

                    item = _items[i] as TileObject;
                    item.Deploy(container, new Vector3(grid[item.Index].X, grid[item.Index].Y));
                }
            }
        }

        public void Destroy()
        {
            foreach (IDestroyable item in _items)
            {
                item.Destroy();
            }

            foreach (IDestroyable unit in _units)
            {
                unit.Destroy();
            }

            _creator.Destroy();
            _creator = null;
        }
    }
}
