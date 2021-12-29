using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Trap : Unit
    {
        public Trap(string type, int index, GameObject view) : base(index, type, view)
        {
            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = index;
            view.name = type;

            Animator anim = view.GetComponent<Animator>();
            anim.enabled = false;

            Color color = view.GetComponent<SpriteRenderer>().color;
            color.a = 0.4f;
            view.GetComponent<SpriteRenderer>().color = color;
        }

    }
}