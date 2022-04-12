using Assets.Scripts.Data;
using Assets.Scripts.Units;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class PathKeeper : IDestroyable
    {
        private Pathfinder _pathfinder;
        public Pathfinder Pathfinder { get => _pathfinder; }

        private Stack<Entity> _poolPoints = new Stack<Entity>();
        private Queue<Entity> _activePoints = new Queue<Entity>();// TODO make stacks
        private Tile[] _grid;

        public PathKeeper()
        {
            _pathfinder = new Pathfinder(Config.WIDTH, Config.HEIGHT);
            _grid = Model.Grid;
        }

        public void ShowPath(Transform container, Creator creator)
        {
            for (int i = 0; i < _pathfinder.Path.Count; i++)
            {
                if (_poolPoints.Count < _pathfinder.Path.Count - i)
                    _poolPoints.Push(creator.GetEntity(Entity.Type.target_0));

                AppearPoint(0.05f * i, _pathfinder.Path[i], container);
            }
        }

        private void AppearPoint(float delay, int index, Transform container)
        {
            Entity point = _poolPoints.Pop();

            (point as PathPoint).DeployWithDelay(container, new Vector3(_grid[index].X, _grid[index].Y), delay);
            _activePoints.Enqueue(point);
        }

        public void HidePoints()
        {
            while (_activePoints.Count > 0)
            {
                HideLastPoint();
            }
        }

        public void HideLastPoint()
        {
            if (_activePoints.Count == 0)
                return;

            Entity point = _activePoints.Dequeue();
            point.Withdraw();
            _poolPoints.Push(point);
        }

        public void Destroy()
        {
            _poolPoints.Clear();
            _poolPoints = null;
            _activePoints.Clear();
            _activePoints = null;

            _pathfinder.Destroy();
            _pathfinder = null;

            _grid = null;
        }
    }
}
