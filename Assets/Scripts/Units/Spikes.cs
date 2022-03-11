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
            tile.IsWall = true;
        }

        public void Init(int i = -1, Tile[] grid = null, Dictionary<int, ICollidable> units = null)
        {
            //empty
        }

        public void Activate()
        {
            State = Unit.ON;
            View.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.SPIKES + "1");
            Tile.IsWall = false;
        }

    }
}