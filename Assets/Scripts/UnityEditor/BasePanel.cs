using Assets.Scripts.Events;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public class BasePanel : MonoBehaviour
    {
        protected Transform _vane;
        protected Transform _smoke;

        protected virtual void Awake()
        {
            //names of prefabs in different Panels
            _vane = this.transform.Find("Mill"); 
            _smoke = this.transform.Find("Smoke");
        }
        protected virtual void Start()
        {
            if(_vane != null)
                RotateMill();

            if(_smoke != null)
                GameEvents.AnimationEndedHandlers += SmokeCompleteHandler;
        }
        private void SmokeCompleteHandler()
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

        protected void RotateMill()
        {
            _vane.DORotate(new Vector3(0, 0, -395), 3.0f).SetLoops(-1).SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            GameEvents.AnimationEndedHandlers -= SmokeCompleteHandler;
        }
    }
}