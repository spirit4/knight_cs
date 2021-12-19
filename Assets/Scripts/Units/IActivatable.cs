using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface IActivatable
    {
        public void activate();
        //public void destroy();

       // public void init(i:number, grid?: Tile[], units?:{[index: number]: ICollidable; });
    }
}