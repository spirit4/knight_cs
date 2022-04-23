using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Tower : TileObject, IActivatable
    {
        private int _directionIndex;
        private TowerArrow _arrow;

        private float _speedTime = 0;
        private const float SPEED = 0.090f;

        private static int _count = 0;//for a delay if there's more than 1 tower

        public Tower(EntityInput config) : base(config)
        {

        }
        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[spriteIndex];
        }

        public override void BindToTile(Tile tile)
        {
            base.BindToTile(tile);
            _view.GetComponent<SpriteRenderer>().sortingOrder = 150;
        }

        public ICollidable Init(Tile activeTile)
        {
            _directionIndex = activeTile.Index;
            CreateArrow();
            _arrow.View.GetComponent<SpriteRenderer>().sortingOrder = 151;

            _count++;

            return _arrow;
        }
        public void Activate()
        {
            //empty
        }

        public void CreateArrow()
        {
            _arrow = new TowerArrow(new EntityInput());
            _arrow.AddView(_config.Sprites, 1);
            _arrow.BindToTile(_tile);

            DOTween.Sequence().AppendInterval(_speedTime * 7 + 0.7f * _count).AppendCallback(Shoot);
            _arrow.AddView(_config.Sprites, 1);
        }

        private void Shoot()
        {
            if (_arrow == null)//level destroyed
                return;

            _arrow.Deploy(_view.transform.parent, new Vector3(_tile.X - 0.01f, _tile.Y + 0.3f));
            int localIndex = (int)(_tile.X / Config.SIZE_W);

            if (_tile.Index > _directionIndex)
            {
                _speedTime = SPEED + localIndex * SPEED;
                _arrow.Shoot(-1, _speedTime);
            }
            else
            {
                _speedTime = (Config.WIDTH - localIndex) * SPEED;
                _arrow.Shoot(1, _speedTime);
            }

            //continues shooting
            DOTween.Sequence().AppendInterval(_speedTime * 7 + 0.7f * _count).AppendCallback(Shoot);
        }

        public override void Destroy()
        {
            _arrow?.Destroy();
            _arrow = null;
            base.Destroy();
        }
    }
}