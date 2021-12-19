using Assets.Scripts.Core;
using Assets.Scripts.Data;
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

        public void activate()
        {
            this.state = Unit.ON;
            view.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.SPIKES + "1");
            tile.isWall = false;
        }

    }
}