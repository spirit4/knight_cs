using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unity
{
    public class EventReceiver : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }


        public void AnimationCompleteHandler()
        {
            //Debug.Log("AnimationCompleteHandler] " + this.name);
            MessageDispatcher.SendMessage(GameEvent.ANIMATION_COMPLETE, this.name);
        }

    }
}
