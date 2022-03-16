using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Boulder : Unit, IActivatable
    {
        private int _activeIndex;

        public Boulder(string type, int index, GameObject view, Tile tile) : base(index, type, view, tile)
        {
            tile.IsWall = true;
        }

        public void Init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {
            _grid = grid;
            _activeIndex = GridUtils.FindAround(_grid, _index, ImagesRes.BOULDER_MARK);
        }

        public void Activate()
        {
            _state = Unit.ON;
            _index = _activeIndex;
            _view.transform.DOLocalMove(new Vector3(_grid[_index].X, _grid[_index].Y + 0.13f), 0.3f)
                .SetEase(Ease.Linear).OnComplete(CompleteHandler);
        }

        private void CompleteHandler()
        {
            _tile.IsWall = false;
            _grid[_index].IsWall = true;
        }
    }
}