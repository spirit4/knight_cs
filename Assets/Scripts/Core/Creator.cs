using Assets.Scripts.Units;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class Creator : IDestroyable
    {
        public Dictionary<Entity.Type, Type> _factory = new Dictionary<Entity.Type, Type>
            {
                {Entity.Type.Grass,  typeof(TileObject)},
                {Entity.Type.Water,  typeof(Water) },
                {Entity.Type.Decor,  typeof(Decor) },
                {Entity.Type.PathPoint,  typeof(PathPoint) },
                {Entity.Type.TargetMark,  typeof(TargetMark) },
                {Entity.Type.Pine,  typeof(StaticObject) },
                {Entity.Type.Stone,  typeof(StaticObject) },
                {Entity.Type.Stump,  typeof(StaticObject) },
                {Entity.Type.Bridge,  typeof(StaticObject) },
                {Entity.Type.Exit,  typeof(StaticObject) },
                {Entity.Type.Star,  typeof(Star) },
                {Entity.Type.Mill,  typeof(MillPress) },
                {Entity.Type.Spikes,  typeof(Spikes) },
                {Entity.Type.Tower,  typeof(Tower) },
                {Entity.Type.Arrow,  typeof(Direction) },//tower creates actual arrow
                {Entity.Type.Boulder,  typeof(Boulder) },
                {Entity.Type.BoulderMark,  typeof(Direction) },
                {Entity.Type.Trap,  typeof(Trap) },
                {Entity.Type.Monster,  typeof(Monster) },
                {Entity.Type.Hero,  typeof(Hero) },
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
