using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Entity : IDestroyable
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
        //protected int _index;
        protected Type _type;

        public GameObject View { get => _view; }

        public Entity(EntityInput config)//Type type, string layer, int sortingOrder)
        {
            _type = config.Type;

            _view = new GameObject(_type.ToString());
            _view.AddComponent<SpriteRenderer>();
            _view.GetComponent<SpriteRenderer>().sortingLayerName = config.Layer.ToString();
            //_view.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            _view.SetActive(false);
            //Debug.Log($"new Entity {_type}    view {_view}");
        }

        public virtual void AddSprite(Sprite[] spites)
        {
            //Debug.Log($"AddSprite {_type}    view {_view}");
            _view.GetComponent<SpriteRenderer>().sprite = spites[0];
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
