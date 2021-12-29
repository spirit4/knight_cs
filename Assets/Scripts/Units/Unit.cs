using Assets.Scripts.Core;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Units
{
    public abstract class Unit : ICollidable
    {
        //states
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
            _view = view;
            _tile = tile;
            _index = index;
            _type = type;
            _state = Unit.ON;
        }

        public virtual void destroy()
        {
            //        this.removeAllEventListeners();
            //        createjs.Tween.removeTweens(this);
            //        this.removeAllChildren();

            //        _view = null;
            //        _tile = null;
            //MessageDispatcher.ClearListeners(); //TODO not sure

            //Debug.Log("unit destroyed: " + _view.name + _view.transform.DOKill());
            _view.transform.DOKill();
            UnityEngine.Object.Destroy(view);
        }

        public virtual void stop()
        {
            //createjs.Tween.removeTweens(this);
            _view.transform.DOKill();
        }

        public GameObject view
        {
            get
            {
                return _view;
            }
        }

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