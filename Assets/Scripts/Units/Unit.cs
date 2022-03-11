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

        private float _x;
        private float _y;

        public Unit(int index, string type, GameObject view, Tile tile = null)
        {
            _view = view;
            _tile = tile;
            _index = index;
            _type = type;
            _state = Unit.ON;
        }

        public virtual void Destroy()
        {
            //        this.removeAllEventListeners();
            //        createjs.Tween.removeTweens(this);
            //        this.removeAllChildren();

            //        _view = null;
            //        _tile = null;
            //MessageDispatcher.ClearListeners(); //TODO not sure

            //Debug.Log("unit destroyed: " + _view.name + _view.transform.DOKill());

            _view.transform.DOKill();
            _view.GetComponent<SpriteRenderer>().DOKill();
            UnityEngine.Object.Destroy(_view);
            _view = null;
        }

        public virtual void Stop()
        {
            //createjs.Tween.removeTweens(this);
            _view.transform.DOKill();
        }

        public GameObject View
        {
            get
            {
                return _view;
            }
        }

        public int Index
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
        public string Type
        {
            get
            {
                return _type;
            }
        }
        public string State
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

        public Tile Tile
        {
            get
            {
                return _tile;
            }
        }

        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
    }
}