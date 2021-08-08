using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class Hints
    {
        /** <summary>level: [x,y,type]</summary> */
        public static readonly Dictionary<int, int[]> texts = new Dictionary<int, int[]>()
        {
            { 0, new int[3] { 58, 455, 0 } },
            { 1, new int[3] { 200, 280, 2 } },
            { 2, new int[3] { 166, 520, 3 } },
            { 7, new int[3] { 66, 520, 4 } }
        };

        ///**{x0,y0},{x1,y1}...*/
        //static holes: {[index: number]: Object[]; } = {
        //    0: [{ x: 82, y: 542, r: 35 }],
        //        1: [{ x: 142, y: 243, r: 35 }, { x: 383, y: 663, r: 35 }, { x: 562, y: 365, r: 35 }, { x: 442, y: 183, r: 30 }, { x: 442, y: 243, r: 30 }, { x: 442, y: 303, r: 30 }, { x: 442, y: 363, r: 30 }],
        //        2: [{ x: 262, y: 642, r: 70 }],
        //        7: [{ x: 142, y: 603, r: 35 }]
        //    };

        ///**x,y,type*/
        //static textsAfterAction: {[index: number]: number[]; } = {
        //    0: [240, 344, 1]
        //    };

        ///**{x0,y0},{x1,y1}...*/
        //static holesAfterAction: {[index: number]: Object[]; } = {
        //    0: [{ x: 263, y: 482, r: 45 }, { x: 502, y: 242, r: 35 }, { x: 442, y: 484, r: 35 }]
        //    };
    }
}
