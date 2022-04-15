using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Grass
    /// </summary>
    public class TileObject : Entity
    {
        protected Tile _tile; //contains index

        private bool _isWall = false;//for pathfinder
        private bool _isExpensive = false;//for pathfinder for mill

        public TileObject(EntityInput config) : base(config)
        {
            if (config.Cost == Cost.Wall)//TODO refactor it in pathfinder first
                _isWall = true;
            else if (config.Cost == Cost.Expensive)
                _isExpensive = true;
        }

        public virtual void BindToTile(Tile tile)
        {
            _tile = tile;
            _tile.IsWall = _isWall;
            _tile.IsExpensive = _isExpensive;

            _view.GetComponent<SpriteRenderer>().sortingOrder = _tile.Index + 20;
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[Random.Range(0, spites.Length)];
        }
    }
}
