using Assets.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Boulder : TileObject, IActivatable
    {
        private Tile _activeTile;

        public Boulder(EntityInput config) : base(config)
        {

        }

        public ICollidable Init(Tile activeTile)
        {
            _activeTile = activeTile;
            return null;
        }

        public void Activate()
        {
            _isActive = true;
            _view.transform.DOLocalMove(new Vector3(_activeTile.X, _activeTile.Y), 0.3f)
                .SetEase(Ease.Linear).OnComplete(CompleteHandler);
        }

        private void CompleteHandler()
        {
            _tile.SetCost(Cost.Normal);
            _activeTile.SetCost(Cost.Wall);
        }

        public override void Destroy()
        {
            _activeTile = null;
            base.Destroy();
        }
    }
}