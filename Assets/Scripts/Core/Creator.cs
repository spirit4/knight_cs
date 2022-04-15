using Assets.Scripts.Data;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    //public class BuildingSpec
    //{
    //    public Type Class { get; private set; }
    //    public Action<object[]>[] Methods { get; private set; }

    //    public BuildingSpec(Type Class, Action<object[]>[] Methods)
    //    {
    //        this.Class = Class;
    //        this.Methods = Methods;
    //    }
    //}

    public class Creator : IDestroyable
    {


        public Dictionary<Entity.Type, Type> _factory = new Dictionary<Entity.Type, Type>
            {
                {Entity.Type.grass,  typeof(TileObject)},
                {Entity.Type.water,  typeof(Water) },
                {Entity.Type.Decor,  typeof(Decor) },
                {Entity.Type.target_0,  typeof(PathPoint) },
            };

        //TODO if method list repeat, use sterategy

        //private Dictionary<Entity.Type, Func<EntityInput, Entity>> _factory
        //    = new Dictionary<Entity.Type, Func<EntityInput, Entity>>()
        //{
        //    {Entity.Type.grass, (config) => new StaticObject(config)},
        //    {Entity.Type.water, (config) => new StaticObject(config)},
        //    {Entity.Type.Decor0, (config) => new Decor(config)},
        //    {Entity.Type.target_0, (config) => new PathPoint(config)},
        //};

        private EntityConfig _config;

        //private Stack<Card> _cards; //TODO make DontDestoryOnLoad  pool
        public Creator(EntityConfig config)
        {
            _config = config;

            //_factory = new Dictionary<Entity.Type, BuildingSpec>
            //{
            //    {Entity.Type.grass,  new BuildingSpec(typeof(StaticObject),new Action<object[]>[]{BindToTile}) },
            //    {Entity.Type.water,  new BuildingSpec(typeof(StaticObject),new Action<object[]>[]{BindToTile}) },
            //    {Entity.Type.target_0,  new BuildingSpec(typeof(PathPoint),new Action<object[]>[]{}) },
            //};
        }

        public TileObject GetTileObject(Entity.Type type, Tile tile, int spriteIndex)
        {
            if (!_factory.ContainsKey(type)) //TODO TEMP
                return null;

            ////Debug.Log($"GetEntity {type}    ");
            //Entity entity = _factory[type](_config.GetConfig(type));

            //entity.AddView(_config.GetConfig(type).Sprites);
            ////entity.BindToTile(tile);
            ///

            //Entity entity = Activator.CreateInstance(_factory[type].Class, _config.GetConfig(type)) as Entity;// ();
            //entity.AddView(_config.GetConfig(type).Sprites);

            //Action<object[]>[] methods = _factory[type].Methods;
            //foreach (var item in methods)
            //{
            //    item.Invoke(new object[] { entity, tile });
            //}
            TileObject entity = Activator.CreateInstance(_factory[type], _config.GetConfig(type)) as TileObject;
            entity.AddView(_config.GetConfig(type).Sprites, spriteIndex);
            entity.BindToTile(tile);

            return entity;
        }


        public Entity GetDefault(Entity.Type type)
        {
            Entity entity = Activator.CreateInstance(_factory[type], _config.GetConfig(type)) as Entity;
            entity.AddView(_config.GetConfig(type).Sprites);
            return entity;
        }

        public void Destroy()
        {
            _config = null;
            _factory.Clear();
            _factory = null;

            //foreach (var card in _cards)
            //{
            //    card.Destroy();
            //}
            //_cards.Clear();
            //_cards = null;
        }
    }
}
