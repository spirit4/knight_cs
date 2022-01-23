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

        public static void saveProgress() //TODO fix static
        {
            SaveGame.Save<int>("levelsCompleted", Progress.levelsCompleted);

            SaveGame.Save<int[]>("achs", Progress.achs);
            SaveGame.Save<int[]>("achParams", Progress.achParams);
            SaveGame.Save<int[]>("deadOnLevel", Progress.deadOnLevel);

            int[] stars = new int[60];
            int i = 0;
            foreach (int star in Progress.starsAllLevels)
            {
                stars[i] = star;
                i++;
            }
            SaveGame.Save<int[]>("starsAllLevels", stars);
        }

        public static void loadProgress()
        {
            //SaveGame.DeleteAll();
            if (!SaveGame.Exists("levelsCompleted"))//first load
                return;

            Progress.levelsCompleted = SaveGame.Load<int>("levelsCompleted");
            SoundManager.getInstance().isMusic = SaveGame.Load<bool>("isMusic");//save in SoundManager

            Progress.achs = SaveGame.Load<int[]>("achs");
            Progress.achParams = SaveGame.Load<int[]>("achParams");
            Progress.deadOnLevel = SaveGame.Load<int[]>("deadOnLevel");

            int[] stars = SaveGame.Load<int[]>("starsAllLevels");
            int k = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Progress.starsAllLevels[i, j] = stars[k];
                    k++;
                }
            }
        }

        public Tile[] grid
        {
            get { return _grid; }
        }
    }
}
