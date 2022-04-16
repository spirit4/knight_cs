using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(menuName = "Entities")] //no need after creation
    public sealed class EntityConfig : ScriptableObject
    {
        [SerializeField]
        private List<EntityInput> _entities;

        private void OnValidate()
        {
            foreach (var e in _entities)
            {
                e.Update();
            }
        }

        public EntityInput GetConfig(Entity.Type type)
        {
            return _entities.Find(entity => entity.Type == type);
        }
    }

    [System.Serializable]
    public class EntityInput
    {
        public enum SortingLayer //MUST be a copy SortingLayer.layers
        {
            Back,
            Action,
            UI
        }

        [SerializeField] [HideInInspector] private string _name;
        [SerializeField] private Entity.Type _type;
        [SerializeField] private Entity.Cost _cost;
        [SerializeField] private SortingLayer _layer;
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Sprite[] _sprites;

        public Entity.Type Type { get => _type; }
        public Entity.Cost Cost { get => _cost; }
        public Sprite[] Sprites { get => _sprites; }
        public SortingLayer Layer { get => _layer; }
        public GameObject[] Prefabs { get => _prefabs; }

        public void Update()
        {
            _name = _type.ToString();
        }
    }
}