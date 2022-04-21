using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface ICollidable
    {
        GameObject View { get; }
        string Type { get; }//TODO temp

        public void Stop();
    }
}