using Assets.Scripts.Achievements;
using System;

namespace Assets.Scripts.Events
{
    public static class GameEvents //TODO make custom EventAgregator
    {
        public const string ANIMATION_COMPLETE = "animationEnded";

        //public const string LEVEL_COMPLETE = "levelComplete";
        public const string RESTART = "restart";
        public const string QUIT = "quit";

        //public const string HERO_REACHED = "heroReached";
        public const string HERO_ONE_CELL_AWAY = "heroOneCellAway";
        public const string HERO_GET_TRAP = "heroGetTrap";

        //TODO make non static Singleton or Observer
        //------------------Global Events, simple implementation---------------------------------
        public static event Action HeroReachedHandlers = delegate { };
        public static void HeroReached() { HeroReachedHandlers.Invoke(); }


        public static event Action LevelCompleteHandlers = delegate { };
        public static void LevelComplete() { LevelCompleteHandlers.Invoke(); }


        public static event Action<Trigger.TriggerType> AchTriggeredHandlers = delegate { };
        public static void AchTriggered(Trigger.TriggerType type) { AchTriggeredHandlers.Invoke(type); }

        public static void Clear()
        {
            HeroReachedHandlers = null;
            LevelCompleteHandlers = null;
            AchTriggeredHandlers = null;
        }
    }
}