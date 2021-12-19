using Assets.Scripts.Core;
using com.ootii.Messages;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Units
{
    public abstract class Unit : ICollidable
    {
        //    //states
        public const string ON = "on";
        public const string OFF = "off";
        public const string STARTED = "started";

        private GameObject _view;
        private int _index;
        private string _type;
        private string _state;
        private Tile _tile;

        public float x;
        public float y; //TODO uppercase

        public Unit(int index, string type, GameObject view, Tile tile = null)//view?: createjs.DisplayObject, tile?: Tile)
        {
            //this.mouseChildren = false;

            _view = view;
            _tile = tile;
            _index = index;
            _type = type;
            _state = Unit.ON;
        }

        public void destroy()
        {
            //        this.removeAllEventListeners();
            //        createjs.Tween.removeTweens(this);
            //        this.removeAllChildren();

            //        this._view = null;
            //        this._tile = null;
            //MessageDispatcher.ClearListeners(); //TODO not sure

            _view.transform.DOKill();
            UnityEngine.Object.Destroy(view);
        }

        public virtual void stop()
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
        //        
        //    }
        //    //--------------------setters

        public GameObject view
        {
            get
            {
                return _view;
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
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        public string type
        {
            get
            {
                return _type;
            }
        }
        public string state
        {
            set
            {
                _state = value;
                //console.log("[set state]",value, this)
            }
            get
            {
                return _state;
            }
        }

        public Tile tile
        {
            get
            {
                return _tile;
            }
        }

    }
}