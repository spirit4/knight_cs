using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class MillPress : Unit
    {
        private GameObject _vane;
        private Tile[] _grid;

        public MillPress(string type, int index, GameObject view, Component container) : base(index, type, view)
        {
            this.State = Unit.OFF;

            _grid = Controller.Instance.model.Grid;
            this.X = _grid[index].X;
            this.Y = _grid[index].Y;

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
            if (State != Unit.OFF)
                return;

            State = Unit.STARTED;

            View.transform.localScale = new Vector3(1.2f, 1.2f);
            DOTween.Sequence().AppendInterval(0.3f).AppendCallback(Comeback);
        }

        private void Comeback()
        {
            if (View != null)
                View.transform.localScale = new Vector3(1.0f, 1.0f);
        }

        private void AddVane()
        {
            _vane = new GameObject("vane");
            _vane.AddComponent<SpriteRenderer>();
            _vane.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.MILL_VANE);
            _vane.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            _vane.GetComponent<SpriteRenderer>().sortingOrder = Index + 1;

            _vane.transform.SetParent(View.transform);
            _vane.transform.localPosition = new Vector3(0.02f, 0.28f);
            _vane.transform.Rotate(0, 0, -45);
        }

        public void StartRotateMill()
        {
            if (State == Unit.ON)
                return;

            AchievementController.Instance.AddParam(AchievementController.MILL_LAUNCHED);

            State = Unit.ON;
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