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

        public void AnimationCompleteHandler()
        {
            MessageDispatcher.SendMessage(GameEvent.ANIMATION_COMPLETE, this.name);
        }

    }
}
