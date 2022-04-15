using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{

    public class StaticObject : TileObject
    {

        public StaticObject(EntityInput config) : base(config)
        {

        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[Random.Range(0, spites.Length)];
        }
    }
}

