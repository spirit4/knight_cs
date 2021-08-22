using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Trap : Unit
    {
        public Trap(string type, int index, GameObject view) : base(index, type, view)
        {
            //this.mc.stop();
            //    this.mc.alpha = 0.4;
        }
        
    }
}