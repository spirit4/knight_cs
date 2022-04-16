using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Decor : TileObject
    {

        public Decor(EntityInput config) : base(config)
        {

        }

        public override void Deploy(Transform container, Vector3 position)
        {
            _view.transform.SetParent(container);

            //local position inside the Tile
            var point = GridUtils.GetUnityPoint((int)position.x, (int)position.y);
            _view.transform.localPosition = new Vector3(point.x - 0.15f, point.y + 0.19f);

            //above grass and water
            _view.GetComponent<SpriteRenderer>().sortingOrder = Config.WIDTH * Config.HEIGHT + 20;
            _view.SetActive(true);
        }

        public override void BindToTile(Tile tile)
        {
            //empty
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            //this line crushes Unity
            //(this as TileObject).AddView(spites, spriteIndex);

            _view.GetComponent<SpriteRenderer>().sprite = spites[spriteIndex];
        }
    }
}

