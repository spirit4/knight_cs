using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Achievements
{
    //[CreateAssetMenu(menuName = "Achievements")] //no need after creation
    public sealed class AchConfig : ScriptableObject
    {

        [SerializeField]
        private AchInput[] _achievements;
        public AchInput[] Achievements { get => _achievements; }


        //void OnEnable()//Needed that once, after it'll just reset all input data
        //{
        //    Debug.Log("AchConfig Awake");
        //    string[] achTypes = System.Enum.GetNames(typeof(Achievement.AchType));
        //    _achievements = new AchInput[achTypes.Length];

        //    for (int i = 0; i < achTypes.Length; i++)
        //    {
        //        _achievements[i] = new AchInput(i);
        //    }
        //}

        private void OnValidate()
        {
            foreach (var a in _achievements)
            {
                foreach (var t in a.Triggers)
                {
                    t.Update();
                }
            }
        }
    }

    [System.Serializable]
    public class AchInput
    {
        [SerializeField] [HideInInspector] private string _name;
        [SerializeField] private List<TriggerInput> _triggers;
        [SerializeField] private bool _isAnd = true; //&& or ||
        [SerializeField] private Sprite _icon;

        private Achievement.AchType _type;

        public List<TriggerInput> Triggers { get => _triggers; }
        public Sprite Icon { get => _icon; }
        public bool IsAnd { get => _isAnd; }
        public Achievement.AchType Type { get => _type; }

        public AchInput(int i)
        {
            _type = (Achievement.AchType)i;
            _name = _type.ToString();
        }
    }

    [System.Serializable]
    public class TriggerInput
    {

        [SerializeField] [HideInInspector] private string _name;
        [SerializeField] private Trigger.TriggerType _type;
        [SerializeField] private int _maxValue = 1;

        public Trigger.TriggerType Type { get => _type; }
        public int MaxValue { get => _maxValue; }

        public void Update()
        {
            _name = _type.ToString();
        }
    }
}