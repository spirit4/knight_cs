///using Assets.Scripts.Core;
using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using System;
using System.Collections.Generic;
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
            _grid = Controller.instance.model.grid;
            this.id = id;

            _pontIndex1 = index;
            this.x = _grid[index].x; //for choosing direction --> Level
            this.y = _grid[index].y;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 95;//TODO --------------??
            view.name = type + id;

            view.transform.SetParent(container.gameObject.transform);
            view.transform.localPosition = new Vector3(_grid[index].x + MARGIN_X, _grid[index].y + MARGIN_Y, 0);
        }

        private void move(float x, float y)
        {
            float time = 0;
            if (_directionX == 1 || _directionX == -1)
            {
                time = SPEED * Math.Abs(_pontIndex1 - _pontIndex2);
                view.transform.DOLocalMoveX(x + MARGIN_X, time).SetEase(Ease.Linear).OnComplete(setDirection);
            }
            else if (_directionY == 1 || _directionY == -1)
            {
                time = SPEED * Math.Abs(_pontIndex1 - _pontIndex2) / Config.WIDTH;
                view.transform.DOLocalMoveY(y + MARGIN_Y, time).SetEase(Ease.Linear).OnComplete(setDirection);
            }
        }

        private void setDirection()
        {
            int directionIndex = (this.index == _pontIndex1) ? _pontIndex2 : _pontIndex1;
            Tile tile = _grid[directionIndex];

            if (tile.y == this.y && tile.x > this.x)//directionIndex >= _pontIndex1)// 
            {
                _directionX = 1;
                _directionY = 0;
            }
            else if (tile.y == this.y && tile.x < this.x)
            {
                _directionX = -1;
                _directionY = 0;
            }
            else if (tile.x == this.x && tile.y > this.y)
            {
                _directionX = 0;
                _directionY = -1;
            }
            else if (tile.x == this.x && tile.y < this.y)
            {
                _directionX = 0;
                _directionY = 1;
            }

            FlipView();
            move(tile.x, tile.y);
            this.index = directionIndex;
            //Debug.Log("[setDirection1: ]" + tile.y  + "   " +  this.y);
            // Debug.Log("[setDirection2: ]" + _directionX + "   " + _directionY);


            this.x = _grid[index].x;//TODO fix hack ?
            //this.y = _grid[index].y;
        }

        private void FlipView()
        {
            //Debug.Log("setPointIndex2" + index);
            if (_directionX == 1)
            {
                view.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (_directionX == -1)
            {
                view.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        public void setPointIndex2(int index)
        {
            _pontIndex2 = index;
            //Debug.Log("setPointIndex2" + index);
            setDirection();
        }

        public void activate()
        {
            //empty
        }

        public void init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {
            //    if (_directionX == 1 || _directionY == 1)
            //    {
            //        _grid[_pontIndex2].setIndex(this);
            //    }
        }

        public override void destroy()
        {
            base.destroy();
        }
    }
}