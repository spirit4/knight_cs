using Assets.Scripts.Data;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class AchController
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


        public static AchController instance;
        private GameObject _icon;
        Sequence _mySequence;

        public AchController()
        {
            AchController.instance = this;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene current)
        {
            //Debug.Log("OnSceneUnloaded: " + 111111);
            removeAchievement();
            //DOTween.KillAll();
            _icon = null;
        }

        public void addParam(int type)
        {
            Progress.achParams[type]++;

            if (type == AchController.HERO_DEAD_BY_ARROW ||
                type == AchController.HERO_DEAD_BY_MONSTER ||
                type == AchController.HERO_DEAD_BY_TRAP)
            {
                Progress.deadOnLevel[Progress.currentLevel]++;
            }
            //Debug.Log("addParam: "+ _icon.GetComponentInParent<Game>());
            checkAchievements(type);
        }


        private void checkAchievements(int currentEvent)
        {
            int[] achs = Progress.achs;
            for (int i = 0; i < achs.Length; i++)
            {
                if (achs[i] == 1)
                    continue;

                switch (i)
                {
                    case AchController.ACH_CATCH_ARROW when Progress.achParams[AchController.HERO_DEAD_BY_ARROW] > 0:
                    case AchController.ACH_KILL_MONSTER when Progress.achParams[AchController.MONSTER_DEAD] > 0:
                    case AchController.ACH_NO_TAKE_ITEMS when Progress.achParams[AchController.LEVEL_WITHOUT_ITEMS] > 0:
                    case AchController.ACH_10_HELMETS when Progress.achParams[AchController.HELMET_TAKED] >= 10:
                    case AchController.ACH_10_SHIELDS when Progress.achParams[AchController.SHIELD_TAKED] >= 10:
                    case AchController.ACH_10_SWORDS when Progress.achParams[AchController.SWORD_TAKED] >= 10:
                    case AchController.ACH_STEP_ON_TRAP when Progress.achParams[AchController.HERO_DEAD_BY_TRAP] > 0:
                    case AchController.ACH_LAUNCH_MILL when Progress.achParams[AchController.MILL_LAUNCHED] > 0:
                    case AchController.ACH_FAST_REACTION when Progress.achParams[AchController.AWAY_FROM_ARROW] > 0:
                    case AchController.ACH_10_LEVELS_WITHOUT_DEATH when Progress.achParams[AchController.LEVEL_WITHOUT_DEATH] >= 10:
                        achs[i] = 1;
                        letAchievement(i);

                        break;

                    case AchController.ACH_TAKE_ALL_ITEMS:
                        if (Progress.achParams[AchController.HELMET_TAKED] >= 20 &&
                            Progress.achParams[AchController.SWORD_TAKED] >= 20 &&
                            Progress.achParams[AchController.SHIELD_TAKED] >= 20)
                        {
                            achs[i] = 1;
                            this.letAchievement(i);
                        }
                        break;

                    case AchController.ACH_FIRST_DEATH:
                        if (Progress.achParams[AchController.HERO_DEAD_BY_ARROW] > 0 ||
                            Progress.achParams[AchController.HERO_DEAD_BY_MONSTER] > 0 ||
                            Progress.achParams[AchController.HERO_DEAD_BY_TRAP] > 0)
                        {
                            achs[i] = 1;
                            this.letAchievement(i);
                        }
                        break;
                }
            }
        }

        private void letAchievement(int type)
        {
            if (_icon == null)
                return;

            if (_icon.activeSelf)
                removeAchievement();

            //Debug.Log("letAchievement: " + _icon);
            _icon.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.ICON_ACH + (type + 1).ToString());
            _icon.SetActive(true);

            _mySequence = DOTween.Sequence();
            _mySequence.SetEase(Ease.InOutQuad);
            _mySequence.Append(_icon.transform.DOScale(1.5f, 0.4f));
            _mySequence.Join(_icon.GetComponent<SpriteRenderer>().DOFade(1, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(0.8f, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(1.3f, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(0.6f, 0.4f));
            _mySequence.Append(_icon.transform.DOScale(1f, 0.4f));
            _mySequence.AppendCallback(hideAchievement);
        }

        private void hideAchievement()
        {
            //Debug.Log("hideAchievement: " + _icon);
            _icon.GetComponent<SpriteRenderer>().DOFade(0, 0.4f).SetEase(Ease.InOutQuad);
            _icon.transform.DOScale(0.3f, 0.4f).SetEase(Ease.InOutQuad).OnComplete(removeAchievement);
        }

        private void removeAchievement()
        {
            _mySequence.Kill();
            //Debug.Log("removeAchievement1: " + _icon);
            if (_icon == null)
                return;

            //Debug.Log("removeAchievement2: " + _icon);
            //_icon.transform.DOKill(true);
            
            //DOTween.Sequence(_icon.GetComponent<SpriteRenderer>()).Kill(true);
            //_icon.GetComponent<SpriteRenderer>().DOKill(true);

            Color color = _icon.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            _icon.GetComponent<SpriteRenderer>().color = color;
            _icon.SetActive(false);
        }

        public void init(Component container)
        {
            //Debug.Log("init: " + _icon);
            _icon = new GameObject("ach");
            _icon.AddComponent<SpriteRenderer>();
            _icon.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.ICON_ACH + 1);
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
