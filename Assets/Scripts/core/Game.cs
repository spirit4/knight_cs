﻿using Assets.Scripts.Data;
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

        //    private _buttonMenu: Button;
        //    private _buttonSound: Button;
        //    private _buttonRestart: Button;

        private Level _level;
        private Hero _hero;
        private MillPress _mill;

        private Pathfinder _pathfinder;

        private Dictionary<int, ICollidable> _units;//private _units: { [index number]: ICollidable; };
        private List<IActivatable> _items;

        //private _helpGameObject
        //private _helpShape: createjs.Shape;
        //private _top: createjs.Shape;
        //private _bottom: createjs.Shape;

        private TargetMark _targetMark;
        private List<GameObject> _poolPoints = new List<GameObject>();
        private List<GameObject> _activePoints = new List<GameObject>();
        private GameObject _boom;

        //private _isStartArrowCheck: boolean = false;

        //public static Sprite sprite = null;// test
        public Game()
        {
            //those came from app.ts, they were first ones -----------------------------------------------------
            var managerBg = new ManagerBg(this);
            //this._bgStage.addChild(this._managerBg);
            new Controller(managerBg);//singleton
            //this._mainStage.addChild(this._core);


            _model = Controller.instance.model;
            _grid = _model.grid;


            //this.x = Config.STAGE_W - Config.WIDTH * Config.SIZE_W >> 1;
            //this.y = Config.MARGIN_TOP;

            //this.createGUI();

            //move to awake from here ------------------------------->>>>>>>>>>>>>>>>>
            //


            //var g: createjs.Graphics = new createjs.Graphics();
            //var shape: createjs.Shape = new createjs.Shape();
            //g.beginFill('rgba(255,255,255, 1)');
            //g.drawRect(0, 0, Config.WIDTH * Config.SIZE_W, Config.HEIGHT * Config.SIZE_H);
            //g.endFill();
            //shape.snapToPixel = true;
            //shape.hitArea = new createjs.Shape(g);
            //this.addChildAt(shape, 0);
            //this.on(GUIEvent.MOUSE_DOWN, this.movedownHandler, this);

            //this.on(GameEvent.COLLISION, this.collisionProcess, this);

            //for (int i = 0; i < 15; i++)   //put on pool
            //    {
            //    this.createPathPoint();
            //}

        }

        private void Awake()
        {
            this.gameObject.isStatic = true;
            //Debug.Log("[Awake]" + ImagesRes.prefabs.Count);
            if (ImagesRes.prefabs.Count == 0) // loading resources to static
                ImagesRes.init();

            DOTween.Init();
            DOTween.Clear();

            //JSONRes.init();
            //ImagesRes.initAnimations();
            Controller.instance.bg.init();

            _level = new Level(this, _model);
            _hero = _level.hero;

            _pathfinder = new Pathfinder(Config.WIDTH, Config.HEIGHT);

            //this._hero.on(GameEvent.HERO_REACHED, this.reachedHandler, this);
            //this._hero.on(GameEvent.HERO_ONE_CELL_AWAY, this.hideLastPoint, this);
            //this._hero.on(GameEvent.HERO_GET_TRAP, this.getTrapHandler, this);
            //this._hero.on(GameEvent.LEVEL_COMPLETE, this.showVictory, this);

            MessageDispatcher.AddListener(GameEvent.HERO_REACHED, reachedHandler);
            MessageDispatcher.AddListener(GameEvent.HERO_ONE_CELL_AWAY, hideLastPoint);

            MessageDispatcher.AddListener(GameEvent.QUIT, destroy);//leave game, from UIManager

            _targetMark = new TargetMark(this.gameObject);

            _units = _level.units;
            _mill = _level.mill;
            _items = _level.items;

            Text level = GameObject.Find("Canvas/PanelGameUI/Image_spear1/level_board/text_level").GetComponent<Text>();
            level.text = (Progress.currentLevel + 1).ToString();
        }


        //private getTrapHandler(e: GameEvent): void
        //{
        //    createjs.Tween.get(this).wait(100).call(this.hideActors, [null], this);
        //    this.showBoom();
        //}


        private void OnMouseDown()// movedownHandler(e: createjs.MouseEvent) : void
        {

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            //Debug.Log("OnMouseDown " + Input.c.name);
            if (_hero.HeroState != Hero.IDLE)
            {
                _hero.stop();
                return;
            }

            //    if (e.target.parent instanceof Button)
            //        {
            //        return;
            //    }

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            int index = GridUtils.getIndex(point.x, point.y);
            //Debug.Log("[tap tile]: " + point + "   " + index);

            if (_hero.index == index)
                return;

            //    if (Progress.currentLevel == 0 && this._hero.index == 10 && index != 54) //to guide
            //    {
            //        return;
            //    }

            //seems not nessasery - look hero reached-------------------------------------------------------
            //    if (e.target instanceof MillPress && Utils.isNeighbours(index, this._hero.index))
            //        {
            //        (< MillPress > e.target).startRotateMill();
            //    }

            //Debug.Log("OnMouseDown " + _grid[index].isWall);
            if (_grid[index].isWall)
                return;


            _pathfinder.Init(_grid); //TODO must be single Init
            _pathfinder.FindPath(_hero.index, index);
            //Debug.Log("OnMouseDown " + _pathfinder.Path.Count);
            //Debug.Log("Game Path  " + _pathfinder.Path.ToString()); //forget it, because of raycast
            if (_pathfinder.Path.Count > 0)// && (!_grid[index].isContainType(ImagesRes.MILL)))// || e.target instanceof MillPress))
            {
                List<int> path = _pathfinder.Path;
                if (_grid[index].isContainType(ImagesRes.MILL))
                {
                    _mill.activate();
                    path.RemoveAt(path.Count - 1);
                }

                if (path.Count > 0)
                {
                    //            this.removeHint();

                    _targetMark.placeByTap(index);
                    showPath(this._pathfinder.Path);

                    //            var arrow: Unit;
                    //                for (var i in this._units)
                    //            {
                    //                arrow = < Unit > this._units[i];
                    //                if (this._units[i] instanceof TowerArrow &&
                    //                    arrow.y == this._hero.y + 5 &&
                    //                    (< TowerArrow > arrow).isShooted())
                    //                    {

                    //                    //console.log("[+++++]: ", i, arrow.y, this._hero.y + 5);
                    //                    this._isStartArrowCheck = true;
                    //                    break;
                    //                }
                    //            }
                    //Debug.Log("Game [path]: " + path.Count);

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
            //Debug.Log("[reached] " + (rMessage.Sender as Hero).index + " === " + _hero.index);
            //Debug.Log("[reached] " + GridUtils.findAround(_grid, _hero.index, ImagesRes.MILL));

            //    if (this._isStartArrowCheck)
            //    {
            //        //console.log("[+++++reached]");
            //        AchController.instance.addParam(AchController.AWAY_FROM_ARROW);
            //        this._isStartArrowCheck = false;
            //    }

            hidePoints();

            //    if (Progress.currentLevel == 0 && this._hero.index == 54) //to guide
            //    {
            //        this.createHintAfterAction();
            //    }

            if (GridUtils.findAround(_grid, _hero.index, ImagesRes.MILL) != -1)
            {
                if (_mill.state != Unit.ON)
                {
                    _mill.startRotateMill();
                    activateItems();
                }
            }
        }

        //private collisionProcess(e: GameEvent): void
        //{
        //    e.stopPropagation();

        //}

        //private animationCompleteHandler(e: GameEvent): void
        //{
        //    var mc: createjs.Sprite = < createjs.Sprite > e.currentTarget;
        //    mc.removeAllEventListeners();
        //    this.removeChild(mc);
        //}

        //private createGUI(): void
        //{
        //    //console.log("[add gui]");
        //    //Utils.addBitmap(0, 0, ImagesRes.GAME_FIELD, this, true);

        //    var bgMap: Object[] = [
        //            { type: ImagesRes.GAME_BG, x: 0, y: 0 },
        //            { type: ImagesRes.UI_LEVEL_BOARD, x: 50, y: 254 },
        //            { type: ImagesRes.SPEAR_UI + '1', x: -280, y: 243 },
        //            { type: ImagesRes.SPEAR_UI + '0', x: 292, y: 200 }
        //        ];
        //    Core.instance.bg.addBitmaps(bgMap);

        //    var container: createjs.Container = new createjs.Container();
        //    this.addChild(container);
        //    container.y = -130;

        //    this._buttonSound = new Button(ImagesRes.BUTTON_SOUND_ON, ImagesRes.BUTTON_SOUND_ON_OVER, ImagesRes.BUTTON_SOUND_OFF, ImagesRes.BUTTON_SOUND_OFF_OVER);
        //    container.addChild(this._buttonSound);
        //    this._buttonSound.x = Config.STAGE_W - this._buttonSound.width - 55;
        //    this._buttonSound.y = 1;
        //    this._buttonSound.name = "";

        //    this._buttonMenu = new Button(ImagesRes.BUTTON_MENU, ImagesRes.BUTTON_MENU_OVER);
        //    container.addChild(this._buttonMenu);
        //    this._buttonMenu.x = this._buttonSound.x - this._buttonMenu.width - 12;
        //    this._buttonMenu.name = "";
        //    this._buttonMenu.y = -4;
        //    this._buttonMenu.on(GUIEvent.MOUSE_CLICK, this.menuClickHandler, this);

        //    this._buttonRestart = new Button(ImagesRes.BUTTON_RESET, ImagesRes.BUTTON_RESET_OVER);
        //    container.addChild(this._buttonRestart);
        //    this._buttonRestart.x = this._buttonMenu.x - this._buttonRestart.width - 12;
        //    this._buttonRestart.y = -8;
        //    this._buttonRestart.name = "";
        //    this._buttonRestart.on(GUIEvent.MOUSE_CLICK, this.restartClickHandler, this);

        //    //Progress.currentLevel = 2;
        //    var level: createjs.Text = new createjs.Text((Progress.currentLevel + 1).toString(), "33px Georgia", "#301213");
        //    container.addChild(level);
        //    level.mouseEnabled = false;
        //    level.snapToPixel = true;
        //    level.x = 92;
        //    level.y = 72;

        //    this.addMill(false);
        //    this.vane.x -= this.x;
        //    this.vane.y -= 52;
        //}

        //private menuClickHandler(e: createjs.MouseEvent): void
        //{
        //    var ev: GUIEvent = new GUIEvent(GUIEvent.GOTO_WINDOW);
        //    ev.window = View.LEVELS;
        //    this.dispatchEvent(ev);
        //}

        //gone to Unity Editor
        private void restartHandler()//e: createjs.MouseEvent = null) : void
        {
            //destroy();

            MessageDispatcher.SendMessage(GameEvent.RESTART);
        }

        //public createHint(): void
        //{
        //    if (!Hints.texts[Progress.currentLevel])
        //    {
        //        return;
        //    }

        //    var params: Array <  number > = Hints.texts[Progress.currentLevel];

        //    var g: createjs.Graphics = new createjs.Graphics();
        //    var shape: createjs.Shape = new createjs.Shape(g);
        //    Core.instance.addChild(shape);
        //    shape.alpha = 0.7;
        //    this._helpShape = shape;

        //    g.beginFill("#333333");
        //    g.drawRect(0, 0, Config.STAGE_W, Config.STAGE_H_MAX);

        //    var holes: Array < Object > = Hints.holes[Progress.currentLevel];
        //    for (int i = 0; i < holes.Length; i++)
        //        {
        //        g.arc(holes[i]['x'], holes[i]['y'], holes[i]['r'], 0, Math.PI * 2, true).closePath();
        //    }

        //    g.endFill();

        //    var bitmap GameObject = new createjs.Bitmap(ImagesRes.getImage(ImagesRes.HELP + params[2]));
        //    bitmap.mouseEnabled = false;
        //    bitmap.snapToPixel = true;
        //    Core.instance.addChild(bitmap);
        //    this._help = bitmap;
        //    bitmap.x = params[0];
        //    bitmap.y = params[1];

        //    g = new createjs.Graphics();
        //    shape = new createjs.Shape(g);
        //    g.beginFill("#333333");
        //    g.drawRect(0, 0, Config.STAGE_W, Config.PADDING);
        //    g.endFill();
        //    shape.snapToPixel = true;
        //    shape.alpha = 0.7;
        //    Core.instance.bg.addChild(shape);
        //    this._top = shape;

        //    shape = new createjs.Shape(g);
        //    shape.snapToPixel = true;
        //    shape.alpha = 0.7;
        //    shape.y = Config.PADDING + Config.STAGE_H_MIN;
        //    Core.instance.bg.addChild(shape);
        //    this._bottom = shape;
        //    Core.instance.bg.update();
        //}

        //public createHintAfterAction(): void
        //{
        //    if (!Hints.textsAfterAction[Progress.currentLevel])
        //    {
        //        return;
        //    }

        //    var params: Array <  number > = Hints.textsAfterAction[Progress.currentLevel];

        //    var g: createjs.Graphics = new createjs.Graphics();
        //    var shape: createjs.Shape = new createjs.Shape(g);
        //    Core.instance.addChild(shape);
        //    shape.alpha = 0.7;
        //    this._helpShape = shape;

        //    g.beginFill("#333333");
        //    g.drawRect(0, 0, Config.STAGE_W, Config.STAGE_H_MAX);

        //    var holes: Array < Object > = Hints.holesAfterAction[Progress.currentLevel];
        //    for (int i = 0; i < holes.Length; i++)
        //        {
        //        g.arc(holes[i]['x'], holes[i]['y'], holes[i]['r'], 0, Math.PI * 2, true).closePath();
        //    }

        //    g.endFill();

        //    var bitmap GameObject = new createjs.Bitmap(ImagesRes.getImage(ImagesRes.HELP + params[2]));
        //    bitmap.mouseEnabled = false;
        //    bitmap.snapToPixel = true;
        //    Core.instance.addChild(bitmap);
        //    this._help = bitmap;
        //    bitmap.x = params[0];
        //    bitmap.y = params[1];

        //    g = new createjs.Graphics();
        //    shape = new createjs.Shape(g);
        //    g.beginFill("#333333");
        //    g.drawRect(0, 0, Config.STAGE_W, Config.PADDING);
        //    g.endFill();
        //    shape.snapToPixel = true;
        //    shape.alpha = 0.7;
        //    Core.instance.bg.addChild(shape);
        //    this._top = shape;

        //    shape = new createjs.Shape(g);
        //    shape.snapToPixel = true;
        //    shape.alpha = 0.7;
        //    shape.y = Config.PADDING + Config.STAGE_H_MIN;
        //    Core.instance.bg.addChild(shape);
        //    this._bottom = shape;
        //    Core.instance.bg.update();
        //}

        //private removeHint(): void
        //{
        //    if (this._help)
        //    {
        //        Core.instance.removeChild(this._help);
        //        Core.instance.removeChild(this._helpShape);
        //        this._help = null;
        //        this._helpShape = null;

        //        Core.instance.bg.removeChild(this._top);
        //        Core.instance.bg.removeChild(this._bottom);
        //        Core.instance.bg.update();
        //        this._top = null;
        //        this._bottom = null;
        //    }
        //}

        //Bounds m_Collider, m_Collider2;
        public void Update()
        {
            if (_units != null)
                checkCollision(_units, 0.25f, 0.25f, 0.25f, 0.15f);
        }

        public void checkCollision(Dictionary<int, ICollidable> vector, float w1, float w2, float h1, float h2)
        {
            GameObject dObject;
            //magic number
            float heroX = _hero.view.transform.localPosition.x;// + 0.3f;// + Config.SIZE_W * 0.5f;
            float heroY = _hero.view.transform.localPosition.y + 0.15f;// + Config.SIZE_H * 0.5f;
            float objX;
            float objY;

            foreach (KeyValuePair<int, ICollidable> pair in vector)
            {
                dObject = pair.Value.view;

                objX = dObject.transform.localPosition.x;// + Config.SIZE_W * 0.5f;
                objY = dObject.transform.localPosition.y;// + Config.SIZE_H * 0.5f;

                //Debug.Log("-1-"+dObject.name  + " heroX: " + heroX + "  heroY: " + heroY + "  objX: " + objX + "  objY: " + objY);
                //if (heroX > objX + x1 && heroX < objX + x2)
                //{
                //    if (heroY > objY + y1 && heroY < objY + y2)
                if ((heroX + w1 >= objX - w2 && heroX - w1 <= objX + w2) && (heroY - h1 <= objY + h2 && heroY + h1 >= objY - h2))
                {
                    //if (heroY > objY + y1 && heroY < objY + y2)
                    //{
                    if (_hero.HeroState != Hero.DEATH)
                    {
                        //Debug.Log("-2---------"+ dObject.name + " heroX: " + heroX + "  heroY: " + heroY + "  objX: " + objX + "  objY: " + objY);

                        //if (pair.Value is Monster)//dObject.name == ImagesRes.MONSTER)// instanceof Monster)
                        //{
                        Monster monster = pair.Value as Monster;
                        //var index number = this._items.indexOf(monster);
                        //this._items.splice(index, 1);                  not now, maybe never
                        //this.removeChild(monster);-------------------------------------------
                        //}

                        //vector[i] = null;
                        //delete vector[i];
                        vector.Remove(pair.Key);

                        if (!_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword) //(this._hero.stateItems != Hero.FULL)
                        {
                            //    if (dObject instanceof Monster)
                            //            {
                            //        AchController.instance.addParam(AchController.HERO_DEAD_BY_MONSTER);
                            //    }
                            //            else if (dObject instanceof TowerArrow)
                            //            {
                            //        AchController.instance.addParam(AchController.HERO_DEAD_BY_ARROW);
                            //    }
                            //    createjs.Tween.get(this).wait(100).call(this.hideActors, [dObject], this);
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
                            //    AchController.instance.addParam(AchController.MONSTER_DEAD);

                            //    createjs.Tween.get(this).wait(100).call(this.hideActors, [dObject, false], this);
                            WaitAndCall(100, hideActors, pair.Value, false); //killing the monster
                            showBoom(ImagesRes.A_ATTACK_BOOM);
                        }

                        break;
                    }
                    //}
                }
            }
        }

        private void hideActors(ICollidable unit, bool IsEnd)
        {
            if (unit != null)
            {
                unit.stop();
                _hero.stop();
                //        unit.view.visible = false;
                unit.view.SetActive(false);
                unit.destroy();
            }

            if (IsEnd)
            {
                //this._hero.visible = false;
                _hero.view.SetActive(false);
            }

        }

        private void showBoom(string boomType = ImagesRes.A_BOOM)
        {
            float boomX = _hero.view.transform.localPosition.x;// + Config.SIZE_W / 2;
            float boomY = _hero.view.transform.localPosition.y + 0.25f;// - Config.SIZE_H / 2 - 0.1f;

            if (_boom == null)
            {
                GameObject gameObject = GameObject.Instantiate(ImagesRes.prefabs[boomType], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                gameObject.transform.SetParent(this.gameObject.transform);
                
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 200;//TODO sorting2
                _boom = gameObject;
            }

            _boom.transform.localPosition = new Vector3(boomX, boomY);
            _boom.SetActive(true);
            MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, boomCompleteHandler);
        }

        public void boomCompleteHandler(IMessage rMessage)
        {
            //Debug.Log("boomCompleteHandler]" + (rMessage.Sender as GameObject).name);

            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, boomCompleteHandler);
            //Destroy(rMessage.Sender as GameObject);
            _boom.SetActive(false);

            if (!_hero.HasHelmet || !_hero.HasShield || !_hero.HasSword)
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
                // Debug.Log("--showPath--" + path.Count + " i " + i);
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
            this._activePoints.Add(bitmap);

            bitmap.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuart).OnComplete(() => reducePoint(bitmap.transform));
        }

        private void reducePoint(Transform transform)
        {
            transform.DOScale(1, 0.06f).SetEase(Ease.InQuart);
        }

        private void hidePoints()
        {
            while (this._activePoints.Count > 0)
            {
                GameObject bitmap = _activePoints[_activePoints.Count - 1];
                _activePoints.RemoveAt(_activePoints.Count - 1);
                bitmap.SetActive(false);
                _poolPoints.Add(bitmap);
            }
        }

        private void hideLastPoint(IMessage rMessage)
        {
            if (this._activePoints.Count == 0)
                return;

            //if (this._isStartArrowCheck)
            //{
            //    //console.log("[+++++reached]");
            //    AchController.instance.addParam(AchController.AWAY_FROM_ARROW);
            //    this._isStartArrowCheck = false;
            //}

            //console.log("[CELL AWAY]", this._hero.index, this._hero.mc.framerate);

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
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, boomCompleteHandler);
            MessageDispatcher.RemoveListener(GameEvent.HERO_REACHED, reachedHandler);
            MessageDispatcher.RemoveListener(GameEvent.HERO_ONE_CELL_AWAY, hideLastPoint);

            //MessageDispatcher.ClearListeners(); //TODO bug in inerating?
            //    this.removeAllEventListeners();
            //    this._hero.removeAllEventListeners();

            //    //console.log("[destroy hero]", this._top)
            //    this._hero.destroy();

            //    if (this._top)
            //    {
            //        this._top.graphics.clear();
            //        this._bottom.graphics.clear();
            //        Core.instance.bg.removeChild(this._top);
            //        Core.instance.bg.removeChild(this._bottom);
            //        Core.instance.bg.update();
            //        this._top = null;
            //        this._bottom = null;

            //        this._helpShape.graphics.clear();
            //        Core.instance.removeChild(this._help);
            //        Core.instance.removeChild(this._helpShape);
            //        this._help = null;
            //        this._helpShape = null;
            //}

            //    this.removeAllEventListeners();

            //    this.removeHint();

            //    this._pathfinder.destroy();
            //    this._pathfinder = null;

            //    var grid: Tile[] = this._grid;
            //    var len number = grid.Length;
            //    for (int i = 0; i < len; i++)
            //        {
            //        grid[i].clear();
            //    }

            //    this.removeAllChildren();
            if (_level != null)
            {
                _level.destroy();
                _level = null;
            }

            _units = null;
            _items = null;

            //    this._targetMark = null;
            //    this._poolPoints.Length = 0;
            //    this._poolPoints = null;
            //    this._activePoints.Length = 0;
            //    this._activePoints = null;

            //    this._grid = null;
            //    this._model = null;

            //    this._hero = null;
            //    this._level = null;
            //    this._mill = null;

            //    this._buttonMenu = null;
            //    this._buttonSound = null;
            //    this._buttonRestart = null;
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
