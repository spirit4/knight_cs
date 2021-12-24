using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    class TowerArrow : Unit
    {
        //private int _directionX = 0;
        //private int _directionY = 0;

        //private Tile[] _grid;
        private int _pontIndex1 = 0;
        //private int _pontIndex2 = 0;

        public int id = -1;

        public TowerArrow(string type, int index, GameObject view) : base(index, type, view)
        {
            //_grid = Controller.instance.model.grid;

            _pontIndex1 = index;


            //    var bd: HTMLImageElement = ImagesRes.getImage(type);
            //    this.view = new createjs.Bitmap(bd);
            //    this.view.snapToPixel = true;

            //    this.addChild(this.view);
        }

        public void shoot(int direction, float speed)
        {
            if (direction == 1)
            {
                //this.view.scaleX = -1;
                //this.view.x = this.view.getBounds().width;
                view.GetComponent<SpriteRenderer>().flipX = true;
                //createjs.Tween.get(this).to({ x: Config.STAGE_W + 110 }, speed, createjs.Ease.linear).call(this.endHandler, [], this);

                view.transform.DOLocalMoveX(Config.STAGE_W + 0.5f, speed).SetEase(Ease.Linear).OnComplete(endHandler);
            }
            else
            {
                view.transform.DOLocalMoveX(-1f, speed).SetEase(Ease.Linear).OnComplete(endHandler);
                //createjs.Tween.get(this).to({ x: -150}, speed, createjs.Ease.linear).call(this.endHandler, [], this);
            }

        }

        private void endHandler()
        {
            view.SetActive(false);
            //    this.visible = false;
        }

        public bool isShooted()
        {
            return view.activeSelf;
        }

        public override void destroy()
        {
            base.destroy();

            //this._grid = null;
        }
    }
}