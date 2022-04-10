using Assets.Scripts.Achievements;
using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class MillPress : Unit
    {
        private GameObject _vane;

        public MillPress(string type, int index, GameObject view, Component container) : base(index, type, view)
        {
            _state = Unit.OFF;

            _grid = Model.Grid;
            _x = _grid[index].X;
            _y = _grid[index].Y;

            view.AddComponent<SpriteRenderer>();
            view.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(type);

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = index;
            view.name = type;

            view.transform.SetParent(container.gameObject.transform);
            view.transform.localPosition = new Vector3(_grid[index].X, _grid[index].Y + 0.22f);

            AddVane();
        }

        public void Activate()
        {
            if (_state != Unit.OFF)
                return;

            _state = Unit.STARTED;

            _view.transform.localScale = new Vector3(1.2f, 1.2f);
            DOTween.Sequence().AppendInterval(0.3f).AppendCallback(Comeback);
        }

        private void Comeback()
        {
            if (_view != null)
                _view.transform.localScale = new Vector3(1.0f, 1.0f);
        }

        private void AddVane()
        {
            _vane = new GameObject("vane");
            _vane.AddComponent<SpriteRenderer>();
            _vane.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.MILL_VANE);
            _vane.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            _vane.GetComponent<SpriteRenderer>().sortingOrder = Index + 1;

            _vane.transform.SetParent(_view.transform);
            _vane.transform.localPosition = new Vector3(0.02f, 0.28f);
            _vane.transform.Rotate(0, 0, -45);
        }

        public void StartRotateMill()
        {
            if (_state == Unit.ON)
                return;

            GameEvents.AchTriggered(Trigger.TriggerType.MillLaunched);

            _state = Unit.ON;
            RotateMill();
        }

        private void RotateMill()
        {
            _vane.transform.DORotate(new Vector3(0, 0, -395), 1.7f).SetLoops(-1).SetEase(Ease.Linear);
        }

        public override void Destroy()
        {
            _vane.transform.DOKill();
            base.Destroy();

            _vane = null;
            _grid = null;
        }
    }
}