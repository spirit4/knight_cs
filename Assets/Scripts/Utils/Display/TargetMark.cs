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
            bitmap.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.TARGET_MARK + 0);
            bitmap.transform.SetParent(container.transform);
            bitmap.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            bitmap.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _center = bitmap;
            bitmap.SetActive(false);

            bitmap = new GameObject();
            bitmap.AddComponent<SpriteRenderer>();
            bitmap.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage(ImagesRes.TARGET_MARK + 1);
            bitmap.transform.SetParent(container.transform);
            bitmap.GetComponent<SpriteRenderer>().sortingLayerName = "Action";
            bitmap.GetComponent<SpriteRenderer>().sortingOrder = 999;
            _outer = bitmap;
            bitmap.SetActive(false);
        }

        public void placeByTap(int index)
        {
            Tile[] grid = Controller.instance.model.grid;

            _center.transform.localScale = new Vector3(0.9f, 0.9f);
            _outer.transform.localScale = new Vector3(0.9f, 0.9f);


            _center.transform.localPosition = new Vector3(grid[index].x, grid[index].y);
            _center.SetActive(true);

            _outer.transform.localPosition = new Vector3(grid[index].x, grid[index].y);
            _outer.SetActive(true);

            appear();
        }

        private void appear()
        {
            _center.transform.DOScale(1.0f, 0.08f).SetEase(Ease.InQuart);
            _outer.transform.DOScale(1.0f, 0.08f).SetEase(Ease.InQuart).OnComplete(extend);
        }

        private void extend()
        {
            _center.transform.DOScale(1.1f, 0.08f).SetEase(Ease.OutQuart);
            _outer.transform.DOScale(1.2f, 0.08f).SetEase(Ease.OutQuart).OnComplete(disappear);
        }

        private void disappear()
        {
            _center.transform.DOScale(0, 0.12f).SetEase(Ease.InQuart);
            _outer.transform.DOScale(0, 0.12f).SetEase(Ease.InQuart).OnComplete(hide);
        }

        private void hide()
        {
            _center.SetActive(false);
            _outer.SetActive(false);
        }

    }
}
