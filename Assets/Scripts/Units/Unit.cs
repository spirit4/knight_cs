using Assets.Scripts.Core;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Units
{
    public abstract class Unit : ICollidable, IDestroyable
    {
        //states
        public const string ON = "on";
        public const string OFF = "off";
        public const string STARTED = "started";

        protected GameObject _view;
        protected int _index;
        protected string _type;
        protected string _state;
        protected Tile _tile;
        protected Tile[] _grid;

        protected float _x;
        protected float _y;

        public Unit(int index, string type, GameObject view, Tile tile = null, Tile[] grid = null)
        {
            _view = view;
            _tile = tile;
            _index = index;
            _type = type;
            _state = Unit.ON;
            _grid = grid;
        }

        public virtual void Destroy()
        {
            _view.transform.DOKill();
            _view.GetComponent<SpriteRenderer>().DOKill();
            UnityEngine.Object.Destroy(_view);
            _view = null;
        }

        public virtual void Stop()
        {
            _view.transform.DOKill();
        }

        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public int Index { get => _index; set => _index = value; }
        public GameObject View { get => _view; }
        public string Type { get => _type;  }
        public string State { get => _state; set => _state = value; }
        public Tile Tile { get => _tile;  }
    }
}