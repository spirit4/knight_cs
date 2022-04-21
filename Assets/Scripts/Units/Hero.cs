using Assets.Scripts.Achievements;
using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using DG.Tweening;

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Hero : MovingUnit, IActivatable
    {
        public enum State : int
        {
            Idle,
            Move,
            Death,
        }

        private const float SPEED = 0.3f;

        private List<int> _path;
        private int _directionX = 1;

        private const float MARGIN_X = -0.3f;
        private const float MARGIN_Y = 0.65f;

        private State _heroState;
        private bool _hasShield = false;
        private bool _hasSword = false;
        private bool _hasHelmet = false;

        private GameObject _inside;
        private int _index;
        public new int Index { get => _index; }//hero  isn't static

        private float _x;
        private float _y;
        private Tile[] _grid; // TODO don't really need this

        public State HeroState { get => _heroState; set => _heroState = value; }
        public bool HasShield { get => _hasShield; }
        public bool HasSword { get => _hasSword; }
        public bool HasHelmet { get => _hasHelmet; }

        //debug
        //private bool _hasShield = true;
        //private bool _hasSword = true;
        //private bool _hasHelmet = true;

        public Hero(EntityInput config) : base(config)
        {
            _grid = Model.Grid;
        }
        public override void BindToTile(Tile tile)
        {
            _tile = tile;
            _index = tile.Index;
            _x = tile.X;
            _y = tile.Y;
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view.GetComponent<SpriteRenderer>().sortingOrder = 200;

            _inside = Object.Instantiate(_config.Prefabs[0]);
            _inside.GetComponent<SpriteRenderer>().sortingLayerName = _config.Layer.ToString();
            _inside.GetComponent<SpriteRenderer>().sortingOrder = 200;
            _inside.transform.localPosition = new Vector3(MARGIN_X, MARGIN_Y);
            _inside.transform.SetParent(_view.transform);
        }

        public ICollidable Init(Tile tile)
        {
            return null;
        }
        public override void Stop()
        {
            _path.Clear();
        }

        public void MoveToCell(List<int> path = null)
        {
            if (path != null)
                _path = path;

            int currentIndex = _index;
            _index = _path[0];
            _path.RemoveAt(0);
            Vector2 point = GridUtils.GetPoint(_index);

            _directionX = 0;

            _x = _view.transform.localPosition.x;
            _y = _view.transform.localPosition.y;

            int step = 0;
            if (point.y == _y && point.x > _x)
            {
                step = 1;
                _directionX = 1;
                _inside.transform.localPosition = new Vector3(MARGIN_X, MARGIN_Y);
            }
            else if (point.y == _y && point.x < _x)
            {
                step = -1;
                _directionX = -1;
                _inside.transform.localPosition = new Vector3(-MARGIN_X, MARGIN_Y);
            }
            else if (point.x == _x && point.y > _y)
            {
                step = -Config.WIDTH;
            }
            else if (point.x == _x && point.y < _y)
            {
                step = Config.WIDTH;
            }

            _x = _view.transform.localPosition.x;
            _y = _view.transform.localPosition.y;
            _view.transform.localPosition = new Vector3(_grid[currentIndex].X, _grid[currentIndex].Y);

            int hack = 0;
            while (_index != currentIndex)
            {
                currentIndex += step;

                if (_grid[currentIndex].IsContainTypes(Entity.Type.star) || _grid[currentIndex].IsContainType(Entity.Type.trap))
                {
                    _index = currentIndex;
                    _path.Clear();
                }
                hack++;
                if (hack >= 100)
                    break;
            }

            Move(true);

        }

        private void ChangeView()
        {
            Animator a = _inside.GetComponent<Animator>();
            a.SetInteger("HeroState", (int)_heroState);
            a.SetBool("HasHelmet", _hasHelmet);
            a.SetBool("HasSword", _hasSword);
            a.SetBool("HasShield", _hasShield);

            if (_directionX == 1)
                _inside.GetComponent<SpriteRenderer>().flipX = false;

            else if (_directionX == -1)
                _inside.GetComponent<SpriteRenderer>().flipX = true;
        }

        private void Idle()
        {
            if (_heroState == State.Death)
                return;

            if (_grid[_index].IsContainTypes(Entity.Type.star))
            {
                string type = _grid[_index].GetConcreteType(ImagesRes.STAR);
                int index = _grid[_index].Types.IndexOf(type);

                GameObject dObject = _grid[_index].Objects[index];

                dObject.transform.DOScale(0, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => StarTweenComplete(type));
            }

            else if (_grid[_index].IsContainType(Entity.Type.trap))
            {
                (_grid[_index].GetEntity(Entity.Type.trap) as Trap).Activate();

                DOTween.Sequence().AppendInterval(0.4f).AppendCallback(TrapTweenComplete);

                GameEvents.AnimationEndedHandlers += TrapAnimationComplete;
            }
            else if (_grid[_index].IsContainType(ImagesRes.EXIT))
            {
                if (_hasHelmet)
                    Progress.StarsAllLevels[Progress.CurrentLevel, 0] = 1;

                if (_hasShield)
                    Progress.StarsAllLevels[Progress.CurrentLevel, 1] = 1;

                if (_hasSword)
                    Progress.StarsAllLevels[Progress.CurrentLevel, 2] = 1;

                if (!_hasHelmet && !_hasShield && !_hasSword)
                    GameEvents.AchTriggered(Trigger.TriggerType.LevelWithoutItems);

                GameEvents.LevelComplete();
            }

            _heroState = State.Idle;

            ChangeView();

            GameEvents.HeroReached();
        }

        private void StarTweenComplete(string type)
        {
            _grid[_index].Remove(type);
            Reclothe(type);
        }

        private void TrapAnimationComplete()
        {
            GameEvents.AnimationEndedHandlers -= TrapAnimationComplete;

            Trap trap = _grid[_index].GetEntity(Entity.Type.trap) as Trap;
            trap.Stop();
            _grid[_index].Remove(ImagesRes.TRAP);//TODO delete it
        }

        private void TrapTweenComplete()
        {
            GameEvents.AchTriggered(Trigger.TriggerType.HeroDeadByTrap);
            GameEvents.HeroTrapped();
        }

        private void Move(bool isContinue = false)
        {
            TweenCallback handler = Idle;
            if (isContinue)
                handler = KeepMove;

            _heroState = State.Move;

            _view.transform.DOLocalMove(new Vector3(_grid[_index].X, _grid[_index].Y), SPEED).SetEase(Ease.Linear).OnComplete(handler);
            if (_path.Count == 0 && _grid[_index].IsContainType(ImagesRes.EXIT))
            {
                _view.transform.DOKill();
                _view.transform.DOLocalMove(new Vector3(_grid[_index].X, _grid[_index].Y - 0.25f), SPEED * 2).SetEase(Ease.Linear).OnComplete(handler);
                _view.transform.DOScale(0, SPEED * 2).SetEase(Ease.Linear);
                _view.GetComponent<SpriteRenderer>().DOFade(0, SPEED * 2).SetEase(Ease.Linear);
            }

            ChangeView();
        }

        private void KeepMove()
        {
            if (_heroState == State.Death)
                return;

            if (_path.Count == 0)
                Idle();
            else
            {
                GameEvents.HeroOneCellAway();
                MoveToCell();
            }
        }

        private void Reclothe(string type)
        {
            if (type == ImagesRes.STAR + 0)
            {
                _hasHelmet = true;
                GameEvents.AchTriggered(Trigger.TriggerType.HelmetTaked);
            }
            else if (type == ImagesRes.STAR + 1)
            {
                _hasShield = true;
                GameEvents.AchTriggered(Trigger.TriggerType.ShieldTaked);
            }
            else if (type == ImagesRes.STAR + 2)
            {
                _hasSword = true;
                GameEvents.AchTriggered(Trigger.TriggerType.SwordTaked);
            }

            ChangeView();
        }

        public override void Destroy()
        {
            base.Destroy();
            _path = null;
            _grid = null;
        }

        public void Activate()
        {
            //empty
        }
    }
}