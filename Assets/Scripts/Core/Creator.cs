using Assets.Scripts.Data;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Creator : IDestroyable
    {
        private Dictionary<Entity.Type, Func<EntityInput, Entity>> _factory 
            = new Dictionary<Entity.Type, Func<EntityInput, Entity>>()
        {
            {Entity.Type.grass, (config) => new Entity(config)},
            {Entity.Type.water, (config) => new Entity(config)},
            {Entity.Type.target_0, (config) => new PathPoint(config)},
        };

        private EntityConfig _config;

        //private Stack<Card> _cards; //TODO make DontDestoryOnLoad  pool
        public Creator(EntityConfig config)
        {
            _config = config;
            //_cards = new Stack<Card>((int)(config.FieldWidth * config.FieldHeight));
        }

        public Entity GetEntity(Entity.Type type)
        {
            Entity e = _factory[type](_config.GetConfig(type));
            e.AddSprite(_config.GetConfig(type).Sprites);
            return e;
        }

        //public Card GetCard(GameObject prefab, int level)
        //{
        //    Card card;
        //    if (_cards.Count > 0)
        //        card = _cards.Pop();
        //    else
        //        card = new Card(GameObject.Instantiate<GameObject>(prefab));

        //    int health = Random.Range(1, level + 2);//max inclusive

        //    card.Init(health);
        //    card.AddTrait(GetTrait(level)); //red or green

        //    return card;
        //}

        //private ITrait GetTrait(int level)
        //{
        //    float p1 = Random.Range(0, 1f);
        //    float p2 = level * 0.1f < 0.5f ? level * 0.1f : 0.5f;
        //    //Debug.Log($"if true {p1} > {p2} then it's a green card");

        //    if (p1 > p2)
        //        return new Greenness();

        //    return new Redness();
        //}


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
