using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Unity
{
    public class PanelMainMenu : MonoBehaviour
    {
        public GameObject vane;
        public GameObject smoke;

        // Start is called before the first frame update
        void Start()
        {
            RotateMill();
            MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, SmokeCompleteHandler);
        }

        // Update is called once per frame
        public void Update()
        {

        }



        private void RotateMill()
        {
            vane.transform.DORotate(new Vector3(0, 0, -395), 3.0f).SetLoops(-1).SetEase(Ease.Linear);
        }

        public void SmokeCompleteHandler(IMessage rMessage)
        {
            //Debug.Log("SmokeCompleteHandler]");

            Animator anim = smoke.GetComponent<Animator>();
            anim.enabled = false;

            Sequence mySequence = DOTween.Sequence();
            mySequence.AppendInterval(3.0f);
            mySequence.AppendCallback(PlayAgain);
        }

        private void PlayAgain()
        {
            Animator anim = smoke.GetComponent<Animator>();
            anim.enabled = true;
        }

    }
}
