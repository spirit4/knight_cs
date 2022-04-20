using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public class EventReceiver : MonoBehaviour
    {

        public void AnimationCompleteHandler()
        {
            //Debug.Log(this.name);
            GameEvents.AnimationEnded(); 
        }

    }
}
