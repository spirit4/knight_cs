///using Assets.Scripts.Core;
using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Monster : Unit, IActivatable
    {
        private int _directionX = 0;
        private int _directionY = 0;

        private Tile[] _grid;
        private int _pontIndex1 = 0;
        private int _pontIndex2 = 0;

        private const float SPEED = 1.0f;//1.15f;
        private const float MARGIN_X = 0f;
        private const float MARGIN_Y = 0.1f;

        public int id = -1;


        public Monster(string type, int index, int id, GameObject view, Component container) : base(index, type, view)
        {
            this._grid = Controller.instance.model.grid;
            this.id = id;

            this._pontIndex1 = index;
            this.x = this._grid[index].x; //for choosing direction --> Level
            this.y = this._grid[index].y;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 10;//TODO --------------??
            view.name = type;
            //view.GetComponent<Bounds>
            view.transform.SetParent(container.gameObject.transform);
            view.transform.localPosition = new Vector3(_grid[index].x + MARGIN_X, _grid[index].y + MARGIN_Y, 0);
            //Debug.Log(view.GetComponent<SpriteRenderer>().sprite.bounds.extents.x);
            //Debug.Log(view.GetComponent<SpriteRenderer>().sprite.bounds.extents.y);
        }

        private void move(float x, float y)
        {
            float time = 0;
            if (this._directionX == 1 || this._directionX == -1) //!= 0
            {
                time = SPEED * Math.Abs(this._pontIndex1 - this._pontIndex2);
                view.transform.DOLocalMoveX(x + MARGIN_X, time).SetEase(Ease.Linear).OnComplete(setDirection);
            }
            else if (this._directionY == 1 || this._directionY == -1)
            {
                //Debug.Log("[move ]" + y + "   " + time);
                time = SPEED * Math.Abs(this._pontIndex1 - this._pontIndex2) / Config.WIDTH;
                view.transform.DOLocalMoveY(y + MARGIN_Y, time).SetEase(Ease.Linear).OnComplete(setDirection); 
            }

            
            //    createjs.Tween.get(this).to({ x: x, y: y }, time, createjs.Ease.linear).call(this.setDirection, [], this);
        }

        private void setDirection()
        {
            int directionIndex = (this.index == this._pontIndex1) ? this._pontIndex2 : this._pontIndex1;
            Tile tile = this._grid[directionIndex];
            if (tile.y == this.x && tile.x > this.x)
            {
                this._directionX = 1;
                this._directionX = 0;
            }
            else if (tile.y == this.y && tile.x < this.x)
            {
                this._directionX = -1;
                this._directionY = 0;
            }
            else if (tile.x == this.x && tile.y > this.y)
            {
                this._directionX = 0;
                this._directionY = 1;
            }
            else if (tile.x == this.x && tile.y < this.y)
            {
                this._directionX = 0;
                this._directionY = -1;
            }

            this.FlipView();
            this.move(tile.x, tile.y);
            this.index = directionIndex;
        }

        private void FlipView() // TODO test it
        {
            if (this._directionX == 1)
            {
                view.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (this._directionX == -1)
            {
                view.GetComponent<SpriteRenderer>().flipX = false;
            }

        }

        public void setPointIndex2(int index)
        {
            this._pontIndex2 = index;
            this.setDirection();
        }

        //public activate(): void
        //{
        //     //empty
        //}

        //public init(): void
        //{
        //    if (this._directionX == 1 || this._directionY == 1)
        //    {
        //        this._grid[this._pontIndex2].setIndex(this);
        //    }
        //}

        //public destroy(): void
        //{
        //    super.destroy();

        //    this._grid = null;
        //}
    }
}