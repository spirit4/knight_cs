using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Unity
{
    public class MainView
    {
        private GameObject _vane;
        private GameObject _smoke;


        public MainView(GameObject vane, GameObject smoke)
        {
            _vane = vane;
            _smoke = smoke;
            RotateMill();
            MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, SmokeCompleteHandler); //TODO remove?
        }
        public void SmokeCompleteHandler(IMessage rMessage)
        {
            Animator anim = _smoke.GetComponent<Animator>();
            anim.enabled = false;

            DOTween.Sequence().AppendInterval(3.0f).AppendCallback(PlayAgain);
        }

        private void PlayAgain()
        {
            Animator anim = _smoke.GetComponent<Animator>();
            anim.enabled = true;
        }

        private void RotateMill()
        {
            _vane.transform.DORotate(new Vector3(0, 0, -395), 3.0f).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}