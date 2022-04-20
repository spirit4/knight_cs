using Assets.Scripts.Data;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Creator : IDestroyable
    {

        /// <summary>
        /// Naming is such, cause this is how it was serialized in JSON levels once ago
        /// </summary>
        public Dictionary<Entity.Type, Type> _factory = new Dictionary<Entity.Type, Type>
            {
                {Entity.Type.grass,  typeof(TileObject)},
                {Entity.Type.water,  typeof(Water) },
                {Entity.Type.Decor,  typeof(Decor) },
                {Entity.Type.target_0,  typeof(PathPoint) },
                {Entity.Type.pine,  typeof(StaticObject) },
                {Entity.Type.stone,  typeof(StaticObject) },
                {Entity.Type.stump,  typeof(StaticObject) },
                {Entity.Type.brige,  typeof(StaticObject) },
                {Entity.Type.exit,  typeof(StaticObject) },
                {Entity.Type.star,  typeof(Star) },
                {Entity.Type.mill,  typeof(MillPress) },
                {Entity.Type.spikes,  typeof(Spikes) },
                {Entity.Type.tower,  typeof(Tower) },
                {Entity.Type.arrow,  typeof(Direction) },//tower create actual arrow
                {Entity.Type.boulder,  typeof(Boulder) },
                {Entity.Type.boulderMark,  typeof(Direction) },
                {Entity.Type.trap,  typeof(Trap) },
            };

        private EntityConfig _config;

        //private Stack<Card> _cards; //TODO make DontDestoryOnLoad  pool
        public Creator(EntityConfig config)
        {
            _config = config;
        }

        public TileObject GetTileObject(Entity.Type type, Tile tile, int spriteIndex)
        {
            if (!_factory.ContainsKey(type)) //TODO TEMP
                return null;

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
