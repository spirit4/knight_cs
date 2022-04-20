using Assets.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface IActivatable
    {
        public void Activate();
        public ICollidable Init(Tile tile);
    }
}