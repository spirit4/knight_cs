using Assets.Scripts.Achievements;
using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class MillPress : TileObject
    {
        private GameObject _vane;

        public MillPress(EntityInput config) : base(config)
        {
            
        }

        public override void BindToTile(Tile tile)
        {
            base.BindToTile(tile);
            _view.GetComponent<SpriteRenderer>().sortingOrder = _tile.Index;

            AddVane();
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[0];
        }

        public void Activate()
        {
            if (_isActive)
                return;

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
            _vane.GetComponent<SpriteRenderer>().sprite = _config.Sprites[1];
            _vane.GetComponent<SpriteRenderer>().sortingLayerName = _config.Layer.ToString();
            _vane.GetComponent<SpriteRenderer>().sortingOrder = _tile.Index + 1;

            _vane.transform.SetParent(_view.transform);
            _vane.transform.localPosition = new Vector3(0.02f, 0.5f);
            _vane.transform.Rotate(0, 0, -45);
        }

        public void StartRotateMill()
        {
            if (_isActive)
                return;

            GameEvents.AchTriggered(Trigger.TriggerType.MillLaunched);

            _isActive = true;
            RotateMill();
        }

        private void RotateMill()
        {
            _vane.transform.DORotate(new Vector3(0, 0, -395), 1.7f).SetLoops(-1).SetEase(Ease.Linear);
        }

        public override void Destroy()
        {
            _vane.transform.DOKill();
            UnityEngine.Object.Destroy(_vane);
            base.Destroy();
            _vane = null;
        }
    }
}