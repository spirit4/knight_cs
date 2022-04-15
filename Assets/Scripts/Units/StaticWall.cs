using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Units
{

    public class StaticWall : TileObject
    {

        public StaticWall(EntityInput config) : base(config)
        {

        }

        public override void Deploy(Transform container, Vector3 position)
        {
            base.Deploy(container, position);
            _view.transform.localPosition = new Vector3(position.x - 0.03f, position.y + 0.07f); 
        }
    }
}
