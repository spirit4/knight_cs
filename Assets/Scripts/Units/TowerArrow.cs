using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    class TowerArrow : Unit
    {

        public TowerArrow(string type, int index, GameObject view) : base(index, type, view)
        {
            
        }

        public void Shoot(int direction, float speed)
        {
            if (direction == 1)
            {
                View.GetComponent<SpriteRenderer>().flipX = true;
                View.transform.DOLocalMoveX(Config.STAGE_W + 0.5f, speed).SetEase(Ease.Linear).OnComplete(EndHandler);
            }
            else
            {
                View.transform.DOLocalMoveX(-1f, speed).SetEase(Ease.Linear).OnComplete(EndHandler);
            }
        }

        private void EndHandler()
        {
            View.SetActive(false);
        }

        public bool IsShooted()
        {
            return View.activeSelf;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}