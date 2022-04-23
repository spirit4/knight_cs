using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Monster : MovingUnit, IActivatable
    {
        private int _directionX = 0;
        private int _directionY = 0;

        private int _pointIndex1 = -1;
        private int _pointIndex2 = -1;
        private int _currentIndex = -1;

        private const float SPEED = 1.0f;
        private const float MARGIN_X = 0f;
        private const float MARGIN_Y = 0.1f;

        private Tile _activeTile;

        public Monster(EntityInput config) : base(config)
        {

        }

        public ICollidable Init(Tile tile)
        {
            _view = Object.Instantiate(_config.Prefabs[0]);
            _view.GetComponent<SpriteRenderer>().sortingLayerName = _config.Layer.ToString();
            _view.GetComponent<SpriteRenderer>().sortingOrder = 130;
            _pointIndex2 = tile.Index;
            _activeTile = tile;

            SetDirection();

            return null;
        }

        public override void CreateView(string layer)
        {
            //empty

        }
        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            //empty
        }

        public override void Deploy(Transform container, Vector3 position)
        {
            base.Deploy(container, position);
            _view.transform.localPosition = position + new Vector3(MARGIN_X, MARGIN_Y);
        }

        public override void BindToTile(Tile tile)
        {
            _tile = tile;
            _pointIndex1 = _tile.Index;
        }

        private void Move(float x, float y)
        {
            float time = 0;
            if (_directionX == 1 || _directionX == -1)
            {
                time = SPEED * Mathf.Abs(_pointIndex1 - _pointIndex2);
                _view.transform.DOLocalMoveX(x + MARGIN_X, time).SetEase(Ease.Linear).OnComplete(SetDirection);
            }
            else if (_directionY == 1 || _directionY == -1)
            {
                time = SPEED * Mathf.Abs(_pointIndex1 - _pointIndex2) / Config.WIDTH;
                _view.transform.DOLocalMoveY(y + MARGIN_Y, time).SetEase(Ease.Linear).OnComplete(SetDirection);
            }
        }

        private void SetDirection()
        {
            int directionIndex = (_currentIndex == _pointIndex1) ? _pointIndex2 : _pointIndex1;
            Tile tile;
            if (_tile.Index == directionIndex)
                tile = _activeTile;
            else
                tile = _tile;

            if (tile.Y == _tile.Y && tile.X > _tile.X)
            {
                _directionX = 1;
                _directionY = 0;
            }
            else if (tile.Y == _tile.Y && tile.X < _tile.X)
            {
                _directionX = -1;
                _directionY = 0;
            }
            else if (tile.X == _tile.X && tile.Y > _tile.Y)
            {
                _directionX = 0;
                _directionY = -1;
            }
            else if (tile.X == _tile.X && tile.Y < _tile.Y)
            {
                _directionX = 0;
                _directionY = 1;
            }

            tile = _tile;
            _tile = _activeTile;
            _activeTile = tile;

            FlipView();
            Move(tile.X, tile.Y);
            _currentIndex = directionIndex;
        }

        private void FlipView()
        {
            if (_directionX == 1)
            {
                _view.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (_directionX == -1)
            {
                _view.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void Activate()
        {
            //empty
        }

        public override void Destroy()
        {
            _activeTile = null;
            base.Destroy();
        }
    }
}