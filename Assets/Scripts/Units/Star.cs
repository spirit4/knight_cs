using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Star : Unit
    {
        public Star(string type, int index, GameObject view) : base(index, type, view)
        {
            Tile[] grid = Controller.instance.model.grid;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 10;
            view.name = type;
        }
    }
}