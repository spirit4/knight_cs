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
        private Tile[] _grid;
        private TowerArrow _arrow;

        private float _speedTime = 0;
        private const float SPEED = 0.090f;

        private int _count = 0;

        private Component _container;

        public Tower(string type, int index, GameObject view, Tile tile, Component container) : base(index, type, view, tile)
        {
            tile.isWall = true;
            _container = container;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 97;
        }

        public void init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {
            _grid = grid;
            _directionIndex = GridUtils.findAround(grid, this.index, ImagesRes.ARROW);
            _count = i;
            this.x = _grid[index].x;
            this.y = _grid[index].y;

            activate();
            units[this.index] = _arrow;
        }

        public void activate()
        {
            //Debug.Log("TowerArrow");
            GameObject dObject = new GameObject(ImagesRes.ARROW);
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.ARROW);
            dObject.transform.SetParent(_container.gameObject.transform);
            //dObject.transform.localPosition = new Vector3(this.x - 0.01f, this.y + 0.3f);
            dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            dObject.GetComponent<SpriteRenderer>().sortingOrder = 98;
            dObject.SetActive(false);

            _arrow = new TowerArrow(ImagesRes.ARROW, this.index, dObject);
            DOTween.Sequence().AppendInterval(_speedTime * 7 + 0.7f * _count).AppendCallback(shoot);
        }

        private void shoot()
        {
            if (_arrow == null)
                return;

            _arrow.view.transform.localPosition = new Vector3(this.x - 0.01f, this.y + 0.3f);
            //    this._arrow.x = this.view.x + 3;
            //    this._arrow.y = this.view.y + 20;
            int localIndex = (int)Math.Round(_grid[this.index].x / Config.SIZE_W);

            _arrow.view.SetActive(true);
            //this._arrow.visible = true;
            if (this.index > _directionIndex)
            {
                _speedTime = SPEED + localIndex * SPEED;
                _arrow.shoot(-1, _speedTime);
            }
            else
            {
                _speedTime = (Config.WIDTH - localIndex) * SPEED;
                _arrow.shoot(1, _speedTime);
            }

            DOTween.Sequence().AppendInterval(_speedTime * 7 + 0.7f * _count).AppendCallback(shoot);
            //    createjs.Tween.get(this).wait(this._speedTime * 7 + 700 * this._count).call(this.shoot, [], this);
        }

        public override void destroy()
        {
            base.destroy();

            //    this._grid = null;
            //_arrow.destroy(); it's destroyed in _units
            _arrow = null;
        }
    }
}