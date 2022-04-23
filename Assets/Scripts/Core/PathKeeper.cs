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

        private Stack<PathPoint> _poolPoints = new();
        private Queue<PathPoint> _activePoints = new ();
        private Tile[] _grid;

        private TargetMark _targetMark;

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
                    _poolPoints.Push(creator.GetDefault(Entity.Type.PathPoint) as PathPoint);

                AppearPoint(0.05f * i, _pathfinder.Path[i], container);
            }

            if(_targetMark == null)
                _targetMark = creator.GetDefault(Entity.Type.TargetMark) as TargetMark;

            int index = _pathfinder.Path[^1];
            _targetMark.Deploy(container, new Vector3(_grid[index].X, _grid[index].Y));
        }

        private void AppearPoint(float delay, int index, Transform container)
        {
            PathPoint point = _poolPoints.Pop();

            point.DeployWithDelay(container, new Vector3(_grid[index].X, _grid[index].Y), delay);
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

            PathPoint point = _activePoints.Dequeue();
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

            _targetMark = null;
            _grid = null;
        }
    }
}
