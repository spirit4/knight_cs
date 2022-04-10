using Assets.Scripts.Core;
using Assets.Scripts.Data;
using BayatGames.SaveGameFree;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Saver
    {
        public static void SaveProgress() //TODO not forget
        {
            //SaveGame.Save<int>("LevelsCompleted", Progress.LevelsCompleted);

            //SaveGame.Save<int[]>("Achievements", Progress.Achievements);
            //SaveGame.Save<int[]>("AchTriggers", Progress.AchTriggers);
            //SaveGame.Save<int[]>("DeadOnLevel", Progress.DeadOnLevel);

            //int[] stars = new int[60];
            //int i = 0;
            //foreach (int star in Progress.StarsAllLevels)
            //{
            //    stars[i] = star;
            //    i++;
            //}
            //SaveGame.Save<int[]>("StarsAllLevels", stars);
        }

        public static void LoadProgress()
        {
            //SaveGame.DeleteAll();
            //if (!SaveGame.Exists("LevelsCompleted"))//first load
            //    return;

            //Progress.LevelsCompleted = SaveGame.Load<int>("LevelsCompleted");
            //SoundManager.Instance.HasMusic = SaveGame.Load<bool>("isMusic");//save in SoundManager

            //Progress.Achievements = SaveGame.Load<int[]>("Achievements");
            //Progress.AchTriggers = SaveGame.Load<int[]>("AchTriggers");
            //Progress.DeadOnLevel = SaveGame.Load<int[]>("DeadOnLevel");

            //int[] stars = SaveGame.Load<int[]>("StarsAllLevels");
            //int k = 0;
            //for (int i = 0; i < 20; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        Progress.StarsAllLevels[i, j] = stars[k];
            //        k++;
            //    }
            //}
        }
    }

}