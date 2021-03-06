using Assets.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class PathPoint : Entity
    {

        public PathPoint(EntityInput config) : base(config)
        {

        }
        public override void CreateView(string layer)
        {
            base.CreateView(layer);
            _view.GetComponent<SpriteRenderer>().sortingOrder = 120;
        }

        public void DeployWithDelay(Transform container, Vector3 position, float delay)
        {
            _view.transform.localScale = Vector3.zero;
            base.Deploy(container, position);

            DOTween.Sequence().AppendInterval(delay).Append(_view.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuart))
                .OnComplete(() => _view.transform.DOScale(1, 0.06f).SetEase(Ease.InQuart));
        }

    }
}

