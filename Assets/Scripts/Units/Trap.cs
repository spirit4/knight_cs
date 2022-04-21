using Assets.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Trap : TileObject
    {
        public Trap(EntityInput config) : base(config)
        {
            //Debug.Break();
        }

        public override void Deploy(Transform container, Vector3 position)
        {
            base.Deploy(container, position);
            _view.transform.localPosition = new Vector3(position.x, position.y + 0.03f);
        }
        public override void CreateView(string layer)
        {
            //empty
        }

        public override void AddView(Sprite[] spites, int spriteIndex)
        {
            _view = Object.Instantiate(_config.Prefabs[spriteIndex]);
            _view.GetComponent<SpriteRenderer>().sortingLayerName = _config.Layer.ToString();
            
            _view.GetComponent<Animator>().enabled = false;

            Color color = _view.GetComponent<SpriteRenderer>().color;
            color.a = 0.4f;
            _view.GetComponent<SpriteRenderer>().color = color;
        }

        public void Activate()
        {
            _view.GetComponent<Animator>().enabled = true;
            _view.GetComponent<SpriteRenderer>().DOFade(1, 0.2f).SetEase(Ease.OutQuad);
        }

        public void Stop()
        {
            _view.GetComponent<Animator>().enabled = false;
        }
    }
}