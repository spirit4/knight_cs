using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class Hints
    {
        /** <summary>level: path</summary> */
        public static readonly Dictionary<int, string> HintImages = new ()
        {
            { 0, "Images/GameScene/Hints/help1_1" },
            { -1, "Images/GameScene/Hints/help1_2" },
            { 1, "Images/GameScene/Hints/help2" },
            { 2, "Images/GameScene/Hints/help3" },
            { 7, "Images/GameScene/Hints/help7" }
        };
    }
}
