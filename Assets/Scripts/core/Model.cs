using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Model
    {
        private Tile[] _grid;

        public Model()
        {
            _grid = new Tile[Config.WIDTH * Config.HEIGHT];

            this.createGrid();
        }

        private void createGrid()
        {
            float xCell;
            float yCell;

            for (int i = 0; i < _grid.Length; i++)
            {
                yCell = (i / Config.WIDTH) * Config.SIZE_H;
                xCell = (i - i / Config.WIDTH * Config.WIDTH) * Config.SIZE_W;
                _grid[i] = new Tile(xCell, -yCell, i);
            }
        }

        //    public saveProgress(): void
        //{
        //    var progress: any = { };
        //    progress.levelsCompleted = Progress.levelsCompleted;
        //    progress.starsAllLevels = Progress.starsAllLevels;
        //    progress.achs = Progress.achs; //TODO add params

        //    try
        //    {
        //        window.localStorage.setItem(Config.GAME_NAME + Config.GAME_VERSION + "Progress", JSON.stringify(progress));
        //    }
        //    catch (error)
        //    {
        //        //console.log("[local storage error setItem]", error);
        //    }
        //}

        //public loadProgress(): void
        //{
        //    try
        //    {
        //        var progress: any = window.localStorage.getItem(Config.GAME_NAME + Config.GAME_VERSION + "Progress");
        //    }
        //    catch (error)
        //    {
        //        //console.log("[local storage error getItem]", error);
        //    }

        //    if (progress)
        //    {
        //        var progress = JSON.parse(progress);
        //        Progress.levelsCompleted = progress.levelsCompleted;
        //        Progress.starsAllLevels = progress.starsAllLevels;
        //        Progress.achs = progress.achs;
        //    }
        //}

        public Tile[] grid
        {
            get { return _grid; }
        }
    }
}
