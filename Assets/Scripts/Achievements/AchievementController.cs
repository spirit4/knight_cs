using Assets.Scripts.Data;
using Assets.Scripts.Events;
using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Achievements
{
    public class AchievementController
    {
        private GameObject _icon;
        Sequence _mySequence;
        private Achievement[] _achievements;

        public AchievementController(Component container, AchConfig config)
        {
            Init(container);
            CreateLists(config);

            GameEvents.AchTriggeredHandlers += IncrementTrigger;
        }

        private void Init(Component container)
        {
            _icon = new GameObject("ach");
            _icon.AddComponent<SpriteRenderer>();
            _icon.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            _icon.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _icon.transform.SetParent(container.gameObject.transform);
            _icon.transform.localPosition = new Vector3(2.45f, 1.16f);

            Color color = _icon.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            _icon.GetComponent<SpriteRenderer>().color = color;
            _icon.SetActive(false);
        }

        private void CreateLists(AchConfig config)
        {
            _achievements = new Achievement[config.Achievements.Length];
            AchInput aData;
            for (int i = 0; i < Progress.Achievements.Length; i++)
            {
                if (!Progress.isUnlocked(i))
                {
                    aData = config.Achievements[i];
                    _achievements[i] = new Achievement(aData.Type, aData.IsAnd, aData.Icon);
                    _achievements[i].Init(aData.Triggers);
                    _achievements[i].UnlockingHandler += Unlock;
                }
            }
        }

        private void Unlock(Achievement ach)
        {
            Debug.Log($"Unlock {ach.Type}");
            ach.UnlockingHandler -= Unlock;

            Progress.Achievements[ach.TypeInt] = 1;
            _achievements[ach.TypeInt] = null;

            ShowAchievement(ach.Icon);
            ach.Destroy();
        }

        private void IncrementTrigger(Trigger.TriggerType type)
        {
            StoreTrigger(type);

            foreach (var ach in _achievements)
            {
                if (ach == null)//unlocked
                    continue;

                if (ach.Triggers.Count == 0)
                    throw new System.Exception($"ADD a TRIGGER in AchConfig to: {ach.Type}");

                foreach (var t in ach.Triggers)
                {
                    if (type == t.Type)
                    {
                        t.CurrentValue++;
                        break;
                    }
                }
            }
        }

        private void StoreTrigger(Trigger.TriggerType trigger)
        {
            Progress.AchTriggers[(int)trigger]++;

            if (trigger == Trigger.TriggerType.HeroDeadByMonster ||
                trigger == Trigger.TriggerType.HeroDeadByArrow ||
                trigger == Trigger.TriggerType.HeroDeadByTrap)
            {
                Progress.DeadOnLevel[Progress.CurrentLevel]++;
                Debug.Log($"StoreDeaths {Progress.DeadOnLevel[Progress.CurrentLevel]}");
            }
        }

        private void ShowAchievement(Sprite icon)
        {
            if (_icon == null)//game restart
                return;

            if (_icon.activeSelf)
                RemoveAchievement();

            _icon.GetComponent<SpriteRenderer>().sprite = icon;
            _icon.SetActive(true);

            _mySequence = DOTween.Sequence();
            _mySequence.SetEase(Ease.InOutQuad);
            _mySequence.Append(_icon.transform.DOScale(1.5f, 0.4f));
            _mySequence.Join(_icon.GetComponent<SpriteRenderer>().DOFade(1, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(0.8f, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(1.3f, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(0.6f, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(1f, 0.4f));
            _mySequence.AppendCallback(HideAchievement);
        }

        private void HideAchievement()
        {
            _icon.GetComponent<SpriteRenderer>().DOFade(0, 0.4f).SetEase(Ease.InOutQuad);
            _icon.transform.DOScale(0.3f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(RemoveAchievement);
        }

        private void RemoveAchievement()
        {
            _mySequence.Kill();
            if (_icon == null)
                return;

            Color color = _icon.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            _icon.GetComponent<SpriteRenderer>().color = color;
            _icon.SetActive(false);
        }

        public void Destroy()
        {
            GameEvents.AchTriggeredHandlers -= IncrementTrigger;
            RemoveAchievement();

            for (int i = 0; i < _achievements.Length; i++)
            {
                _achievements[i]?.Destroy();
                _achievements[i] = null;
            }

            _icon = null;
            _mySequence = null;
            _achievements = null;
        }
    }
}
