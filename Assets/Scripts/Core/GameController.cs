using Assets.Scripts.Achievements;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Units;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Display;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
    /** <summary>script of GameContainer</summary> */
    public class GameController : MonoBehaviour
    {
        private Tile[] _grid;

        private Level _level;
        private Hero _hero;
        //private MillPress _mill;

        private PathKeeper _path;
        private AchievementController _achController;

        private Dictionary<int, ICollidable> _units;
        private List<IActivatable> _items;

        private GameObject _help;
        private TargetMark _targetMark;
        private GameObject _boom;

        private bool _isStartArrowCheck = false;

        [SerializeField] private AchConfig _achConfig;
        [SerializeField] private EntityConfig _entityConfig;

        private void Awake()
        {
            JSONRes.Init();
            new Model();
            _grid = Model.Grid;

            //Debug.Log("Game Awake");
            this.gameObject.isStatic = true;

            DOTween.Init();
            DOTween.Clear();

            Debug.Log("GameController Awake");
            _achController = new AchievementController(this, _achConfig);
            _path = new PathKeeper();

            _level = new Level(this.transform, _entityConfig);
            _hero = _level.Hero;

            GameEvents.HeroReachedHandlers += HeroReachedHandler;
            GameEvents.HeroTrappedHandlers += GetTrapHandler;
            GameEvents.HeroOneCellAwayHandlers += _path.HideLastPoint;

            GameEvents.GameQuitHandlers += Destroy;//leaving game, from UIManager

            _targetMark = new TargetMark(this.gameObject);

            _units = _level.Units;
            //_mill = _level.Mill;
            _items = _level.Items;

            Text level = GameObject.Find("Canvas/PanelGameUI/ImageSpear1/LevelBoard/TextLevel").GetComponent<Text>();
            level.text = (Progress.CurrentLevel + 1).ToString();

            CreateHint(true);
        }

        private void GetTrapHandler()
        {
            StartCoroutine(HideActors(0.1f, null, true));
            ShowBoom();
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (_hero.HeroState != Hero.IDLE)
            {
                _hero.Stop();
                return;
            }

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            int index = GridUtils.GetIndex(point.x, point.y);

            if (_hero.Index == index)
                return;

            if (_grid[index].IsWall)
                return;

            _path.Pathfinder.Init(_grid); //clearing and rechecking the grid
            _path.Pathfinder.FindPath(_hero.Index, index);

            if (_path.Pathfinder.Path.Count > 0)
            {
                List<int> path = _path.Pathfinder.Path;
                if (_grid[index].IsContainType(ImagesRes.MILL))
                {
                    (_grid[index].GetEntity(ImagesRes.MILL) as MillPress).Activate();
                    path.RemoveAt(path.Count - 1);
                }

                if (path.Count > 0)
                {
                    RemoveHint();

                    _targetMark.PlaceByTap(index);
                    _path.ShowPath(this.transform, _level.Creator);

                    TowerArrow arrow;
                    foreach (var pair in _units)
                    {

                        if (!(pair.Value is TowerArrow))
                            continue;

                        arrow = pair.Value as TowerArrow;
                        //TODO position instead of index?
                        //if (GridUtils.GetPoint(arrow.Index).y == GridUtils.GetPoint(_hero.Index).y && arrow.IsShooted())
                        //{
                        //    _isStartArrowCheck = true;
                        //    break;
                        //}
                    }

                    _hero.MoveToCell(path);
                }
            }
        }

        public void ActivateItems()
        {
            int len = _items.Count;
            for (int i = 0; i < len; i++)
            {
                _items[i].Activate();
            }
        }

        private void HeroReachedHandler()
        {
            if (_isStartArrowCheck)
            {
                GameEvents.AchTriggered(Trigger.TriggerType.AwayFromArrow);
                _isStartArrowCheck = false;
            }

            _path.HidePoints();

            if (Progress.CurrentLevel == 0 && _hero.Index == 54) //to guide
                CreateHintAfterAction();

            int millIndex = GridUtils.FindAround(_grid, _hero.Index, ImagesRes.MILL);

            if (millIndex != -1)
            {
                MillPress mill = _grid[millIndex].GetEntity(ImagesRes.MILL) as MillPress;
                if (!mill.IsActive)
                {
                    mill.StartRotateMill();
                    ActivateItems();
                }
            }
        }

        private void RestartHandler()
        {
            GameEvents.GameRestart();
        }

        private void CreateHint(bool isFirst)
        {
            if (!Hints.HintImages.ContainsKey(Progress.CurrentLevel))
                return;

            int key;
            if (isFirst)
                key = Progress.CurrentLevel;
            else
                key = -1;

            Sprite sprite = Resources.Load<Sprite>(Hints.HintImages[key]); //TODO move it

            _help = new GameObject("Help");//TODO add to factory
            _help.AddComponent<SpriteRenderer>();
            _help.GetComponent<SpriteRenderer>().sprite = sprite;
            _help.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            _help.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _help.transform.SetParent(this.gameObject.transform);
            _help.transform.localPosition = new Vector3(2.4f, -1.9f);
        }

        public void CreateHintAfterAction()
        {
            CreateHint(false);//second hint
        }

        private void RemoveHint()
        {
            if (_help != null)
            {
                Destroy(_help);
                _help = null;
            }
        }

        public void Update()
        {
            if (_units != null)
                CheckCollision(_units, 0.25f, 0.25f, 0.25f, 0.15f);
        }

        public void CheckCollision(Dictionary<int, ICollidable> vector, float w1, float w2, float h1, float h2)
        {
            GameObject dObject;

            float heroX = _hero.View.transform.localPosition.x;
            float heroY = _hero.View.transform.localPosition.y + 0.15f;//magic number
            float objX;
            float objY;

            foreach (KeyValuePair<int, ICollidable> pair in vector)
            {
                dObject = pair.Value.View;

                objX = dObject.transform.localPosition.x;
                objY = dObject.transform.localPosition.y;

                if ((heroX + w1 >= objX - w2 && heroX - w1 <= objX + w2) && (heroY - h1 <= objY + h2 && heroY + h1 >= objY - h2))
                {
                    if (_hero.HeroState != Hero.DEATH)
                    {
                        Monster monster = pair.Value as Monster;

                        vector.Remove(pair.Key);

                        if (!_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword)
                        {
                            if (pair.Value is Monster)
                                GameEvents.AchTriggered(Trigger.TriggerType.HeroDeadByMonster);
                            else if (pair.Value is TowerArrow)
                                GameEvents.AchTriggered(Trigger.TriggerType.HeroDeadByArrow);

                            StartCoroutine(HideActors(0.1f, pair.Value, true));
                            _hero.HeroState = Hero.DEATH;
                            ShowBoom();
                        }
                        else if (pair.Value is Monster)
                        {
                            GameEvents.AchTriggered(Trigger.TriggerType.MonsterDead);

                            StartCoroutine(HideActors(0.1f, pair.Value, false)); //killing the monster
                            ShowBoom(ImagesRes.A_ATTACK_BOOM);
                        }
                        break;
                    }
                }
            }
        }

        private IEnumerator HideActors(float delay, ICollidable unit, bool IsEnd)
        {
            yield return new WaitForSeconds(delay);

            if (unit != null)
            {
                unit.Stop();
                _hero.Stop();
                unit.View.SetActive(false);// TODO withdraw
                (unit as IDestroyable).Destroy();
            }

            if (IsEnd)
                _hero.View.SetActive(false);
        }

        private void ShowBoom(string boomType = ImagesRes.A_BOOM)
        {
            float boomX = _hero.View.transform.localPosition.x;
            float boomY = _hero.View.transform.localPosition.y + 0.25f;

            if (_boom == null)
            {
                GameObject gameObject = GameObject.Instantiate(ImagesRes.Prefabs[boomType], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                gameObject.transform.SetParent(this.gameObject.transform);

                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 200;//works fine
                _boom = gameObject;
                _boom.name = ImagesRes.A_BOOM;
            }

            _boom.transform.localPosition = new Vector3(boomX, boomY);
            _boom.SetActive(true);

            GameEvents.AnimationEndedHandlers += BoomCompleteHandler;
        }

        public void BoomCompleteHandler()
        {
            GameEvents.AnimationEndedHandlers -= BoomCompleteHandler;

            _boom.SetActive(false);

            if (_hero == null || !_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword)//TODO flags?
                RestartHandler();
        }

        private void Destroy()
        {
            //Debug.Log("GameController Destroy");
            GameEvents.GameQuitHandlers -= Destroy;//manual
            GameEvents.Clear();//auto

            if (_hero != null)
                _hero.Destroy();

            RemoveHint();

            _path.Destroy();
            _path = null;

            _achConfig = null;
            _achController.Destroy();
            _achController = null;

            //TODO clear grid here?

            if (_level != null)
            {
                _level.Destroy();
                _level = null;
            }

            _units = null;
            _items = null;

            _targetMark = null;
            _grid = null;

            _hero = null;
            _level = null;

            //if (_mill != null)
            //    _mill.Destroy();

            //_mill = null;
        }

    }
}
