using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public abstract class Entity : IDestroyable
    {
        public enum Type
        {
            grass,//TODO PascalCase
            water,
            Decor,//case is important for now
            target_0,
        }

        public enum Cost : int
        {
            None,
            Normal,
            Wall,
            Expensive,
        }

        protected GameObject _view;
        protected Type _type;

        public GameObject View { get => _view; }

        public Entity(EntityInput config)
        {
            _type = config.Type;

            _view = new GameObject(_type.ToString());
            _view.AddComponent<SpriteRenderer>();
            _view.GetComponent<SpriteRenderer>().sortingLayerName = config.Layer.ToString();
            _view.SetActive(false);
        }

        public virtual void AddView(Sprite[] spites, int spriteIndex = 0)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[spriteIndex];
        }

        public virtual void Deploy(Transform container, Vector3 position)
        {
            _view.transform.SetParent(container);
            _view.transform.localPosition = position;
            _view.SetActive(true);
        }

        public virtual void Withdraw()
        {
            _view.SetActive(false);
        }

        public virtual void Destroy()
        {
            UnityEngine.Object.Destroy(_view);
            _view = null;
        }
    }
}
