using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public abstract class MovingUnit : TileObject, ICollidable
    {

        public MovingUnit(EntityInput config) : base(config)
        {
            
        }

        public virtual void Stop()
        {
            _view.transform.DOKill();
        }
        public override void Destroy()
        {
            _view?.transform.DOKill();//destroy on boom and restarting level
            _view?.GetComponent<SpriteRenderer>().DOKill();
            base.Destroy();
        }

        public new string Type { get; }//temp
    }
}
