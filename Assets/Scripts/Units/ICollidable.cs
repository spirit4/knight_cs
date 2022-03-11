using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface ICollidable
    {
        GameObject View { get; }
        int Index { get; }
        string Type { get; }
        string State { get; }

        public void Stop();
        public void Destroy();
    }
}