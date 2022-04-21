using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class TargetMark : Entity
    {

        public TargetMark(EntityInput config) : base(config)
        {

        }
        public override void CreateView(string layer)
        {
            base.CreateView(layer);
            _view.GetComponent<SpriteRenderer>().sortingOrder = 120;
        }
        public override void Deploy(Transform container, Vector3 position)
        {
            _view.transform.localScale = new Vector3(0.9f, 0.9f);
            _view.transform.SetParent(container);
            _view.transform.localPosition = position;
            _view.SetActive(true);

            Appear();
        }

        private void Appear()
        {
            _view.transform.DOScale(1.0f, 0.08f).SetEase(Ease.InQuart).OnComplete(Extend);
        }

        private void Extend()
        {
            _view.transform.DOScale(1.2f, 0.08f).SetEase(Ease.OutQuart).OnComplete(Disappear);
        }

        private void Disappear()
        {
            _view.transform.DOScale(0, 0.12f).SetEase(Ease.InQuart).OnComplete(Withdraw);
        }
    }
}
