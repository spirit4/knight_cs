using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    class TowerArrow : MovingUnit
    {

        public TowerArrow(EntityInput config) : base(config)
        {
            
        }

        public override void CreateView(string layer)
        {
            _type = Entity.Type.arrow;

            base.CreateView(layer);
            _view.GetComponent<SpriteRenderer>().sortingLayerName = EntityInput.SortingLayer.Action.ToString();
        }

        public void Shoot(int direction, float speed)
        {
            if (direction == 1)
            {
                _view.GetComponent<SpriteRenderer>().flipX = true;
                _view.transform.DOLocalMoveX(Config.STAGE_W + 0.6f, speed).SetEase(Ease.Linear).OnComplete(Withdraw);
            }
            else
            {
                _view.GetComponent<SpriteRenderer>().flipX = false;
                _view.transform.DOLocalMoveX(-1.2f, speed).SetEase(Ease.Linear).OnComplete(Withdraw);
            }
        }

        public bool IsShooted()
        {
            return _view.activeSelf;
        }

        public override void BindToTile(Tile tile)
        {
            _tile = tile;
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[spriteIndex];
        }

    }
}