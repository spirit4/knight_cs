using Assets.Scripts.Core;
using Assets.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Utils.Display
{
    public class TargetMark
    {
        private GameObject _center;
        private GameObject _outer;

        public TargetMark(GameObject container)
        {
            GameObject bitmap = new GameObject();
            bitmap.AddComponent<SpriteRenderer>();
            bitmap.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.TARGET_MARK + 0);
            bitmap.transform.SetParent(container.transform);
            bitmap.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            bitmap.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _center = bitmap;
            bitmap.SetActive(false);

            bitmap = new GameObject();
            bitmap.AddComponent<SpriteRenderer>();
            bitmap.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(ImagesRes.TARGET_MARK + 1);
            bitmap.transform.SetParent(container.transform);
            bitmap.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            bitmap.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _outer = bitmap;
            bitmap.SetActive(false);
        }

        public void PlaceByTap(int index)
        {
            Tile[] grid = Model.Grid;

            _center.transform.localScale = new Vector3(0.9f, 0.9f);
            _outer.transform.localScale = new Vector3(0.9f, 0.9f);


            _center.transform.localPosition = new Vector3(grid[index].X, grid[index].Y);
            _center.SetActive(true);

            _outer.transform.localPosition = new Vector3(grid[index].X, grid[index].Y);
            _outer.SetActive(true);

            Appear();
        }

        private void Appear()
        {
            _center.transform.DOScale(1.0f, 0.08f).SetEase(Ease.InQuart);
            _outer.transform.DOScale(1.0f, 0.08f).SetEase(Ease.InQuart).OnComplete(Extend);
        }

        private void Extend()
        {
            _center.transform.DOScale(1.1f, 0.08f).SetEase(Ease.OutQuart);
            _outer.transform.DOScale(1.2f, 0.08f).SetEase(Ease.OutQuart).OnComplete(Disappear);
        }

        private void Disappear()
        {
            _center.transform.DOScale(0, 0.12f).SetEase(Ease.InQuart);
            _outer.transform.DOScale(0, 0.12f).SetEase(Ease.InQuart).OnComplete(Hide);
        }

        private void Hide()
        {
            _center.SetActive(false);
            _outer.SetActive(false);
        }

    }
}
