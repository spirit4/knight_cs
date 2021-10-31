using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Events
{
    public class GameEvent
    {
        //       static GOTO_LEVEL: string = "gotoLevel";
        //  public static UPDATE: string = "tick";

        //  public static COMPLETE: string = "complete";
          public const string ANIMATION_COMPLETE = "animationEnded";
        //  public static PROGRESS: string = "progress";

        //  public static LEVEL_COMPLETE: string = "levelComplete";
        //  public static RESTART: string = "restart";
        //  public static COLLISION: string = "collision";
        //  public static EDITOR_ON_OFF: string = "editorOnOff";

        public const string HERO_REACHED = "heroReached";
        public const string HERO_ONE_CELL_AWAY = "heroOneCellAway";
        //  public static HERO_GET_TRAP: string = "heroGetTrap";

        //public index: number;
        //public objectType: string;

        public GameEvent()// string type, bool bubbles = false, bool cancelable = false)
        {
            //base(type, bubbles, cancelable);
        }
    }
}