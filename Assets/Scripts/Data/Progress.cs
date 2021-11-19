using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class Progress
    {
        public static int levelsCompleted = 20;
        public static int currentLevel = 0;

        /** <summary>starsAllLevels [level][helmet, shield, sword]</summary> */
        public static int[,] starsAllLevels = new int[20, 3]
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
        public static int[] achs = new int[12]{
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

        public static List<string> hintAchs = new List<string>()
        {
            "Let an arrow \rkill the knight",
            "Obtain the entire outfit \rand kill the beast",
            "Complete a level \rwithout the outfit",
            "Collect the entire outfit \rfrom every level",
            "Find a trap \rand step on it",//5
            "Activate the windmill \rcoming up to it",
            "Collect ten helmets \rfrom any ten levels",
            "The knight has to die ;)",
            "Collect ten shields \rfrom any ten levels",
            "Complete any ten levels \rwithout dying",//10
            "Collect ten swords \rfrom any ten levels",
            "Make a step \rsidewards from the arrow \rafter it's been shot"
        };
    }
}