using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using com.ootii.Messages;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Hero : Unit
    {
        ////states
        public const int IDLE = 0;
        public const int MOVE = 1;
        public const int DEATH = 2;

        private const float SPEED = 0.3f;

        private Tile[] _grid;

        private List<int> _path;
        private int _directionX = 1;
        //private int _directionY = 1;

        //static currentTween: createjs.Tween;
        //static currentView: createjs.Sprite;

        private float MARGIN_X = -0.3f; //todo fix it ? or not
        private const float MARGIN_Y = 0.7f;

        private int _heroState;
        private bool _hasShield = false;
        private bool _hasSword = false;
        private bool _hasHelmet = false;

        //debug
        //private bool _hasShield = true;
        //private bool _hasSword = true;
        //private bool _hasHelmet = true;

        public Hero(int index, GameObject view) : base(index, ImagesRes.HERO, view)
        {
            this._grid = Controller.instance.model.grid;

            this.x = this._grid[index].x; //for choosing direction
            this.y = this._grid[index].y;

            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 100;//TODO sorting
            view.name = type;

            view.transform.localPosition = new Vector3(this.x + MARGIN_X, this.y + MARGIN_Y);

            _heroState = IDLE;

            //    this.mouseEnabled = false;

            //    this._stateItems = Hero.NO_ITEMS;
            //    
            //    var ss: any[] = this.getAnimation();

            //    Hero.currentView = this.view = new createjs.Sprite(ss[0], ss[1]);
            //    this.view.snapToPixel = true;
            //    this.mc.framerate = 15;

            //    this.x = this._grid[index].x;
            //    this.y = this._grid[index].y;
            //    this.regX = this.view.getBounds().width >> 1;
            //    this.regY = this.view.getBounds().height;
            //    this.view.x = 4 + Config.SIZE_W >> 1;
            //    this.view.y = Config.SIZE_H - 20;

        }


        public override void stop()
        {
            _path.Clear();
        }

        public void moveToCell(List<int> path = null)
        {
            if (path != null)
            {
                this._path = path;
            }

            int currentIndex = index;
            index = _path[0];
            _path.RemoveAt(0);
            Vector2 point = GridUtils.GetPoint(index);

            this._directionX = 0;
            //this._directionY = 0;


            this.x = view.transform.localPosition.x - MARGIN_X;
            this.y = view.transform.localPosition.y - MARGIN_Y;

            // Debug.Log("--MOVE1--" + point);
            //Debug.Log("--MOVE2--x: " + x + " y: " + y);
            //Debug.Log("currentIndex1: " + currentIndex + " index: " + index);

            int step = 0;
            if (point.y == this.y && point.x > this.x)
            {
                step = 1;
                this._directionX = 1;
                MARGIN_X = -0.3f;
            }
            else if (point.y == this.y && point.x < this.x)
            {
                step = -1;
                this._directionX = -1;
                MARGIN_X = 0.3f;
            }
            else if (point.x == this.x && point.y > this.y)
            {
                step = -Config.WIDTH;
                //this._directionY = -1;
            }
            else if (point.x == this.x && point.y < this.y)
            {
                step = Config.WIDTH;
               // this._directionY = 1;
            }

            this.x = view.transform.localPosition.x + MARGIN_X;
            this.y = view.transform.localPosition.y + MARGIN_Y;
            view.transform.localPosition = new Vector3(_grid[currentIndex].x + MARGIN_X, _grid[currentIndex].y + MARGIN_Y);

            //Debug.Log("--stepX== " + point.x + " === " + this.x);
            //Debug.Log("--stepY== " + point.y + " === " + this.y);
            int hack = 0;
            while (this.index != currentIndex)
            {
                currentIndex += step;
                // Debug.Log("currentIndex2: " + currentIndex + " index: " + index + " step: " + step);
                if (this._grid[currentIndex].isContainTypes(ImagesRes.STAR) || this._grid[currentIndex].isContainType(ImagesRes.TRAP))
                {
                    this.index = currentIndex;
                    this._path.Clear();
                }
                hack++;
                if (hack >= 100)
                    break;
            }
            //Debug.Log("--hack== " + hack + " [path]: " + _path.Count);
            this.move(true);

        }

        //private moveCompleteHandler(): void
        //{
        //    this.parent.setChildIndex(this, this._grid[this.index].getFirstIndex() + 1);
        //}

        private void changeView()
        {
            Animator a = view.GetComponent<Animator>();
            a.SetInteger("HeroState", _heroState);
            a.SetBool("HasHelmet", _hasHelmet);
            a.SetBool("HasSword", _hasSword);
            a.SetBool("HasShield", _hasShield);

            if (_directionX == 1)
            {
                view.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (this._directionX == -1)
            {
                view.GetComponent<SpriteRenderer>().flipX = true;
            }

            //            case Hero.NO_ITEMS:
            //                this.mc.framerate = 30;
            //                break;
            //            case Hero.ONE_HELM:
            //                this.mc.framerate = 80;
            //                break;
            //            case Hero.ONE_SHIELD:
            //                this.mc.framerate = 30;
            //                break;
            //            case Hero.ONE_SWORD:
            //                this.mc.framerate = 30;
            //                break;
            //            case Hero.TWO_HELM_SHIELD:
            //                this.mc.framerate = 30;
            //                break;
            //            case Hero.TWO_HELM_SWORD:
            //                this.mc.framerate = 30;
            //                break;
            //            case Hero.TWO_SHIELD_SWORD:
            //                this.mc.framerate = 30;
            //                break;
            //            case Hero.FULL:
            //                this.mc.framerate = 36;
            //                break;
            //        }

        }

        ////-------actions---------------------------------
        private void idle()
        {
            //    if (this.state == Hero.DEATH)
            //    {
            //        return;
            //    }

            //    //console.log("idle", this.index, this._directionX, this._directionY);

            //    if (this._directionX == -1 || this._directionY == -1) //left | top
            //    {
            //        this._grid[this.index].setIndex(this, false);
            //    }

            //    if (this._directionX == 1 || this._directionY == 1)
            //    {
            //        this._grid[this.index].setIndex(this);
            //    }
            // Debug.Log("idle: " + index + " 111: " + _grid[index].isContainTypes(ImagesRes.STAR));

            if (_grid[index].isContainTypes(ImagesRes.STAR))
            {
                string type = _grid[this.index].getConcreteType(ImagesRes.STAR);
                int index = _grid[this.index].types.IndexOf(type);

                GameObject dObject = _grid[this.index].objects[index];

                dObject.transform.DOScale(0, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => starTweenComplete(type));
            }

            //    else if (this._grid[this.index].isContainType(ImagesRes.TRAP))
            //    {
            //        var index: number = this._grid[this.index].types.indexOf(ImagesRes.TRAP);

            //        var trap: createjs.Sprite = <createjs.Sprite> this._grid[this.index].objects[index];
            //        trap.on(GameEvent.ANIMATION_COMPLETE, this.trapAnimationComplete, this);
            //        trap.gotoAndPlay(ImagesRes.A_TRAP);
            //        createjs.Tween.get(trap).to({ alpha: 1 }, 150, createjs.Ease.quadOut).call(this.trapTweenComplete, [type], this);

            //    }
            //    else if (this._grid[this.index].isContainType(ImagesRes.EXIT))
            //    {
            //        //console.log("Hero starsAllLevels", type, this.index);
            //        for (var i: number = 0; i < 3; i++)
            //        {
            //            if (this._wearItems[i] == 1)
            //            {
            //                Progress.starsAllLevels[Progress.currentLevel][i] = 1;
            //            }
            //        }

            //        if (this._wearItems[0] == 0 && this._wearItems[1] == 0 && this._wearItems[2] == 0)
            //        {
            //            AchController.instance.addParam(AchController.LEVEL_WITHOUT_ITEMS);
            //        }

            //        this.levelComplete();
            //        //this.dispatchEvent(new GameEvent(GameEvent.RESTART, true));
            //    }

            //    //this._directionIndex = 0;

            _heroState = Hero.IDLE;
            //Debug.Log("changeView IDLE" + _heroState + "   " + _hasHelmet);
            changeView();


            MessageDispatcher.SendMessage(this, GameEvent.HERO_REACHED, null, 0);


            //    this.parent.addChild(this);//up top
        }

        private void starTweenComplete(string type)
        {
            //console.log("starTweenComplete", type, this.index);
            this._grid[this.index].remove(type);
            reclothe(type);
        }

        //private trapTweenComplete(): void
        //{
        //    AchController.instance.addParam(AchController.HERO_DEAD_BY_TRAP);
        //    this.dispatchEvent(new GameEvent(GameEvent.HERO_GET_TRAP));
        //}

        //private trapAnimationComplete(e: GameEvent): void
        //{
        //    e.currentTarget.visible = false;
        //    e.currentTarget.removeAllEventListeners();
        //    e.currentTarget.stop();
        //    this._prevState = Hero.MOVE;//dirty hack
        //    this._grid[this.index].remove(ImagesRes.TRAP);
        //}

        //private levelComplete(): void
        //{
        //    //console.log("EXIT");
        //    this.view.removeAllEventListeners();
        //    this.dispatchEvent(new GameEvent(GameEvent.LEVEL_COMPLETE, false, false));
        //}

        private void move(bool isContinue = false)
        {
            TweenCallback handler = idle;
            if (isContinue)
                handler = keepMove;

            // //console.log("move", isContinue, this.index, this._directionX, this._directionY);

            //    //var newIndex: number;
            //    if (this._directionX == 1 || this._directionY == 1)
            //    {
            //        this._grid[this.index].setIndex(this);
            //    }

            _heroState = Hero.MOVE;
            //Debug.Log("changeView MOVE" + _heroState + "   " + HeroState);
            view.transform.DOLocalMove(new Vector3(_grid[index].x + MARGIN_X, _grid[index].y + MARGIN_Y), SPEED).SetEase(Ease.Linear).OnComplete(handler);
            //    if (this._path.length == 0 && this._grid[this.index].isContainType(ImagesRes.EXIT))
            //    {
            //        Hero.currentTween = createjs.Tween.get(this).
            //            to({ x: this._grid[this.index].x + Config.SIZE_W / 2, y: this._grid[this.index].y + Config.SIZE_H - 4, scaleX: 0, scaleY: 0, alpha: 0 }, this.SPEED * 4, createjs.Ease.linear).
            //            call(handler, [], this);
            //    }
            //    else //copy top line?
            //    {
            //        Hero.currentTween = createjs.Tween.get(this).to({ x: this._grid[this.index].x, y: this._grid[this.index].y }, this.SPEED, createjs.Ease.linear).call(handler, [], this);
            //    }

            changeView();//todo fix many changes
        }

        private void keepMove()
        {
            //    if (this.state == Hero.DEATH)
            //    {
            //        return;
            //    }

            if (this._path.Count == 0)
            {
                this.idle();
            }
            else
            {
                //        //console.log("keepMove", this.index);
                MessageDispatcher.SendMessage(GameEvent.HERO_ONE_CELL_AWAY);
                moveToCell();
            }
        }

        //private die(): void
        //{
        //    //this.parent.removeChildAt(this.parent.getNumChildren() - 1);//oops

        //    this.dispatchEvent(new GameEvent(GameEvent.RESTART, true, false));
        //}

        //public destroy(): void
        //{
        //    super.destroy();

        //    this._path.length = 0;
        //    this._path = null;
        //    this._grid = null;
        //    this._wearItems.length = 0;
        //    this._wearItems = null;

        //    Hero.currentTween = null;
        //    Hero.currentView = null;
        //}

        //private getAnimation(): any[]
        //{
        //    switch (this.state)
        //    {
        //        case Hero.IDLE:
        //            return [ImagesRes.A_HERO_IDLEs[this._stateItems]['atlas'], ImagesRes.A_HERO_IDLEs[this._stateItems]['animation']];
        //            break;
        //        case Hero.MOVE:
        //            return [ImagesRes.A_HERO_MOVEs[this._stateItems]['atlas'], ImagesRes.A_HERO_MOVEs[this._stateItems]['animation']];
        //            break;
        //    }
        //    return ["incorrect", "animation"];
        //}

        private void reclothe(string type)
        {
            if (type == ImagesRes.STAR + 0)
            {
                _hasHelmet = true;
                //AchController.instance.addParam(AchController.HELMET_TAKED);
            }
            else if (type == ImagesRes.STAR + 1)
            {
                _hasShield = true;
                //AchController.instance.addParam(AchController.SHIELD_TAKED);
            }
            else if (type == ImagesRes.STAR + 2)
            {
                _hasSword = true;
                //AchController.instance.addParam(AchController.SWORD_TAKED);
            }

            changeView();
            //Debug.Log("changeView RE------ " + HeroState + "   " + _heroState);
        }

        //public get stateItems(): number
        //{
        //    return this._stateItems;
        //}
        public int HeroState
        {
            get
            {
                return _heroState;
            }
            set
            {
                _heroState = value;
            }
        }
        public bool HasHelmet
        {
            get
            {
                return _hasHelmet;
            }
        }
        public bool HasShield
        {
            get
            {
                return _hasShield;
            }
        }
        public bool HasSword
        {
            get
            {
                return _hasSword;
            }
        }
    }
}