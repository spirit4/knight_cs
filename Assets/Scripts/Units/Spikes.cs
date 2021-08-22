using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Spikes : Unit, IActivatable
    {
        public Spikes(string type, int index, GameObject view, Tile tile) : base(index, type, view, tile)
        {
            tile.isWall = true;
        }

        //public init(): void
        //{
        //    //empty
        //}

        //public activate(): void
        //{
        //    this.state = Unit.ON;
        //    this.bitmap.image = ImagesRes.getImage(ImagesRes.SPIKES + 1);
        //    this.tile.isWall = false;
        //}

    }
}