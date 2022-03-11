﻿using Assets.Scripts.Core;
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

        private const float MARGIN_X = -0.3f;
        private const float MARGIN_Y = 0.65f;

        private int _heroState;
        private bool _hasShield = false;
        private bool _hasSword = false;
        private bool _hasHelmet = false;

        private GameObject _inside;

        //debug
        //private bool _hasShield = true;
        //private bool _hasSword = true;
        //private bool _hasHelmet = true;

        public Hero(int index, GameObject inside, GameObject view) : base(index, ImagesRes.HERO, view)
        {
            _grid = Controller.instance.model.Grid;

            this.X = _grid[index].X; //for choosing direction
            this.Y = _grid[index].Y;

            _inside = inside;

            view.AddComponent<SpriteRenderer>();
            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 100;

            _inside.name = Type;
            _inside.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            _inside.GetComponent<SpriteRenderer>().sortingOrder = 100;

            view.transform.localPosition = new Vector3(this.X, this.Y);
            _inside.transform.localPosition = new Vector3(MARGIN_X, MARGIN_Y);

            _heroState = IDLE;
        }


        public override void Stop()
        {
            _path.Clear();
        }

        public void MoveToCell(List<int> path = null)
        {
            if (path != null)
                _path = path;

            int currentIndex = Index;
            Index = _path[0];
            _path.RemoveAt(0);
            Vector2 point = GridUtils.GetPoint(Index);

            _directionX = 0;
            //_directionY = 0;


            this.X = View.transform.localPosition.x;
            this.Y = View.transform.localPosition.y;

            int step = 0;
            if (point.y == this.Y && point.x > this.X)
            {
                step = 1;
                _directionX = 1;
                _inside.transform.localPosition = new Vector3(MARGIN_X, MARGIN_Y);
            }
            else if (point.y == this.Y && point.x < this.X)
            {
                step = -1;
                _directionX = -1;
                _inside.transform.localPosition = new Vector3(-MARGIN_X, MARGIN_Y);
            }
            else if (point.x == this.X && point.y > this.Y)
            {
                step = -Config.WIDTH;
            }
            else if (point.x == this.X && point.y < this.Y)
            {
                step = Config.WIDTH;
            }

            this.X = View.transform.localPosition.x;
            this.Y = View.transform.localPosition.y;
            View.transform.localPosition = new Vector3(_grid[currentIndex].X, _grid[currentIndex].Y);

            int hack = 0;
            while (Index != currentIndex)
            {
                currentIndex += step;

                if (_grid[currentIndex].IsContainTypes(ImagesRes.STAR) || _grid[currentIndex].IsContainType(ImagesRes.TRAP))
                {
                    Index = currentIndex;
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
            a.SetInteger("HeroState", _heroState);
            a.SetBool("HasHelmet", _hasHelmet);
            a.SetBool("HasSword", _hasSword);
            a.SetBool("HasShield", _hasShield);

            if (_directionX == 1)
                _inside.GetComponent<SpriteRenderer>().flipX = false;

            else if (_directionX == -1)
                _inside.GetComponent<SpriteRenderer>().flipX = true;
        }

        ////-------actions---------------------------------
        private void Idle()
        {
            if (_heroState == Hero.DEATH)
                return;

            if (_grid[Index].IsContainTypes(ImagesRes.STAR))
            {
                string type = _grid[Index].GetConcreteType(ImagesRes.STAR);
                int index = _grid[Index].Types.IndexOf(type);

                GameObject dObject = _grid[Index].Objects[index];

                dObject.transform.DOScale(0, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => StarTweenComplete(type));
            }

            else if (_grid[Index].IsContainType(ImagesRes.TRAP))
            {
                GameObject trap = _grid[Index].GetObject(ImagesRes.TRAP);

                Animator anim = trap.GetComponent<Animator>();
                anim.enabled = true;
                trap.GetComponent<SpriteRenderer>().DOFade(1, 0.15f).SetEase(Ease.OutQuad);
                DOTween.Sequence().AppendInterval(0.15f).AppendCallback(TrapTweenComplete);
                MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.TRAP, TrapAnimationComplete, false);
            }
            else if (_grid[Index].IsContainType(ImagesRes.EXIT))
            {
                if (_hasHelmet)
                    Progress.StarsAllLevels[Progress.CurrentLevel, 0] = 1;

                if (_hasShield)
                    Progress.StarsAllLevels[Progress.CurrentLevel, 1] = 1;

                if (_hasSword)
                    Progress.StarsAllLevels[Progress.CurrentLevel, 2] = 1;

                if (!_hasHelmet && !_hasShield && !_hasSword)
                    AchievementController.Instance.AddParam(AchievementController.LEVEL_WITHOUT_ITEMS);

                LevelComplete();
            }

            _heroState = Hero.IDLE;

            ChangeView();

            MessageDispatcher.SendMessage(GameEvent.HERO_REACHED);
        }

        private void StarTweenComplete(string type)
        {
            _grid[Index].Remove(type);
            Reclothe(type);
        }

        private void TrapTweenComplete()
        {
            AchievementController.Instance.AddParam(AchievementController.HERO_DEAD_BY_TRAP);
            MessageDispatcher.SendMessage(GameEvent.HERO_GET_TRAP);
        }

        private void TrapAnimationComplete(IMessage rMessage)
        {
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.TRAP, TrapAnimationComplete);

            Animator anim = _grid[Index].GetObject(ImagesRes.TRAP).GetComponent<Animator>();
            anim.enabled = false;
            _grid[Index].Remove(ImagesRes.TRAP);
        }

        private void LevelComplete()
        {
            MessageDispatcher.SendMessage(GameEvent.LEVEL_COMPLETE);
        }

        private void Move(bool isContinue = false)
        {
            TweenCallback handler = Idle;
            if (isContinue)
                handler = KeepMove;

            _heroState = Hero.MOVE;

            View.transform.DOLocalMove(new Vector3(_grid[Index].X, _grid[Index].Y), SPEED).SetEase(Ease.Linear).OnComplete(handler);
            if (_path.Count == 0 && _grid[Index].IsContainType(ImagesRes.EXIT))
            {
                View.transform.DOKill();
                View.transform.DOLocalMove(new Vector3(_grid[Index].X, _grid[Index].Y - 0.25f), SPEED * 2).SetEase(Ease.Linear).OnComplete(handler);
                View.transform.DOScale(0, SPEED * 2).SetEase(Ease.Linear);//.OnComplete(handler);
                View.GetComponent<SpriteRenderer>().DOFade(0, SPEED * 2).SetEase(Ease.Linear);
            }

            ChangeView();
        }

        private void KeepMove()
        {
            if (_heroState == Hero.DEATH)
                return;

            if (_path.Count == 0)
            {
                Idle();
            }
            else
            {
                MessageDispatcher.SendMessage(GameEvent.HERO_ONE_CELL_AWAY);
                MoveToCell();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            _path = null;
            _grid = null;
        }

        private void Reclothe(string type)
        {
            if (type == ImagesRes.STAR + 0)
            {
                _hasHelmet = true;
                AchievementController.Instance.AddParam(AchievementController.HELMET_TAKED);
            }
            else if (type == ImagesRes.STAR + 1)
            {
                _hasShield = true;
                AchievementController.Instance.AddParam(AchievementController.SHIELD_TAKED);
            }
            else if (type == ImagesRes.STAR + 2)
            {
                _hasSword = true;
                AchievementController.Instance.AddParam(AchievementController.SWORD_TAKED);
            }

            ChangeView();
        }

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