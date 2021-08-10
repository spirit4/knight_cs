using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Hero : Unit
    {
        ////states
        //static IDLE: string = 'idle';
        //static MOVE: string = 'move';
        //static DEATH: string = 'death';

        //private SPEED: number = 300;

        //private _grid: Tile[];

        //private _path: number[] = [];
        //private _directionX: number = 1;
        //private _directionY: number = 1;
        //private _prevState: string = "";

        //static currentTween: createjs.Tween;
        //static currentView: createjs.Sprite;

        ///**helm, shield, sword*/
        //private _wearItems: number[] = [0, 0, 0];
        //private _stateItems: number;

        //static NO_ITEMS: number = 0;
        //static ONE_HELM: number = 1;
        //static ONE_SHIELD: number = 2;
        //static ONE_SWORD: number = 3;
        //static TWO_HELM_SHIELD: number = 4;
        //static TWO_HELM_SWORD: number = 5;
        //static TWO_SHIELD_SWORD: number = 6;
        //static FULL: number = 7;

        //constructor(index: number)
        //{
        //    super(index, ImagesRes.HERO);
        //    this.mouseEnabled = false;

        //    this.state = Hero.IDLE;

        //    this._stateItems = Hero.NO_ITEMS;

        //    this._grid = Core.instance.model.grid;
        //    this.index = index;

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

        //    Hero.currentTween = createjs.Tween.get(this).wait(270).call(this.addHeroView, [], this);
        //}

        //private addHeroView(): void
        //{
        //    var ss: any[] = this.getAnimation();
        //    this.mc.spriteSheet = ss[0];
        //    this.mc.gotoAndPlay(ss[1]);
        //    this.addChild(this.view);
        //}

        //public stop(): void
        //{
        //    this._path.length = 0;
        //}

        //public moveToCell(path: number[] = null): void
        //{
        //    if (path)
        //    {
        //        this._path = path;
        //    }

        //    var currentIndex: number = this.index;
        //    this.index = this._path.shift();
        //    var point: createjs.Point = Utils.getPoint(this.index);

        //    this._directionX = 0;
        //    this._directionY = 0;

        //    //console.log("--MOVE--", this.index)

        //    var step: number = 0;
        //    if (point.y == this.y && point.x > this.x)
        //    {
        //        step = 1;
        //        this._directionX = 1;
        //    }
        //    else if (point.y == this.y && point.x < this.x)
        //    {
        //        step = -1;
        //        this._directionX = -1;
        //    }
        //    else if (point.x == this.x && point.y > this.y)
        //    {
        //        step = Config.WIDTH;
        //        this._directionY = 1;
        //    }
        //    else if (point.x == this.x && point.y < this.y)
        //    {
        //        step = -Config.WIDTH;
        //        this._directionY = -1;
        //    }

        //    while (this.index != currentIndex)
        //    {
        //        currentIndex += step;
        //        if (this._grid[currentIndex].isContainTypes(ImagesRes.STAR) || this._grid[currentIndex].isContainType(ImagesRes.TRAP))
        //        {
        //            this.index = currentIndex;
        //            this._path.length = 0;
        //        }
        //    }

        //    this.move(true);
        //}

        //private moveCompleteHandler(): void
        //{
        //    this.parent.setChildIndex(this, this._grid[this.index].getFirstIndex() + 1);
        //}

        //private changeView(): void
        //{
        // //console.log("changeView", this._prevState, this.state);
        //    if (this._prevState != this.state)
        //    {
        //        var ss: any[] = this.getAnimation();
        //        this.mc.spriteSheet = ss[0];
        //        this.mc.gotoAndPlay(ss[1]);
        //    }

        //    this._prevState = this.state;

        //    if (this._directionX == 1)
        //    {
        //        this.view.scaleX = 1;
        //        this.view.scaleY = 1;
        //        this.view.x = 4 + Config.SIZE_W >> 1;
        //    }
        //    else if (this._directionX == -1)
        //    {
        //        this.view.scaleX = -1;
        //        this.view.scaleY = 1;
        //        this.view.x = 3 * Config.SIZE_W / 2;
        //    }

        //    if (this.state == Hero.IDLE)
        //    {
        //        this.mc.framerate = 15;
        //    }
        //    else if (this.state == Hero.MOVE)
        //    {
        //        switch (this._stateItems)
        //        {
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

        //    }
        //    else
        //    {
        //        this.mc.framerate = 15;
        //    }
        //}

        ////-------actions---------------------------------
        //private idle(): void
        //{
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

        //    if (this._grid[this.index].isContainTypes(ImagesRes.STAR))
        //    {
        //        var type: string = this._grid[this.index].getConcreteType(ImagesRes.STAR);
        //        var index: number = this._grid[this.index].types.indexOf(type);

        //        var dObject: createjs.DisplayObject = this._grid[this.index].objects[index];
        //        createjs.Tween.get(dObject).to({ alpha: 0 }, 150, createjs.Ease.quadOut).call(this.starTweenComplete, [type], this);

        //    }
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
        //    this.state = Hero.IDLE;
        //    this.dispatchEvent(new GameEvent(GameEvent.HERO_REACHED));

        //    this.changeView();
        //    this.parent.addChild(this);//up top
        //}

        //private starTweenComplete(type: string): void
        //{
        //    //console.log("starTweenComplete", type, this.index);
        //    this._prevState = Hero.MOVE;//dirty hack
        //    this._grid[this.index].remove(type);
        //    this.reclothe(type);
        //}

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

        //private move(isContinue: boolean = false): void
        //{
        //    var handler: any = !isContinue ? this.idle : this.keepMove;
        // //console.log("move", isContinue, this.index, this._directionX, this._directionY);

        //    //var newIndex: number;
        //    if (this._directionX == 1 || this._directionY == 1)
        //    {
        //        this._grid[this.index].setIndex(this);
        //    }

        //    this.state = Hero.MOVE;
        //    //Hero.currentTween = createjs.Tween.get(this).to({ x: this._grid[this.index].x, y: this._grid[this.index].y }, this.SPEED, createjs.Ease.linear).call(handler, [], this);

        //    if (this._path.length == 0 && this._grid[this.index].isContainType(ImagesRes.EXIT))
        //    {
        //        Hero.currentTween = createjs.Tween.get(this).
        //            to({ x: this._grid[this.index].x + Config.SIZE_W / 2, y: this._grid[this.index].y + Config.SIZE_H - 4, scaleX: 0, scaleY: 0, alpha: 0 }, this.SPEED * 4, createjs.Ease.linear).
        //            call(handler, [], this);
        //    }
        //    else
        //    {
        //        Hero.currentTween = createjs.Tween.get(this).to({ x: this._grid[this.index].x, y: this._grid[this.index].y }, this.SPEED, createjs.Ease.linear).call(handler, [], this);
        //    }

        //    this.changeView();
        //}

        //private keepMove(): void
        //{
        //    if (this.state == Hero.DEATH)
        //    {
        //        return;
        //    }

        //    if (this._path.length == 0)
        //    {
        //        this.idle();
        //    }
        //    else
        //    {
        //        //console.log("keepMove", this.index);
        //        this.dispatchEvent(new GameEvent(GameEvent.HERO_ONE_CELL_AWAY));
        //        this.moveToCell();
        //    }
        //}

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

        //private reclothe(type: string): void
        //{
        //    var index: number = +type.substr(4, 1);
        //    this._wearItems[index] = 1;

        //    if (index == 0)
        //    {
        //        AchController.instance.addParam(AchController.HELMET_TAKED);
        //    }
        //    else if (index == 1)
        //    {
        //        AchController.instance.addParam(AchController.SHIELD_TAKED);
        //    }
        //    else if (index == 2)
        //    {
        //        AchController.instance.addParam(AchController.SWORD_TAKED);
        //    }


        //    if (this._wearItems[0])
        //    {
        //        if (this._wearItems[1])
        //        {
        //            if (this._wearItems[2])
        //            {
        //                this._stateItems = Hero.FULL;
        //            }
        //            else
        //            {
        //                this._stateItems = Hero.TWO_HELM_SHIELD;
        //            }
        //        }
        //        else if (this._wearItems[2])
        //        {
        //            this._stateItems = Hero.TWO_HELM_SWORD;
        //        }
        //        else
        //        {
        //            this._stateItems = Hero.ONE_HELM;
        //        }
        //    }
        //    else if (this._wearItems[1])
        //    {
        //        if (this._wearItems[2])
        //        {
        //            this._stateItems = Hero.TWO_SHIELD_SWORD;
        //        }
        //        else
        //        {
        //            this._stateItems = Hero.ONE_SHIELD;
        //        }
        //    }
        //    else if (this._wearItems[2])
        //    {
        //        this._stateItems = Hero.ONE_SWORD;
        //    }

        //    this.changeView();
        //}

        //public get stateItems(): number
        //{
        //    return this._stateItems;
        //}
    }
}