using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

namespace Assets.Scripts.Achievements
{
    public class Achievement
    {
        public enum AchType : int
        {
            CatchArrow,
            KillMonster,
            NoTakeItems,
            TakeAllItems,
            StepOnTrap,
            LaunchMill,
            TenHelmets,
            FirstDeath,
            TenShields,
            TenLevelsWithoutDeath,
            TenSwords,
            FastReaction
        }

        private List<Trigger> _triggers;
        private bool _isAnd = true; //&& or ||
        private Sprite _icon;

        private AchType _achType;

        public List<Trigger> Triggers { get => _triggers; }
        public Sprite Icon { get => _icon; }
        public int TypeInt
        {
            get
            {
                return (int)_achType;
            }
        }
        public AchType Type
        {
            get
            {
                return _achType;
            }
        }

        public Achievement(AchType type, bool isAnd, Sprite icon)
        {
            _achType = type;
            _icon = icon;
            _isAnd = isAnd;
        }

        public void Init(List<TriggerInput> triggers) //only if Unlocked List<Trigger> triggers
        {
            _triggers = new List<Trigger>(triggers.Count);
            TriggerInput tData;
            for (int i = 0; i < triggers.Count; i++)
            {
                tData = triggers[i];
                _triggers.Add(new Trigger(tData.Type, tData.MaxValue, Progress.AchTriggers[(int)tData.Type]));
                _triggers[i].TriggerHandler += MaxReachedHandler;
            }
        }

        public event Action<Achievement> UnlockingHandler = delegate { };
        public void MaxReachedHandler(Trigger trigger)
        {
            trigger.TriggerHandler -= MaxReachedHandler;

                _triggers.Remove(trigger);
                if (!_isAnd) //if one trigger of many is enough
                    _triggers.Clear();

            if (_triggers.Count == 0)
                UnlockingHandler.Invoke(this);
        }

        public void Destroy()
        {
            _triggers.Clear();
            _triggers = null;
            _icon = null;
        }
    }
}
