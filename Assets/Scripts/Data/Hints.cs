using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class Hints
    {
        /** <summary>level: path</summary> */
        public static readonly Dictionary<int, string> HintImages = new Dictionary<int, string>()
        {
            { 0, "images/ui/Hints/help1_1" },
            { -1, "images/ui/Hints/help1_2" },
            { 1, "images/ui/Hints/help2" },
            { 2, "images/ui/Hints/help3" },
            { 7, "images/ui/Hints/help7" }
        };
    }
}
