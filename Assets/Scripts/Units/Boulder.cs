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
        private Tile[] _grid;

        public Boulder(string type, int index, GameObject view, Tile tile) : base(index, type, view, tile)
        {
            tile.IsWall = true;
        }

        public void Init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {
            _grid = grid;
            _activeIndex = GridUtils.FindAround(grid, Index, ImagesRes.BOULDER_MARK);
        }

        public void Activate()
        {
            State = Unit.ON;
            Index = _activeIndex;
            View.transform.DOLocalMove(new Vector3(_grid[Index].X, _grid[Index].Y + 0.13f), 0.3f)
                .SetEase(Ease.Linear).OnComplete(CompleteHandler);
        }

        private void CompleteHandler()
        {
            Tile.IsWall = false;
            _grid[Index].IsWall = true;
        }

        //    public destroy(): void
        //    {
        //        super.destroy();

        //        _grid = null;
        //    }

        //}
    }
}