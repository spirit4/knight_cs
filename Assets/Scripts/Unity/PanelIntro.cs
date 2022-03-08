using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Unity
{
    public class PanelIntro : MonoBehaviour
    {

        private List<Image> _frames = new List<Image>();

        [SerializeField]
        private Image _intro1;
        [SerializeField]
        private Image _intro2;
        [SerializeField]
        private Image _intro3;

        // Start is called before the first frame update
        void Start()
        {
            _frames.Add(_intro1);
            _frames.Add(_intro2);
            _frames.Add(_intro3);

            _frames[0].color = new Color(_frames[1].color.r, _frames[1].color.g, _frames[1].color.b, 0);
            _frames[1].color = new Color(_frames[1].color.r, _frames[1].color.g, _frames[1].color.b, 0);
            _frames[2].color = new Color(_frames[2].color.r, _frames[2].color.g, _frames[2].color.b, 0);

            showNext();
        }


        private void OnMouseDown()
        {
            if (_frames.Count == 0)
                return;

            _frames[0].color = new Color(_frames[0].color.r, _frames[0].color.g, _frames[0].color.b, 1f);
            _frames.RemoveAt(0);
        }


        private void showNext()
        {
            if (_frames.Count == 0)
                return;

            _frames[0].DOFade(1, 0.5f).SetEase(Ease.Linear);
            _frames.RemoveAt(0);
            DOTween.Sequence().AppendInterval(1.5f).AppendCallback(showNext);
        }

    }
}

