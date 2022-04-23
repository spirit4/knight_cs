using Assets.Scripts.Units;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class Tile : IDestroyable
    {

        private readonly float _x;
        private readonly float _y;
        private readonly int _index;

        private List<Entity> _entities;

        private Entity.Cost _cost;

        public float X { get => _x;  }
        public float Y { get => _y;  }
        public int Index { get => _index;  }
        public Entity.Cost Cost { get => _cost; }

        public Tile(float x, float y, int index)
        {
            _x = x;
            _y = y;
            _index = index;
            _entities = new List<Entity>();
        }

        public void SetCost(Entity.Cost cost)
        {
            _cost = cost;
        }

        public void AddEntity(Entity entity)
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);
        }

        public void RemoveEntity(Entity.Type type)//one type per tile
        {
            _entities.Remove(_entities.Find(entity => entity.Kind == type));
        }
        public void RemoveEntity(Entity entity)//one type per tile
        {
            _entities.Remove(entity);
        }

        public Entity GetEntity(Entity.Type type)
        {
            return _entities.Find(entity => entity.Kind == type);
        }

        public bool IsContainType(Entity.Type type)
        {
            if (_entities.Find(entity => entity.Kind == type) != null)
                return true;

            return false;
        }

        public bool IsContainDirection()
        {
            if (_entities.Find(entity => entity is Direction) != null)
                return true;

            return false;
        }

        public void Destroy()
        {
            foreach (Entity e in _entities)
            {
                e.Destroy();
            }
            _entities.Clear();
            _entities = null;
        }
    }
}
