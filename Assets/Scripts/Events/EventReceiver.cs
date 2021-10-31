using Assets.Scripts.Events;
using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Events
{
    public class EventReceiver : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AnimationCompleteHandler()
        {
            //Debug.Log("AnimationCompleteHandler]");
            MessageDispatcher.SendMessage(this.gameObject, GameEvent.ANIMATION_COMPLETE, null, 0);
        }
    }
}
