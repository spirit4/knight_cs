using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Unit : MonoBehaviour, ICollidable
    {
        //    //states
        //    static ON: string = 'on';
        //    static OFF: string = 'off';
        //    static STARTED: string = 'started';

        private GameObject _view;
        private int _index;
        private string _type;
        private string _state;
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

        public void destroy()
        {
            //        this.removeAllEventListeners();
            //        createjs.Tween.removeTweens(this);
            //        this.removeAllChildren();

            //        this._view = null;
            //        this._tile = null;
        }

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

        public GameObject view
        {
            get
            {
                return this._view;
            }
        }

        //    public get mc(): createjs.Sprite
        //    {
        //        return <createjs.Sprite> this._view;
        //    }

        //    public get bitmap(): createjs.Bitmap
        //    {
        //        return <createjs.Bitmap> this._view;
        //    }

        public int index
        {
            get
            {
                return this._index;
            }
        }
        public string type
        {
            get
            {
                return this._type;
            }
        }
        public string state
        {
            get
            {
                return this._state;
            }
        }

        //    public get tile(): Tile
        //    {
        //        return this._tile;
        //    }

    }
}