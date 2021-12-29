using Assets.Scripts.Core;
using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Spikes : Unit, IActivatable
    {
        public Spikes(string type, int index, GameObject view, Tile tile) : base(index, type, view, tile)
        {
            tile.isWall = true;
        }

        public void init(int i = -1, Tile[] grid = null, Dictionary<int, ICollidable> units = null)
        {
            //empty
        }

        public void activate()
        {
            this.state = Unit.ON;
            view.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.SPIKES + "1");
            tile.isWall = false;
        }

    }
}