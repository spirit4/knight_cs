using Assets.Scripts.Data;
using BayatGames.SaveGameFree;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Model
    {
        private Tile[] _grid;

        public Model()
        {
            _grid = new Tile[Config.WIDTH * Config.HEIGHT];

            CreateGrid();
        }

        private void CreateGrid()
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

        public void SaveProgress()
        {
            SaveGame.Save<int>("levelsCompleted", Progress.LevelsCompleted);

            SaveGame.Save<int[]>("achs", Progress.Achievements);
            SaveGame.Save<int[]>("achParams", Progress.AchievementParams);
            SaveGame.Save<int[]>("deadOnLevel", Progress.DeadOnLevel);

            int[] stars = new int[60];
            int i = 0;
            foreach (int star in Progress.StarsAllLevels)
            {
                stars[i] = star;
                i++;
            }
            SaveGame.Save<int[]>("starsAllLevels", stars);
        }

        public void LoadProgress()
        {
            //SaveGame.DeleteAll();
            if (!SaveGame.Exists("levelsCompleted"))//first load
                return;

            Progress.LevelsCompleted = SaveGame.Load<int>("levelsCompleted");
            SoundManager.GetInstance().HasMusic = SaveGame.Load<bool>("isMusic");//save in SoundManager

            Progress.Achievements = SaveGame.Load<int[]>("achs");
            Progress.AchievementParams = SaveGame.Load<int[]>("achParams");
            Progress.DeadOnLevel = SaveGame.Load<int[]>("deadOnLevel");

            int[] stars = SaveGame.Load<int[]>("starsAllLevels");
            int k = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Progress.StarsAllLevels[i, j] = stars[k];
                    k++;
                }
            }
        }

        public Tile[] Grid
        {
            get { return _grid; }
        }
    }
}
