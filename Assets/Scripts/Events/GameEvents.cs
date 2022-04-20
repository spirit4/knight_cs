using Assets.Scripts.Achievements;
using System;

namespace Assets.Scripts.Events
{
    public static class GameEvents 
    {
        //TODO make non static Singleton or Observer\EventAgregator
        //------------------Global Events, simple implementation---------------------------------
        public static event Action HeroReachedHandlers = delegate { };
        public static void HeroReached() { HeroReachedHandlers.Invoke(); }

        public static event Action HeroOneCellAwayHandlers = delegate { };
        public static void HeroOneCellAway() { HeroOneCellAwayHandlers.Invoke(); }

        public static event Action HeroTrappedHandlers = delegate { };
        public static void HeroTrapped() { HeroTrappedHandlers.Invoke(); }


        public static event Action AnimationEndedHandlers = delegate { };
        public static void AnimationEnded() { AnimationEndedHandlers.Invoke(); }

        public static event Action LevelCompleteHandlers = delegate { };
        public static void LevelComplete() { LevelCompleteHandlers.Invoke(); }

        public static event Action GameRestartHandlers = delegate { };
        public static void GameRestart() { GameRestartHandlers.Invoke(); }

        public static event Action GameQuitHandlers = delegate { };
        public static void GameQuit() { GameQuitHandlers.Invoke(); }


        public static event Action<Trigger.TriggerType> AchTriggeredHandlers = delegate { };
        public static void AchTriggered(Trigger.TriggerType type) { AchTriggeredHandlers.Invoke(type); }

        public static void Clear()
        {
            HeroReachedHandlers = null;
            HeroOneCellAwayHandlers = null;
            HeroTrappedHandlers = null;

            AnimationEndedHandlers = null;//TODO normal manager with sender object
            LevelCompleteHandlers = null;
            GameRestartHandlers = null;

            //GameQuitHandlers = null;//keeping between scenes

            AchTriggeredHandlers = null;
        }
    }
}