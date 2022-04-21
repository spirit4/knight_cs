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
            PathPoint,
            pine,
            stone,
            star,
            stump,
            brige,//artist's spelling
            exit,
            mill,
            spikes,
            tower,
            arrow,
            boulder,
            boulderMark,
            trap,
            wolwpig,
            hero,
            TargetMark, //TODO rename them
        }

        //this should be in TileObject
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

        /// <summary>
        /// common Type of Entity (get => _type)
        /// </summary>
        public Type Kind { get => _type; }

        public Entity(EntityInput config)
        {
            _type = config.Type;

            CreateView(config.Layer.ToString());
        }

        public virtual void CreateView(string layer)
        {
            _view = new GameObject(_type.ToString());
            _view.AddComponent<SpriteRenderer>();
            _view.GetComponent<SpriteRenderer>().sortingLayerName = layer;
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
