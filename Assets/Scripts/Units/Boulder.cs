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
            tile.isWall = true;
        }

        public void init(int i, Tile[] grid, Dictionary<int, ICollidable> units = null)
        {
            _grid = grid;
            _activeIndex = GridUtils.findAround(grid, this.index, ImagesRes.BOULDER_MARK);
        }

        public void activate()
        {
            this.state = Unit.ON;
            this.index = _activeIndex;
            view.transform.DOLocalMove(new Vector3(_grid[this.index].x, _grid[this.index].y + 0.13f), 0.3f)
                .SetEase(Ease.Linear).OnComplete(completeHandler);
        }

        private void completeHandler()
        {
            tile.isWall = false;
            _grid[this.index].isWall = true;
        }

        //    public destroy(): void
        //    {
        //        super.destroy();

        //        _grid = null;
        //    }

        //}
    }
}