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
            this.state = Unit.OFF;

            _grid = Controller.instance.model.grid;
            this.x = _grid[index].x;
            this.y = _grid[index].y;

            view.AddComponent<SpriteRenderer>();
            view.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(type);

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = index;
            view.name = type;

            view.transform.SetParent(container.gameObject.transform);
            view.transform.localPosition = new Vector3(_grid[index].x, _grid[index].y + 0.22f);

            AddVane();
        }

        public void activate()
        {
            if (this.state != Unit.OFF)
                return;

            this.state = Unit.STARTED;

            view.transform.localScale = new Vector3(1.2f, 1.2f);
            DOTween.Sequence().AppendInterval(0.3f).AppendCallback(comeback);
        }

        private void comeback()
        {
            view.transform.localScale = new Vector3(1.0f, 1.0f);
        }

        private void AddVane()
        {
            _vane = new GameObject("vane");
            _vane.AddComponent<SpriteRenderer>();
            _vane.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.MILL_VANE);
            _vane.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            _vane.GetComponent<SpriteRenderer>().sortingOrder = index + 1;

            _vane.transform.SetParent(view.transform);
            _vane.transform.localPosition = new Vector3(0.02f, 0.28f);
            _vane.transform.Rotate(0, 0, -45);
        }

        public void startRotateMill()
        {
            if (this.state == Unit.ON)
                return;

            AchController.instance.addParam(AchController.MILL_LAUNCHED);

            this.state = Unit.ON;
            rotateMill();
        }

        private void rotateMill()
        {
            _vane.transform.DORotate(new Vector3(0, 0, -395), 1.7f).SetLoops(-1).SetEase(Ease.Linear);
        }

        //public destroy(): void
        //{
        //    super.destroy();
        //    createjs.Tween.removeTweens(_vane);

        //    _vane = null;
        //    _grid = null;
        //}
    }
}