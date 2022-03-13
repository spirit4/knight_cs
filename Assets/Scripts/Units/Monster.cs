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

        private int _id = -1;


        public Monster(string type, int index, int id, GameObject view, Component container) : base(index, type, view)
        {
            _grid = Controller.Instance.model.Grid;
            _id = id;

            _pontIndex1 = index;
            this.X = _grid[index].X; //for choosing direction --> Level
            this.Y = _grid[index].Y;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 95;
            view.name = type + id;

            view.transform.SetParent(container.gameObject.transform);
            view.transform.localPosition = new Vector3(_grid[index].X + MARGIN_X, _grid[index].Y + MARGIN_Y, 0);
        }

        private void Move(float x, float y)
        {
            float time = 0;
            if (_directionX == 1 || _directionX == -1)
            {
                time = SPEED * Math.Abs(_pontIndex1 - _pontIndex2);
                View.transform.DOLocalMoveX(x + MARGIN_X, time).SetEase(Ease.Linear).OnComplete(SetDirection);
            }
            else if (_directionY == 1 || _directionY == -1)
            {
                time = SPEED * Math.Abs(_pontIndex1 - _pontIndex2) / Config.WIDTH;
                View.transform.DOLocalMoveY(y + MARGIN_Y, time).SetEase(Ease.Linear).OnComplete(SetDirection);
            }
        }

        private void SetDirection()
        {
            int directionIndex = (Index == _pontIndex1) ? _pontIndex2 : _pontIndex1;
            Tile tile = _grid[directionIndex];

            if (tile.Y == this.Y && tile.X > this.X)//directionIndex >= _pontIndex1)// 
            {
                _directionX = 1;
                _directionY = 0;
            }
            else if (tile.Y == this.Y && tile.X < this.X)
            {
                _directionX = -1;
                _directionY = 0;
            }
            else if (tile.X == this.X && tile.Y > this.Y)
            {
                _directionX = 0;
                _directionY = -1;
            }
            else if (tile.X == this.X && tile.Y < this.Y)
            {
                _directionX = 0;
                _directionY = 1;
            }

            FlipView();
            Move(tile.X, tile.Y);
            Index = directionIndex;


            this.X = _grid[Index].X;//TODO find better solution?
            //this.y = _grid[index].y;
        }

        private void FlipView()
        {
            if (_directionX == 1)
            {
                View.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (_directionX == -1)
            {
                View.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        public void SetPointIndex2(int index)
        {
            _pontIndex2 = index;
            SetDirection();
        }

        public void Activate()
        {
            //empty
        }

        public void Init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {

        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}