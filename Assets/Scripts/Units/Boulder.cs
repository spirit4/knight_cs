using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Boulder : Unit, IActivatable
    {
        private int _activeIndex;
        private Tile[] _grid;

        public Boulder(string type, int index, GameObject view, Tile tile) : base(index, type, view, tile)
        {
            //    constructor(type: string, index: number, view: createjs.DisplayObject, tile: Tile)
            //    {
            //        super(index, type, view, tile);
            //        tile.isWall = true;
            //        view.x += 4;
            //        view.y -= 13;
        }

        //    public init(i:number, grid: Tile[]): void
        //    {
        //        this._grid = grid;
        //        this._activeIndex = Utils.findAround(grid, this.index, ImagesRes.BOULDER_MARK);
        //        //console.log("checkInit", this._activeIndex);
        //    }

            public void activate()
            {
        //        this.state = Unit.ON;
        //        this.index = this._activeIndex;
        //        createjs.Tween.get(this.bitmap).
        //            to({ x: this._grid[this.index].x + 4, y: this._grid[this.index].y - 13 },300, createjs.Ease.linear)
        //            .call(this.completeHandler, [], this);
            }

        //    private completeHandler(): void
        //    {
        //        this.tile.isWall = false;
        //        this._grid[this.index].isWall = true;
        //    }

        //    public destroy(): void
        //    {
        //        super.destroy();

        //        this._grid = null;
        //    }

        //}
    }
}