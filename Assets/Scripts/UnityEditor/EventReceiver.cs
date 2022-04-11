using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public class EventReceiver : MonoBehaviour
    {

        public void AnimationCompleteHandler()
        {
            GameEvents.AnimationEnded(); //basically it's a pecular smoke detector
        }

    }
}
