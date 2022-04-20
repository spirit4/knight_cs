using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Star : TileObject
    {
        public enum Kind
        {
            Helmet,
            Shield,
            Sword,
        }
        private Kind _starType;

        public Kind StarType { get => _starType; }

        
        public Star(EntityInput config) : base(config)
        {
            
        }

        public override void CreateView(string layer)
        {
            //empty
        }

        public override void BindToTile(Tile tile)
        {
            base.BindToTile(tile);

            _view.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view = Object.Instantiate(_config.Prefabs[spriteIndex]);
            _view.GetComponent<SpriteRenderer>().sortingLayerName = _config.Layer.ToString();
            _starType = (Kind)spriteIndex;
            _view.name = _starType.ToString();
        }

        public override void Deploy(Transform container, Vector3 position)
        {
            base.Deploy(container, position);
            _view.transform.localPosition = new Vector3(position.x + 0.02f, position.y + 0.11f * (int)_starType - 0.07f);
        }
 
    }
}