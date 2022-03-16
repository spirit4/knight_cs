using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Tower : Unit, IActivatable
    {
        private int _directionIndex;
        private TowerArrow _arrow;

        private float _speedTime = 0;
        private const float SPEED = 0.090f;

        private int _count = 0;

        private Component _container;

        public Tower(string type, int index, GameObject view, Tile tile, Component container) : base(index, type, view, tile)
        {
            tile.IsWall = true;
            _container = container;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 97;
        }

        public void Init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {
            _grid = grid;
            _directionIndex = GridUtils.FindAround(grid, _index, ImagesRes.ARROW);
            _count = i;
            _x = _grid[_index].X;
            _y = _grid[_index].Y;

            Activate();
            units[_index] = _arrow;
        }

        public void Activate()
        {
            GameObject dObject = new GameObject(ImagesRes.ARROW);
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.ARROW);
            dObject.transform.SetParent(_container.gameObject.transform);

            dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            dObject.GetComponent<SpriteRenderer>().sortingOrder = 98;
            dObject.SetActive(false);

            _arrow = new TowerArrow(ImagesRes.ARROW, _index, dObject);
            DOTween.Sequence().AppendInterval(_speedTime * 7 + 0.7f * _count).AppendCallback(Shoot);
        }

        private void Shoot()
        {
            if (_arrow == null)
                return;

            _arrow.View.transform.localPosition = new Vector3(_x - 0.01f,_y + 0.3f);
            int localIndex = (int)Math.Round(_grid[_index].X / Config.SIZE_W);

            _arrow.View.SetActive(true);

            if (_index > _directionIndex)
            {
                _speedTime = SPEED + localIndex * SPEED;
                _arrow.Shoot(-1, _speedTime);
            }
            else
            {
                _speedTime = (Config.WIDTH - localIndex) * SPEED;
                _arrow.Shoot(1, _speedTime);
            }

            DOTween.Sequence().AppendInterval(_speedTime * 7 + 0.7f * _count).AppendCallback(Shoot);

        }

        public override void Destroy()
        {
            base.Destroy();
            _arrow = null;
        }
    }
}