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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
    /** <summary>the script of GameContainer (Sprite on Unity GameScene)</summary> */
    public class Game : MonoBehaviour

    {
        private Tile[] _grid;
        private Model _model;

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

        public Game()
        {
            var managerBg = new ManagerBg(this);
            new Controller(managerBg);//singleton

            _model = Controller.instance.model;
            _grid = _model.grid;
        }

        private void Awake()
        {
            this.gameObject.isStatic = true;

            DOTween.Init();
            DOTween.Clear();

            AchController ac = new AchController();//singleton
            ac.init(this);

            _level = new Level(this, _model);
            _hero = _level.hero;

            _pathfinder = new Pathfinder(Config.WIDTH, Config.HEIGHT);

            MessageDispatcher.AddListener(GameEvent.HERO_REACHED, reachedHandler);
            MessageDispatcher.AddListener(GameEvent.HERO_GET_TRAP, getTrapHandler);
            MessageDispatcher.AddListener(GameEvent.HERO_ONE_CELL_AWAY, hideLastPoint);

            MessageDispatcher.AddListener(GameEvent.QUIT, destroy);//leave game, from UIManager

            _targetMark = new TargetMark(this.gameObject);

            _units = _level.units;
            _mill = _level.mill;
            _items = _level.items;

            Text level = GameObject.Find("Canvas/PanelGameUI/Image_spear1/level_board/text_level").GetComponent<Text>();
            level.text = (Progress.currentLevel + 1).ToString();

            createHint(true);
        }


        private void getTrapHandler(IMessage rMessage)
        {
            MessageDispatcher.RemoveListener(GameEvent.HERO_GET_TRAP, getTrapHandler);
            WaitAndCall(100, hideActors, null, true);
            showBoom();
        }


        private void OnMouseDown()
        {

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (_hero.HeroState != Hero.IDLE)
            {
                _hero.stop();
                return;
            }

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            int index = GridUtils.getIndex(point.x, point.y);
            //Debug.Log("[tap tile]: " + point + "   " + index);

            if (_hero.index == index)
                return;

            //    if (Progress.currentLevel == 0 && _hero.index == 10 && index != 54) //to guide
            //    {
            //        return;
            //    }

            //Debug.Log("OnMouseDown " + _grid[index].isWall);
            if (_grid[index].isWall)
                return;

            _pathfinder.Init(_grid); //TODO must be single Init
            _pathfinder.FindPath(_hero.index, index);

            if (_pathfinder.Path.Count > 0)
            {
                List<int> path = _pathfinder.Path;
                if (_grid[index].isContainType(ImagesRes.MILL))
                {
                    _mill.activate();
                    path.RemoveAt(path.Count - 1);
                }

                if (path.Count > 0)
                {
                    removeHint();

                    _targetMark.placeByTap(index);
                    showPath(_pathfinder.Path);

                    TowerArrow arrow;
                    foreach (var pair in _units)
                    {

                        if (!(pair.Value is TowerArrow))
                            continue;

                        arrow = pair.Value as TowerArrow;
                        if (GridUtils.GetPoint(arrow.index).y == GridUtils.GetPoint(_hero.index).y && arrow.isShooted())
                        {
                            _isStartArrowCheck = true;
                            break;
                        }
                    }

                    _hero.moveToCell(path);
                }

            }
        }

        public void activateItems()
        {
            int len = _items.Count;
            for (int i = 0; i < len; i++)
            {
                if (!(_items[i] is Tower))
                {
                    _items[i].activate();
                }
            }
        }

        private void reachedHandler(IMessage rMessage)//e: GameEvent) : void
        {
            if (_isStartArrowCheck)
            {
                //Debug.Log("[+++++reached 2]");
                AchController.instance.addParam(AchController.AWAY_FROM_ARROW);
                _isStartArrowCheck = false;
            }

            hidePoints();

            if (Progress.currentLevel == 0 && _hero.index == 54) //to guide
                createHintAfterAction();


            if (GridUtils.findAround(_grid, _hero.index, ImagesRes.MILL) != -1)
            {
                if (_mill.state != Unit.ON)
                {
                    _mill.startRotateMill();
                    activateItems();
                }
            }
        }

        //gone to Unity Editor
        private void restartHandler()//e: createjs.MouseEvent = null) : void
        {
            //destroy();

            MessageDispatcher.SendMessage(GameEvent.RESTART);
        }

        private void createHint(bool isFirst)
        {
            if (!Hints.hints.ContainsKey(Progress.currentLevel))
                return;

            int key;
            if (isFirst)
                key = Progress.currentLevel;
            else
                key = -1;

            Sprite sprite = Resources.Load<Sprite>(Hints.hints[key]);

            _help = new GameObject("Help");
            _help.AddComponent<SpriteRenderer>();
            _help.GetComponent<SpriteRenderer>().sprite = sprite;
            _help.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            _help.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _help.transform.SetParent(this.gameObject.transform);
            _help.transform.localPosition = new Vector3(2.45f, -1.9f);
        }

        public void createHintAfterAction()
        {
            createHint(false);//second hint
        }

        private void removeHint()
        {
            if (_help != null)
            {
                Destroy(_help);
                _help = null;
            }
        }

        //Bounds m_Collider, m_Collider2;
        public void Update()
        {
            if (_units != null)
                checkCollision(_units, 0.25f, 0.25f, 0.25f, 0.15f);//TODO maybe need to make arrow more narrow
        }

        public void checkCollision(Dictionary<int, ICollidable> vector, float w1, float w2, float h1, float h2)
        {
            GameObject dObject;
            //magic number
            float heroX = _hero.view.transform.localPosition.x;
            float heroY = _hero.view.transform.localPosition.y + 0.15f;
            float objX;
            float objY;

            foreach (KeyValuePair<int, ICollidable> pair in vector)
            {
                dObject = pair.Value.view;

                objX = dObject.transform.localPosition.x;// + Config.SIZE_W * 0.5f;
                objY = dObject.transform.localPosition.y;// + Config.SIZE_H * 0.5f;

                if ((heroX + w1 >= objX - w2 && heroX - w1 <= objX + w2) && (heroY - h1 <= objY + h2 && heroY + h1 >= objY - h2))
                {
                    if (_hero.HeroState != Hero.DEATH)
                    {
                        Monster monster = pair.Value as Monster;

                        vector.Remove(pair.Key);

                        if (!_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword) //(_hero.stateItems != Hero.FULL)
                        {
                            if (pair.Value is Monster)
                                AchController.instance.addParam(AchController.HERO_DEAD_BY_MONSTER);
                            else if (pair.Value is TowerArrow)
                                AchController.instance.addParam(AchController.HERO_DEAD_BY_ARROW);

                            WaitAndCall(100, hideActors, pair.Value, true);
                            _hero.HeroState = Hero.DEATH;
                            showBoom();

                            //    if (Core.instance.api)
                            //    {
                            //        Core.instance.api.gameOver();
                            //    }
                        }
                        else if (pair.Value is Monster)
                        {
                            AchController.instance.addParam(AchController.MONSTER_DEAD);

                            WaitAndCall(100, hideActors, pair.Value, false); //killing the monster
                            showBoom(ImagesRes.A_ATTACK_BOOM);
                        }

                        break;
                    }
                }
            }
        }

        private void hideActors(ICollidable unit, bool IsEnd)
        {
            if (unit != null)
            {
                unit.stop();
                _hero.stop();
                unit.view.SetActive(false);
                unit.destroy();
            }

            if (IsEnd)
                _hero.view.SetActive(false);
        }

        private void showBoom(string boomType = ImagesRes.A_BOOM)
        {
            float boomX = _hero.view.transform.localPosition.x;
            float boomY = _hero.view.transform.localPosition.y + 0.25f;

            if (_boom == null)
            {
                GameObject gameObject = GameObject.Instantiate(ImagesRes.prefabs[boomType], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                gameObject.transform.SetParent(this.gameObject.transform);

                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 200;//TODO sorting2
                _boom = gameObject;
                _boom.name = ImagesRes.A_BOOM;
            }

            _boom.transform.localPosition = new Vector3(boomX, boomY);
            _boom.SetActive(true);
            MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.A_BOOM, boomCompleteHandler, false);
        }

        public void boomCompleteHandler(IMessage rMessage)
        {
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.A_BOOM, boomCompleteHandler);

            _boom.SetActive(false);

            if (_hero == null || !_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword)
                restartHandler();
        }

        private void showPath(List<int> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (_poolPoints.Count < path.Count)
                {
                    this.createPathPoint();
                }
                WaitAndCall(50 * i, appearPoint, path[i]);
            }
        }

        private void appearPoint(int index)
        {
            //Debug.Log("--appearPoint--" + Time.time);
            GameObject bitmap = _poolPoints[_poolPoints.Count - 1];
            _poolPoints.RemoveAt(_poolPoints.Count - 1);

            bitmap.transform.localScale = new Vector3(0, 0);
            bitmap.transform.localPosition = new Vector3(_grid[index].x, _grid[index].y, 0);
            bitmap.SetActive(true);
            _activePoints.Add(bitmap);

            bitmap.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuart).OnComplete(() => reducePoint(bitmap.transform));
        }

        private void reducePoint(Transform transform)
        {
            transform.DOScale(1, 0.06f).SetEase(Ease.InQuart);
        }

        private void hidePoints()
        {
            while (_activePoints.Count > 0)
            {
                GameObject bitmap = _activePoints[_activePoints.Count - 1];
                _activePoints.RemoveAt(_activePoints.Count - 1);
                bitmap.SetActive(false);
                _poolPoints.Add(bitmap);
            }
        }

        private void hideLastPoint(IMessage rMessage)
        {
            if (_activePoints.Count == 0)
                return;

            //if (_isStartArrowCheck)
            //{
            //    //console.log("[+++++reached]");
            //    AchController.instance.addParam(AchController.AWAY_FROM_ARROW);
            //    _isStartArrowCheck = false;
            //}

            //console.log("[CELL AWAY]", _hero.index, _hero.mc.framerate);

            GameObject bitmap = _activePoints[0];
            _activePoints.RemoveAt(0);
            bitmap.SetActive(false);
            _poolPoints.Add(bitmap);
        }

        private void createPathPoint()
        {
            GameObject dObject = new GameObject();
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.TARGET_MARK + 0);
            dObject.transform.SetParent(this.gameObject.transform);
            dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            dObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            dObject.SetActive(false);

            _poolPoints.Add(dObject);
        }


        private void destroy(IMessage rMessage = null)//TODO continue
        {
            //Debug.Log("GAME destroyed: " + rMessage + " level: " + _level);
            MessageDispatcher.RemoveListener(GameEvent.QUIT, destroy);
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.A_BOOM, boomCompleteHandler);
            MessageDispatcher.RemoveListener(GameEvent.HERO_REACHED, reachedHandler);
            MessageDispatcher.RemoveListener(GameEvent.HERO_ONE_CELL_AWAY, hideLastPoint);
            MessageDispatcher.RemoveListener(GameEvent.HERO_GET_TRAP, getTrapHandler);

            if(_hero != null) //T
                _hero.destroy();
            

            //    this.removeAllEventListeners();

            //    this.removeHint();

            //_pathfinder.Destroy();
            //_pathfinder = null;

            //    var grid: Tile[] = _grid;
            //    var len number = grid.Length;
            //for (int i = 0; i < _grid.Length; i++)
            //{
            //    _grid[i].clear();
            //}

            //    this.removeAllChildren();
            if (_level != null)
            {
                _level.destroy();
                _level = null;
            }

            _units = null;
            _items = null;

            //    _targetMark = null;
            //    _poolPoints.Length = 0;
            //    _poolPoints = null;
            //    _activePoints.Length = 0;
            //    _activePoints = null;

            //    _grid = null;
            //    _model = null;

                _hero = null;
                _level = null;

            if (_mill != null)
                _mill.destroy();

            _mill = null;
        }

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
