using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface ICollidable
    {
        GameObject view { get; }
        int index { get; }
        string type { get; }
        string state { get; }

        public void stop();
        public void destroy();
    }
}