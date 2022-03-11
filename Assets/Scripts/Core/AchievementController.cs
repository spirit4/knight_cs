using Assets.Scripts.Data;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class AchievementController
    {
        //achs
        public const int ACH_CATCH_ARROW = 0;
        public const int ACH_KILL_MONSTER = 1;
        public const int ACH_NO_TAKE_ITEMS = 2;
        public const int ACH_TAKE_ALL_ITEMS = 3;

        public const int ACH_STEP_ON_TRAP = 4;
        public const int ACH_LAUNCH_MILL = 5;
        public const int ACH_10_HELMETS = 6;
        public const int ACH_FIRST_DEATH = 7;

        public const int ACH_10_SHIELDS = 8;
        public const int ACH_10_LEVELS_WITHOUT_DEATH = 9;
        public const int ACH_10_SWORDS = 10;
        public const int ACH_FAST_REACTION = 11;

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


        public static AchievementController Instance;
        private GameObject _icon;
        Sequence _mySequence;

        public AchievementController()
        {
            AchievementController.Instance = this;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene current)
        {
            //Debug.Log("OnSceneUnloaded: " + 111111);
            RemoveAchievement();
            //DOTween.KillAll();
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
            //Debug.Log("addParam: "+ _icon.GetComponentInParent<Game>());
            CheckAchievements(type);
        }


        private void CheckAchievements(int currentEvent)
        {
            int[] achs = Progress.Achievements;
            for (int i = 0; i < achs.Length; i++)
            {
                if (achs[i] == 1)
                    continue;

                switch (i)
                {
                    case AchievementController.ACH_CATCH_ARROW when Progress.AchievementParams[AchievementController.HERO_DEAD_BY_ARROW] > 0:
                    case AchievementController.ACH_KILL_MONSTER when Progress.AchievementParams[AchievementController.MONSTER_DEAD] > 0:
                    case AchievementController.ACH_NO_TAKE_ITEMS when Progress.AchievementParams[AchievementController.LEVEL_WITHOUT_ITEMS] > 0:
                    case AchievementController.ACH_10_HELMETS when Progress.AchievementParams[AchievementController.HELMET_TAKED] >= 10:
                    case AchievementController.ACH_10_SHIELDS when Progress.AchievementParams[AchievementController.SHIELD_TAKED] >= 10:
                    case AchievementController.ACH_10_SWORDS when Progress.AchievementParams[AchievementController.SWORD_TAKED] >= 10:
                    case AchievementController.ACH_STEP_ON_TRAP when Progress.AchievementParams[AchievementController.HERO_DEAD_BY_TRAP] > 0:
                    case AchievementController.ACH_LAUNCH_MILL when Progress.AchievementParams[AchievementController.MILL_LAUNCHED] > 0:
                    case AchievementController.ACH_FAST_REACTION when Progress.AchievementParams[AchievementController.AWAY_FROM_ARROW] > 0:
                    case AchievementController.ACH_10_LEVELS_WITHOUT_DEATH when Progress.AchievementParams[AchievementController.LEVEL_WITHOUT_DEATH] >= 10:
                        achs[i] = 1;
                        LetAchievement(i);

                        break;

                    case AchievementController.ACH_TAKE_ALL_ITEMS:
                        if (Progress.AchievementParams[AchievementController.HELMET_TAKED] >= 20 &&
                            Progress.AchievementParams[AchievementController.SWORD_TAKED] >= 20 &&
                            Progress.AchievementParams[AchievementController.SHIELD_TAKED] >= 20)
                        {
                            achs[i] = 1;
                            this.LetAchievement(i);
                        }
                        break;

                    case AchievementController.ACH_FIRST_DEATH:
                        if (Progress.AchievementParams[AchievementController.HERO_DEAD_BY_ARROW] > 0 ||
                            Progress.AchievementParams[AchievementController.HERO_DEAD_BY_MONSTER] > 0 ||
                            Progress.AchievementParams[AchievementController.HERO_DEAD_BY_TRAP] > 0)
                        {
                            achs[i] = 1;
                            this.LetAchievement(i);
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
            //Debug.Log("hideAchievement: " + _icon);
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
            //Debug.Log("init: " + _icon);
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
