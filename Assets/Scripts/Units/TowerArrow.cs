using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    class TowerArrow : Unit
    {
        private int _pontIndex1 = 0;

        public int id = -1;

        public TowerArrow(string type, int index, GameObject view) : base(index, type, view)
        {
            _pontIndex1 = index;
        }

        public void shoot(int direction, float speed)
        {
            if (direction == 1)
            {
                view.GetComponent<SpriteRenderer>().flipX = true;
                view.transform.DOLocalMoveX(Config.STAGE_W + 0.5f, speed).SetEase(Ease.Linear).OnComplete(endHandler);
            }
            else
            {
                view.transform.DOLocalMoveX(-1f, speed).SetEase(Ease.Linear).OnComplete(endHandler);
            }
        }

        private void endHandler()
        {
            view.SetActive(false);
        }

        public bool isShooted()
        {
            return view.activeSelf;
        }

        public override void destroy()
        {
            base.destroy();
        }
    }
}