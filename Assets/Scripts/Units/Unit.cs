using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Unit : MonoBehaviour //: ICollidable
    {
        //    //states
        //    static ON: string = 'on';
        //    static OFF: string = 'off';
        //    static STARTED: string = 'started';

        //    private _view: createjs.DisplayObject;
        //    private _index: number;
        //    private _type: string;
        //    private _state: string;
        //    private _tile: Tile;

        //    constructor(index: number, type: string, view?: createjs.DisplayObject, tile?: Tile)
        //    {
        //        super();
        //        this.mouseChildren = false;

        //        this._view = view;
        //        this._tile = tile;
        //        this._index = index;
        //        this._type = type;
        //        this._state = Unit.ON;
        //    }

        //    public destroy(): void
        //    {
        //        this.removeAllEventListeners();
        //        createjs.Tween.removeTweens(this);
        //        this.removeAllChildren();

        //        this._view = null;
        //        this._tile = null;
        //    }

        public void stop()
        {
            //createjs.Tween.removeTweens(this);
        }

        //    //--------------------setters
        //    public set view(value: createjs.DisplayObject)
        //    {
        //        this._view = value;
        //    }

        //    public set index(value: number)
        //    {
        //        this._index = value;
        //    }

        //    public set state(value: string)
        //    {
        //        this._state = value;
        //        //console.log("[set state]",value, this)
        //    }
        //    //--------------------setters

        //    public get view(): createjs.DisplayObject
        //    {
        //        return this._view;
        //    }

        //    public get mc(): createjs.Sprite
        //    {
        //        return <createjs.Sprite> this._view;
        //    }

        //    public get bitmap(): createjs.Bitmap
        //    {
        //        return <createjs.Bitmap> this._view;
        //    }

        //    public get index(): number
        //    {
        //        return this._index;
        //    }

        //    public get type(): string
        //    {
        //        return this._type;
        //    }

        //    public get state(): string
        //    {
        //        return this._state;
        //    }

        //    public get tile(): Tile
        //    {
        //        return this._tile;
        //    }

    }
}