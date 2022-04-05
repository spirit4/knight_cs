using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Units;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Display;
using com.ootii.Messages;
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
        private MillPress _mill;

        private Pathfinder _pathfinder;

        private Dictionary<int, ICollidable> _units;
        private List<IActivatable> _items;

        private GameObject _help;

        private TargetMark _targetMark;
        private List<GameObject> _poolPoints = new List<GameObject>();
        private List<GameObject> _activePoints = new List<GameObject>();
        private GameObject _boom;

        private bool _isStartArrowCheck = false;


        private void Awake()
        {
            var managerBg = new ManagerBg(this);

            JSONRes.Init();
            new Model();
            _grid = Model.Grid;

            //Debug.Log("Game Awake");
            this.gameObject.isStatic = true;

            DOTween.Init();
            DOTween.Clear();

            AchievementController.Instance.Init(this);

            _level = new Level(this, managerBg);
            _hero = _level.Hero;

            _pathfinder = new Pathfinder(Config.WIDTH, Config.HEIGHT);

            MessageDispatcher.AddListener(GameEvent.HERO_REACHED, ReachedHandler);
            MessageDispatcher.AddListener(GameEvent.HERO_GET_TRAP, GetTrapHandler);
            MessageDispatcher.AddListener(GameEvent.HERO_ONE_CELL_AWAY, HideLastPoint);

            MessageDispatcher.AddListener(GameEvent.QUIT, Destroy);//leaving game, from UIManager

            _targetMark = new TargetMark(this.gameObject);

            _units = _level.Units;
            _mill = _level.Mill;
            _items = _level.Items;

            Text level = GameObject.Find("Canvas/PanelGameUI/ImageSpear1/LevelBoard/TextLevel").GetComponent<Text>();
            level.text = (Progress.CurrentLevel + 1).ToString();

            CreateHint(true);
        }


        private void GetTrapHandler(IMessage rMessage)
        {
            MessageDispatcher.RemoveListener(GameEvent.HERO_GET_TRAP, GetTrapHandler);
            WaitAndCall(100, HideActors, null, true);
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

            _pathfinder.Init(_grid); //clearing and rechecking the grid
            _pathfinder.FindPath(_hero.Index, index);

            if (_pathfinder.Path.Count > 0)
            {
                List<int> path = _pathfinder.Path;
                if (_grid[index].IsContainType(ImagesRes.MILL))
                {
                    _mill.Activate();
                    path.RemoveAt(path.Count - 1);
                }

                if (path.Count > 0)
                {
                    RemoveHint();

                    _targetMark.PlaceByTap(index);
                    ShowPath(_pathfinder.Path);

                    TowerArrow arrow;
                    foreach (var pair in _units)
                    {

                        if (!(pair.Value is TowerArrow))
                            continue;

                        arrow = pair.Value as TowerArrow;
                        if (GridUtils.GetPoint(arrow.Index).y == GridUtils.GetPoint(_hero.Index).y && arrow.IsShooted())
                        {
                            _isStartArrowCheck = true;
                            break;
                        }
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
                if (!(_items[i] is Tower))
                {
                    _items[i].Activate();
                }
            }
        }

        private void ReachedHandler(IMessage rMessage)
        {
            if (_isStartArrowCheck)
            {
                AchievementController.Instance.AddParam(AchievementController.AWAY_FROM_ARROW);
                _isStartArrowCheck = false;
            }

            HidePoints();

            if (Progress.CurrentLevel == 0 && _hero.Index == 54) //to guide
                CreateHintAfterAction();


            if (GridUtils.FindAround(_grid, _hero.Index, ImagesRes.MILL) != -1)
            {
                if (_mill.State != Unit.ON)
                {
                    _mill.StartRotateMill();
                    ActivateItems();
                }
            }
        }

        private void RestartHandler()
        {
            MessageDispatcher.SendMessage(GameEvent.RESTART);
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

            _help = new GameObject("Help");
            _help.AddComponent<SpriteRenderer>();
            _help.GetComponent<SpriteRenderer>().sprite = sprite;
            _help.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            _help.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _help.transform.SetParent(this.gameObject.transform);
            _help.transform.localPosition = new Vector3(2.4f, -1.9f); //TODO consts
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
                CheckCollision(_units, 0.25f, 0.25f, 0.25f, 0.15f);//TODO maybe need to make arrow more narrow
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
                                AchievementController.Instance.AddParam(AchievementController.HERO_DEAD_BY_MONSTER);
                            else if (pair.Value is TowerArrow)
                                AchievementController.Instance.AddParam(AchievementController.HERO_DEAD_BY_ARROW);

                            WaitAndCall(100, HideActors, pair.Value, true);
                            _hero.HeroState = Hero.DEATH;
                            ShowBoom();
                        }
                        else if (pair.Value is Monster)
                        {
                            AchievementController.Instance.AddParam(AchievementController.MONSTER_DEAD);

                            WaitAndCall(100, HideActors, pair.Value, false); //killing the monster
                            ShowBoom(ImagesRes.A_ATTACK_BOOM);
                        }
                        break;
                    }
                }
            }
        }

        private void HideActors(ICollidable unit, bool IsEnd)
        {
            if (unit != null)
            {
                unit.Stop();
                _hero.Stop();
                unit.View.SetActive(false);
                unit.Destroy();
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
            MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.A_BOOM, BoomCompleteHandler, false);
        }

        public void BoomCompleteHandler(IMessage rMessage)
        {
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.A_BOOM, BoomCompleteHandler);

            _boom.SetActive(false);

            if (_hero == null || !_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword)
                RestartHandler();
        }

        private void ShowPath(List<int> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (_poolPoints.Count < path.Count)
                {
                    this.CreatePathPoint();
                }
                WaitAndCall(50 * i, AppearPoint, path[i]);
            }
        }

        private void AppearPoint(int index)
        {
            GameObject bitmap = _poolPoints[_poolPoints.Count - 1];
            _poolPoints.RemoveAt(_poolPoints.Count - 1);

            bitmap.transform.localScale = new Vector3(0, 0);
            bitmap.transform.localPosition = new Vector3(_grid[index].X, _grid[index].Y, 0);
            bitmap.SetActive(true);
            _activePoints.Add(bitmap);

            bitmap.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuart).OnComplete(() => ReducePoint(bitmap.transform));
        }

        private void ReducePoint(Transform transform)
        {
            transform.DOScale(1, 0.06f).SetEase(Ease.InQuart);
        }

        private void HidePoints()
        {
            while (_activePoints.Count > 0)
            {
                GameObject bitmap = _activePoints[_activePoints.Count - 1];
                _activePoints.RemoveAt(_activePoints.Count - 1);
                bitmap.SetActive(false);
                _poolPoints.Add(bitmap);
            }
        }

        private void HideLastPoint(IMessage rMessage)
        {
            if (_activePoints.Count == 0)
                return;

            GameObject bitmap = _activePoints[0];
            _activePoints.RemoveAt(0);
            bitmap.SetActive(false);
            _poolPoints.Add(bitmap);
        }

        private void CreatePathPoint()
        {
            GameObject dObject = new GameObject();
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.TARGET_MARK + 0);
            dObject.transform.SetParent(this.gameObject.transform);
            dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            dObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            dObject.SetActive(false);

            _poolPoints.Add(dObject);
        }


        private void Destroy(IMessage rMessage = null)
        {
            MessageDispatcher.RemoveListener(GameEvent.QUIT, Destroy);
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.A_BOOM, BoomCompleteHandler);
            MessageDispatcher.RemoveListener(GameEvent.HERO_REACHED, ReachedHandler);
            MessageDispatcher.RemoveListener(GameEvent.HERO_ONE_CELL_AWAY, HideLastPoint);
            MessageDispatcher.RemoveListener(GameEvent.HERO_GET_TRAP, GetTrapHandler);

            if (_hero != null)
                _hero.Destroy();

            RemoveHint();

            _pathfinder.Destroy();
            _pathfinder = null;

            //TODO clear grid here?

            if (_level != null)
            {
                _level.Destroy();
                _level = null;
            }

            _units = null;
            _items = null;

            _targetMark = null;
            _poolPoints.Clear();
            _poolPoints = null;
            _activePoints.Clear();
            _activePoints = null;

            _grid = null;

            _hero = null;
            _level = null;

            if (_mill != null)
                _mill.Destroy();

            _mill = null;
        }


        //let it be, althought this shorter:
        //DOTween.Sequence().AppendInterval(delay).AppendCallback(callback);

        /** <summary>delay (ms)</summary> */
        private void WaitAndCall(float delay, Action<int> callback, int index)
        {
            StartCoroutine(ExampleCoroutine(delay, callback, index));
        }
        private IEnumerator ExampleCoroutine(float delay, Action<int> callback, int index)
        {
            yield return new WaitForSeconds(delay / 1000);
            callback(index);
        }

        //overloading
        private void WaitAndCall(float delay, Action<ICollidable, bool> callback, ICollidable unit, bool flag)
        {
            StartCoroutine(ExampleCoroutine(delay, callback, unit, flag));
        }
        private IEnumerator ExampleCoroutine(float delay, Action<ICollidable, bool> callback, ICollidable unit, bool flag)
        {
            yield return new WaitForSeconds(delay / 1000);
            callback(unit, flag);
        }
    }


}
