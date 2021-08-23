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
            view.GetComponent<SpriteRenderer>().sortingOrder = 10;//TODO --------------??
            view.name = type;

            //view.transform.SetParent(container.gameObject.transform);
            //view.transform.localPosition = new Vector3(grid[index].x - 0, grid[index].y + 0.2f, 0);
        }
    }
}