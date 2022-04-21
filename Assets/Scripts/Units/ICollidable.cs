using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface ICollidable
    {
        GameObject View { get; }
        public void Stop();
    }
}