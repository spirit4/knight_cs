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
            _grid = Controller.instance.model.grid;

            this.x = _grid[index].x; //for choosing direction
            this.y = _grid[index].y;

            _inside = inside;

            view.AddComponent<SpriteRenderer>();
            view.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            view.GetComponent<SpriteRenderer>().sortingOrder = 100;

            _inside.name = type;
            _inside.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            _inside.GetComponent<SpriteRenderer>().sortingOrder = 100;

            view.transform.localPosition = new Vector3(this.x, this.y);
            _inside.transform.localPosition = new Vector3(MARGIN_X, MARGIN_Y);

            _heroState = IDLE;
        }


        public override void stop()
        {
            _path.Clear();
        }

        public void moveToCell(List<int> path = null)
        {
            if (path != null)
                _path = path;

            int currentIndex = index;
            index = _path[0];
            _path.RemoveAt(0);
            Vector2 point = GridUtils.GetPoint(index);

            _directionX = 0;
            //_directionY = 0;


            this.x = view.transform.localPosition.x;
            this.y = view.transform.localPosition.y;

            int step = 0;
            if (point.y == this.y && point.x > this.x)
            {
                step = 1;
                _directionX = 1;
                _inside.transform.localPosition = new Vector3(MARGIN_X, MARGIN_Y);
            }
            else if (point.y == this.y && point.x < this.x)
            {
                step = -1;
                _directionX = -1;
                _inside.transform.localPosition = new Vector3(-MARGIN_X, MARGIN_Y);
            }
            else if (point.x == this.x && point.y > this.y)
            {
                step = -Config.WIDTH;
            }
            else if (point.x == this.x && point.y < this.y)
            {
                step = Config.WIDTH;
            }

            this.x = view.transform.localPosition.x;
            this.y = view.transform.localPosition.y;
            view.transform.localPosition = new Vector3(_grid[currentIndex].x, _grid[currentIndex].y);

            int hack = 0;
            while (this.index != currentIndex)
            {
                currentIndex += step;

                if (_grid[currentIndex].isContainTypes(ImagesRes.STAR) || _grid[currentIndex].isContainType(ImagesRes.TRAP))
                {
                    this.index = currentIndex;
                    _path.Clear();
                }
                hack++;
                if (hack >= 100)
                    break;
            }

            move(true);

        }

        private void changeView()
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
        private void idle()
        {
            if (_heroState == Hero.DEATH)
                return;

            if (_grid[index].isContainTypes(ImagesRes.STAR))
            {
                string type = _grid[this.index].getConcreteType(ImagesRes.STAR);
                int index = _grid[this.index].types.IndexOf(type);

                GameObject dObject = _grid[this.index].objects[index];

                dObject.transform.DOScale(0, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => starTweenComplete(type));
            }

            else if (_grid[this.index].isContainType(ImagesRes.TRAP))
            {
                GameObject trap = _grid[this.index].getObject(ImagesRes.TRAP);

                Animator anim = trap.GetComponent<Animator>();
                anim.enabled = true;
                trap.GetComponent<SpriteRenderer>().DOFade(1, 0.15f).SetEase(Ease.OutQuad);
                DOTween.Sequence().AppendInterval(0.15f).AppendCallback(trapTweenComplete);
                MessageDispatcher.AddListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.TRAP, trapAnimationComplete, false);
            }
            else if (_grid[this.index].isContainType(ImagesRes.EXIT))
            {
                if (_hasHelmet)
                    Progress.starsAllLevels[Progress.currentLevel, 0] = 1;

                if (_hasShield)
                    Progress.starsAllLevels[Progress.currentLevel, 1] = 1;

                if (_hasSword)
                    Progress.starsAllLevels[Progress.currentLevel, 2] = 1;

                if (!_hasHelmet && !_hasShield && !_hasSword)
                    AchController.instance.addParam(AchController.LEVEL_WITHOUT_ITEMS);

                levelComplete();
            }

            _heroState = Hero.IDLE;

            changeView();

            MessageDispatcher.SendMessage(GameEvent.HERO_REACHED);
        }

        private void starTweenComplete(string type)
        {
            //console.log("starTweenComplete", type, this.index);
            _grid[this.index].remove(type);
            reclothe(type);
        }

        private void trapTweenComplete()
        {
            AchController.instance.addParam(AchController.HERO_DEAD_BY_TRAP);
            MessageDispatcher.SendMessage(GameEvent.HERO_GET_TRAP);
        }

        private void trapAnimationComplete(IMessage rMessage)
        {
            MessageDispatcher.RemoveListener(GameEvent.ANIMATION_COMPLETE, ImagesRes.TRAP, trapAnimationComplete);

            Animator anim = _grid[this.index].getObject(ImagesRes.TRAP).GetComponent<Animator>();
            anim.enabled = false;
            _grid[this.index].remove(ImagesRes.TRAP);
        }

        private void levelComplete()
        {
            //Debug.Log("EXIT");
            //    this.view.removeAllEventListeners();
            MessageDispatcher.SendMessage(GameEvent.LEVEL_COMPLETE);
        }

        private void move(bool isContinue = false)
        {
            TweenCallback handler = idle;
            if (isContinue)
                handler = keepMove;

            _heroState = Hero.MOVE;

            view.transform.DOLocalMove(new Vector3(_grid[index].x, _grid[index].y), SPEED).SetEase(Ease.Linear).OnComplete(handler);
            if (_path.Count == 0 && _grid[this.index].isContainType(ImagesRes.EXIT))
            {
                view.transform.DOKill();
                view.transform.DOLocalMove(new Vector3(_grid[index].x, _grid[index].y - 0.25f), SPEED * 2).SetEase(Ease.Linear).OnComplete(handler);
                view.transform.DOScale(0, SPEED * 2).SetEase(Ease.Linear);//.OnComplete(handler);
                view.GetComponent<SpriteRenderer>().DOFade(0, SPEED * 2).SetEase(Ease.Linear);
            }

            changeView();
        }

        private void keepMove()
        {
            if (_heroState == Hero.DEATH)
                return;

            if (_path.Count == 0)
            {
                idle();
            }
            else
            {
                MessageDispatcher.SendMessage(GameEvent.HERO_ONE_CELL_AWAY);
                moveToCell();
            }
        }

        public override void destroy()
        {
            base.destroy();
            _path = null;
            _grid = null;
        }

        private void reclothe(string type)
        {
            if (type == ImagesRes.STAR + 0)
            {
                _hasHelmet = true;
                AchController.instance.addParam(AchController.HELMET_TAKED);
            }
            else if (type == ImagesRes.STAR + 1)
            {
                _hasShield = true;
                AchController.instance.addParam(AchController.SHIELD_TAKED);
            }
            else if (type == ImagesRes.STAR + 2)
            {
                _hasSword = true;
                AchController.instance.addParam(AchController.SWORD_TAKED);
            }

            changeView();
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