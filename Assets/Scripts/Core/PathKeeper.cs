using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class PathKeeper : IDestroyable
    {
        private Pathfinder _pathfinder;
        public Pathfinder Pathfinder { get => _pathfinder; }

        private List<GameObject> _poolPoints = new List<GameObject>();
        private List<GameObject> _activePoints = new List<GameObject>();
        private Tile[] _grid;

        public PathKeeper()
        {
            _pathfinder = new Pathfinder(Config.WIDTH, Config.HEIGHT);
            _grid = Model.Grid;
        }

        public void ShowPath(Transform container)
        {
            Sequence s = DOTween.Sequence();
            for (int i = 0; i < _pathfinder.Path.Count; i++)
            {
                if (_poolPoints.Count < _pathfinder.Path.Count)
                    CreatePathPoint(container);

                AppearPoint(i, _pathfinder.Path[i]);
            }
        }

        private void AppearPoint(int i, int index)
        {
            GameObject bitmap = _poolPoints[_poolPoints.Count - 1];
            _poolPoints.RemoveAt(_poolPoints.Count - 1);

            bitmap.transform.localScale = new Vector3(0, 0);
            bitmap.transform.localPosition = new Vector3(_grid[index].X, _grid[index].Y, 0);
            bitmap.SetActive(true);
            _activePoints.Add(bitmap);

            DOTween.Sequence().AppendInterval(0.05f * i).Append(bitmap.transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutQuart))
                .OnComplete(() => bitmap.transform.DOScale(1, 0.06f).SetEase(Ease.InQuart));
        }

        public void HidePoints()
        {
            while (_activePoints.Count > 0)
            {
                GameObject bitmap = _activePoints[_activePoints.Count - 1];
                _activePoints.RemoveAt(_activePoints.Count - 1);
                bitmap.SetActive(false);
                _poolPoints.Add(bitmap);
            }
        }

        public void HideLastPoint()
        {
            if (_activePoints.Count == 0)
                return;

            GameObject bitmap = _activePoints[0];
            _activePoints.RemoveAt(0);
            bitmap.SetActive(false);
            _poolPoints.Add(bitmap);
        }

        private void CreatePathPoint(Transform container)
        {
            GameObject dObject = new GameObject();
            dObject.AddComponent<SpriteRenderer>();
            dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.TARGET_MARK + 0);
            dObject.transform.SetParent(container);
            dObject.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            dObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            dObject.SetActive(false);

            _poolPoints.Add(dObject);
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
