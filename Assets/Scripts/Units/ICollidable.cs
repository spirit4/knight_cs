using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface ICollidable
    {
        GameObject View { get; }
        //int Index { get; }
        string Type { get; }//temp
        //string State { get; }

        public void Stop();
    }
}