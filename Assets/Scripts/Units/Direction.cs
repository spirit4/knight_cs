using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    /// <summary>
    /// Only purpose is to direct an Object nearby
    /// </summary>
    public class Direction : TileObject
    {

        public Direction(EntityInput config) : base(config)
        {

        }
        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            //empty
        }
        public override void CreateView(string layer)//TODO name better
        {
            //empty
        }
        public override void Deploy(Transform container, Vector3 position)
        {
            //empty
        }

        public override void Withdraw()
        {
            //empty
        }

        public override void Destroy()
        {
            //empty
        }

        public override void BindToTile(Tile tile)
        {
            //empty
        }
    }
}