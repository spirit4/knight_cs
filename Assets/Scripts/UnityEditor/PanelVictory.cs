using Assets.Scripts.Achievements;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityEditor
{
    public class PanelVictory : BasePanel
    {
        protected override void Start()
        {
            GameEvents.LevelCompleteHandlers += Activate;
        }

        private void Activate()
        {
            this.gameObject.SetActive(true);

            if (Progress.CurrentLevel != 19)
                RotateMill();
            else
            {
                this.gameObject.GetComponent<Image>().sprite = ImagesRes.GetImage("game_end");
                _vane.gameObject.SetActive(false);
            }

            if (Progress.DeadOnLevel[Progress.CurrentLevel] == 0)
                GameEvents.AchTriggered(Trigger.TriggerType.LevelWithoutDeath);

            ShowStars();

            if (Progress.StarsAllLevels.Length > Progress.LevelsCompleted && Progress.CurrentLevel + 1 == Progress.LevelsCompleted)
                Progress.LevelsCompleted++;

            Saver.SaveProgress();
        }

        private void ShowStars()
        {
            Sequence sequence = DOTween.Sequence();

            if (Progress.StarsAllLevels[Progress.CurrentLevel, 2] == 1)
                CreateStar("LevelSword", new Vector3(53.2f, 17.0f), new Vector3(53.2f, 17.0f), sequence, 0);

            if (Progress.StarsAllLevels[Progress.CurrentLevel, 0] == 1)
                CreateStar("LevelHelmet", new Vector3(21.9f, -28.4f), new Vector3(21.9f, -28.4f), sequence, 0.25f);

            if (Progress.StarsAllLevels[Progress.CurrentLevel, 1] == 1)
                CreateStar("LevelShield", new Vector3(-10.8f, 17.8f), new Vector3(-10.8f, 17.8f), sequence, 0.5f);
        }

        private GameObject CreateStar(string sprite, Vector3 from, Vector3 to, Sequence sequence, float delay)
        {
            var star = new GameObject(sprite, typeof(SpriteRenderer));
            star.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage(sprite);
            star.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            star.GetComponent<SpriteRenderer>().sortingOrder = 1;
            star.transform.SetParent(this.gameObject.transform);
            star.transform.localPosition = from + new Vector3(50, 50);
            star.transform.localScale = new Vector3(200, 200);

            Color color = star.GetComponent<SpriteRenderer>().color;
            color.a = 0f;
            star.GetComponent<SpriteRenderer>().color = color;

            sequence.Insert(delay, star.transform.DOLocalMove(to, 0.5f).SetEase(Ease.OutQuart));
            sequence.Join(star.transform.DOScale(100, 0.5f).SetEase(Ease.OutQuart));
            sequence.Join(star.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.OutQuart));

            return star;
        }


        private void OnDestroy()
        {
            GameEvents.LevelCompleteHandlers -= Activate;
        }
    }
}
