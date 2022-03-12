using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Events
{
    public class GameEvent
    {
        public const string ANIMATION_COMPLETE = "animationEnded";

        public const string LEVEL_COMPLETE = "levelComplete";
        public const string RESTART = "restart";
        public const string QUIT = "quit";

        public const string HERO_REACHED = "heroReached";
        public const string HERO_ONE_CELL_AWAY = "heroOneCellAway";
        public const string HERO_GET_TRAP = "heroGetTrap";

        public GameEvent()
        {

        }
    }
}