using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Achievements
{
    public class Trigger
    {
        public enum TriggerType : int
        {
            HeroDeadByMonster,
            HeroDeadByArrow,
            HeroDeadByTrap,
            MonsterDead,
            LevelWithoutItems,
            MillLaunched,
            HelmetTaked,
            ShieldTaked,
            SwordTaked,
            LevelWithoutDeath,
            AwayFromArrow
        }

        private TriggerType _type;
        private int _maxValue;
        private int _currentValue;

        public int TypeInt
        {
            get
            {
                return (int)_type;
            }
        }
        public TriggerType Type
        {
            get
            {
                return _type;
            }
        }

        public Trigger(TriggerType type, int maxValue, int currentValue)
        {
            _type = type;
            _maxValue = maxValue;
            _currentValue = currentValue;
        }

        public event Action<Trigger> TriggerHandler = delegate { };
        public int CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                _currentValue = value;
                //Debug.Log($"Trigger {_type} _currentValue {_currentValue} _maxValue {_maxValue}");
                if (_currentValue >= _maxValue)
                    TriggerHandler.Invoke(this);
            }
        }
    }
}
