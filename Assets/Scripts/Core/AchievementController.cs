using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class AchievementController
    {
        //param actions
        public const int HERO_DEAD_BY_MONSTER = 0;
        public const int HERO_DEAD_BY_ARROW = 1;
        public const int HERO_DEAD_BY_TRAP = 2;
        public const int MONSTER_DEAD = 3;
        public const int LEVEL_WITHOUT_ITEMS = 4;
        public const int MILL_LAUNCHED = 5;
        public const int HELMET_TAKED = 6;
        public const int SHIELD_TAKED = 7;
        public const int SWORD_TAKED = 8;
        public const int LEVEL_WITHOUT_DEATH = 9;
        public const int AWAY_FROM_ARROW = 10;

        private enum AchType : int
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

        private GameObject _icon;
        Sequence _mySequence;

        private static AchievementController _instance;
        public static AchievementController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AchievementController();

                return _instance;
            }
        }
        private AchievementController()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene current)
        {
            RemoveAchievement();
            _icon = null;
        }

        public void AddParam(int type)
        {
            
            Progress.AchievementParams[type]++;

            if (type == AchievementController.HERO_DEAD_BY_ARROW ||
                type == AchievementController.HERO_DEAD_BY_MONSTER ||
                type == AchievementController.HERO_DEAD_BY_TRAP)
            {
                Progress.DeadOnLevel[Progress.CurrentLevel]++;
            }
            
            CheckAchievements(type);
        }


        private void CheckAchievements(int currentEvent)
        {
            AchType type;
            int[] achs = Progress.Achievements;
            for (int i = 0; i < achs.Length; i++)
            {
                if (achs[i] == 1)
                    continue;

                type = (AchType)i;
                
                switch (type)
                {
                    case AchType.CatchArrow when Progress.AchievementParams[AchievementController.HERO_DEAD_BY_ARROW] > 0:
                    case AchType.KillMonster when Progress.AchievementParams[AchievementController.MONSTER_DEAD] > 0:
                    case AchType.NoTakeItems when Progress.AchievementParams[AchievementController.LEVEL_WITHOUT_ITEMS] > 0:
                    case AchType.TenHelmets when Progress.AchievementParams[AchievementController.HELMET_TAKED] >= 10:
                    case AchType.TenShields when Progress.AchievementParams[AchievementController.SHIELD_TAKED] >= 10:
                    case AchType.TenSwords when Progress.AchievementParams[AchievementController.SWORD_TAKED] >= 10:
                    case AchType.StepOnTrap when Progress.AchievementParams[AchievementController.HERO_DEAD_BY_TRAP] > 0:
                    case AchType.LaunchMill when Progress.AchievementParams[AchievementController.MILL_LAUNCHED] > 0:
                    case AchType.FastReaction when Progress.AchievementParams[AchievementController.AWAY_FROM_ARROW] > 0:
                    case AchType.TenLevelsWithoutDeath when Progress.AchievementParams[AchievementController.LEVEL_WITHOUT_DEATH] >= 10:
                        achs[i] = 1;
                        LetAchievement(i);
                        
                        break;

                    case AchType.TakeAllItems:
                        if (Progress.AchievementParams[AchievementController.HELMET_TAKED] >= 20 &&
                            Progress.AchievementParams[AchievementController.SWORD_TAKED] >= 20 &&
                            Progress.AchievementParams[AchievementController.SHIELD_TAKED] >= 20)
                        {
                            achs[i] = 1;
                            this.LetAchievement(i);
                        }
                        break;

                    case AchType.FirstDeath:
                        
                        if (Progress.AchievementParams[AchievementController.HERO_DEAD_BY_ARROW] > 0 ||
                            Progress.AchievementParams[AchievementController.HERO_DEAD_BY_MONSTER] > 0 ||
                            Progress.AchievementParams[AchievementController.HERO_DEAD_BY_TRAP] > 0)
                        {
                            achs[i] = 1;
                            LetAchievement(i);
                        }
                        break;
                }
            }
        }

        private void LetAchievement(int type)
        {
            if (_icon == null)
                return;

            if (_icon.activeSelf)
                RemoveAchievement();

            _icon.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.ICON_ACH + (type + 1).ToString());
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

        public void Init(Component container)
        {
            _icon = new GameObject("ach");
            _icon.AddComponent<SpriteRenderer>();
            _icon.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.ICON_ACH + 1);
            _icon.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            _icon.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _icon.transform.SetParent(container.gameObject.transform);
            _icon.transform.localPosition = new Vector3(2.45f, 1.16f);

            Color color = _icon.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            _icon.GetComponent<SpriteRenderer>().color = color;
            _icon.SetActive(false);
        }
    }
}
