using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Unity
{
    public class PanelVictory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _vane;


        //logic in Editor's buttons
        public void Activate(IMessage rMessage = null)
        {
            this.gameObject.SetActive(true);

            if (Progress.currentLevel != 19)
                RotateMill();
            else
            {
                this.gameObject.GetComponent<Image>().sprite = ImagesRes.getImage("game_end");
                _vane.SetActive(false);
            }

            if (Progress.deadOnLevel[Progress.currentLevel] == 0)
                AchController.instance.addParam(AchController.LEVEL_WITHOUT_DEATH);

            CreateStars();

            if (Progress.starsAllLevels.Length > Progress.levelsCompleted && Progress.currentLevel + 1 == Progress.levelsCompleted)
                Progress.levelsCompleted++;

            Controller.instance.model.saveProgress();
        }

        public void CreateStars()
        {
            GameObject dObject;
            Color color;
            Sequence sequence = DOTween.Sequence();

            if (Progress.starsAllLevels[Progress.currentLevel, 2] == 1)
            {
                dObject = new GameObject();
                dObject.AddComponent<SpriteRenderer>();
                dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage("LevelSword");
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                dObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(53.2f + 50, 17.0f + 50);
                dObject.transform.localScale = new Vector3(200, 200);

                color = dObject.GetComponent<SpriteRenderer>().color;
                color.a = 0f;
                dObject.GetComponent<SpriteRenderer>().color = color;

                sequence.Append(dObject.transform.DOLocalMove(new Vector3(53.2f, 17.0f), 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.transform.DOScale(100, 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.OutQuart));
            }

            if (Progress.starsAllLevels[Progress.currentLevel, 0] == 1)
            {
                dObject = new GameObject();
                dObject.AddComponent<SpriteRenderer>();
                dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage("LevelHelmet");
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                dObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(21.9f + 50, -28.4f + 50);
                dObject.transform.localScale = new Vector3(200, 200);

                color = dObject.GetComponent<SpriteRenderer>().color;
                color.a = 0f;
                dObject.GetComponent<SpriteRenderer>().color = color;

                sequence.Insert(0.25f, dObject.transform.DOLocalMove(new Vector3(21.9f, -28.4f), 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.transform.DOScale(100, 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.OutQuart));
            }
            if (Progress.starsAllLevels[Progress.currentLevel, 1] == 1)
            {
                dObject = new GameObject();
                dObject.AddComponent<SpriteRenderer>();
                dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage("LevelShield");
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                dObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(-10.8f + 50, 17.8f + 50);
                dObject.transform.localScale = new Vector3(200, 200);

                color = dObject.GetComponent<SpriteRenderer>().color;
                color.a = 0f;
                dObject.GetComponent<SpriteRenderer>().color = color;

                sequence.Insert(0.5f, dObject.transform.DOLocalMove(new Vector3(-10.8f, 17.8f), 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.transform.DOScale(100, 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.OutQuart));
            }
        }

        private void RotateMill()
        {
            _vane.transform.DORotate(new Vector3(0, 0, -395), 3.0f).SetLoops(-1).SetEase(Ease.Linear);
        }

    }
}
