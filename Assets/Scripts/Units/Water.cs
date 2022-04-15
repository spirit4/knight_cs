using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{

    public class Water : TileObject
    {

        public Water(EntityInput config) : base(config)
        {

        }

        public override void BindToTile(Tile tile)
        {
            base.BindToTile(tile);
            _view.GetComponent<SpriteRenderer>().sortingOrder = _tile.Index + 1;
        }
    }
}
