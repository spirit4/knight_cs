using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class Progress
    {
        public static int LevelsCompleted = 20;
        public static int CurrentLevel = 16;

        /** <summary>starsAllLevels [level][helmet, shield, sword]</summary> */
        public static int[,] StarsAllLevels = new int[20, 3]
        {
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},//5

             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},//10

             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},//15

             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0},
             {0, 0, 0}//20
    };

        /**0 or 1*/
        public static int[] Achievements = new int[12]{
            0,
            0,
            0,
            0,
            0,//5
            
            0,
            0,
            0,
            0,
            0,//10
            
            0,
            0
        };

        public static bool isUnlocked(int type)
        {
            return Convert.ToBoolean(Achievements[type]);
        }

        public static int[] AchTriggers = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] DeadOnLevel = new int[20]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                            0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        public static List<string> HintAchievements = new List<string>()
        {
            "Let an arrow \nkill the knight",
            "Obtain the entire outfit \nand kill the beast",
            "Complete a level \nwithout the outfit",
            "Collect the entire outfit \nfrom every level",
            "Find a trap \nand step on it",//5
            "Activate the windmill \ncoming up to it",
            "Collect ten helmets \nfrom any ten levels",
            "The knight has to die ;)",
            "Collect ten shields \nfrom any ten levels",
            "Complete any ten levels \nwithout dying",//10
            "Collect ten swords \nfrom any ten levels",
            "Make a step \nsidewards from the arrow \nafter it's been shot"
        };
    }
}