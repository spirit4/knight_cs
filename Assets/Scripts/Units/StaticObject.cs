using UnityEngine;

namespace Assets.Scripts.Units
{
    public class StaticObject : Entity
    {
        private bool _isWall = false;//for pathfinder
        private bool _isExpensive = false;//for pathfinder for mill

        public bool IsWall { get => _isWall; }
        public bool IsExpensive { get => _isExpensive; }

        public StaticObject(EntityInput config) : base(config)
        {
            if (config.Cost == Cost.Wall)//TODO refactor it in pathfinder first
                _isWall = true;
            else if (config.Cost == Cost.Expensive)
                _isExpensive = true;

            _view.isStatic = true;
        }

        public override void AddSprite(Sprite[] spites)
        {
            _view.GetComponent<SpriteRenderer>().sprite = spites[Random.Range(0, spites.Length)];
        }
    }
}

