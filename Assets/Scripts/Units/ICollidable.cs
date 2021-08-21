using UnityEngine;

namespace Assets.Scripts.Units
{
    interface ICollidable
    {
        GameObject view { get; }
        int index { get; }
        string type { get; }
        string state { get; }

        public void stop();
        public void destroy();
    }
}