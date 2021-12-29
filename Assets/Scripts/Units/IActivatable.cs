using Assets.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface IActivatable
    {
        public void activate();

        public void init(int i, Tile[] grid = null, Dictionary<int, ICollidable> units = null);
    }
}