using Assets.Scripts.Core;
using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Units
{
    public class Spikes : TileObject, IActivatable
    {
        public Spikes(EntityInput config) : base(config)
        {

        }

        public override void BindToTile(Tile tile)
        {
            base.BindToTile(tile);
            _view.GetComponent<SpriteRenderer>().sortingOrder = _tile.Index;
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[0];
        }

        public void Activate()
        {
            _view.GetComponent<SpriteRenderer>().sprite = _config.Sprites[1];
            _tile.SetCost(Cost.Normal);
        }

        public ICollidable Init(Tile tile)
        {
            return null;
        }
    }
}