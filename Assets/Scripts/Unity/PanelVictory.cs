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
        public GameObject vane;

        // Start is called before the first frame update
        void Awake()
        {
            this.gameObject.SetActive(false);

            //if (Progress.starsAllLevels.length > Progress.levelsCompleted && Progress.currentLevel + 1 == Progress.levelsCompleted)
            //{
            //    Progress.levelsCompleted++;
            //}
            MessageDispatcher.AddListener(GameEvent.LEVEL_COMPLETE, Activate);
        }

        // Update is called once per frame
        public void Update()
        {

        }


        //logic in Editor's buttons
        public void Activate(IMessage rMessage = null)
        {
            MessageDispatcher.RemoveListener(GameEvent.LEVEL_COMPLETE, Activate); //TODO destroy?
            this.gameObject.SetActive(true);

            if (Progress.currentLevel != 19)
                RotateMill();
            else
            {
                this.gameObject.GetComponent<Image>().sprite = ImagesRes.getImage("game_end");
                vane.SetActive(false);
            }

            CreateStars();
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
                //dObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
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
                //    bd = ImagesRes.getImage(ImagesRes.STAR + '2');
                //    bitmap = new createjs.Bitmap(bd);
                //    bitmap.snapToPixel = true;
                //    this.addChild(bitmap);
                //    bitmap.regX = bd.width >> 1;
                //    bitmap.regY = bd.height >> 1;
                //    bitmap.x = 347 + bitmap.regX;
                //    bitmap.y = 315 + bitmap.regY;

                //sequence.AppendInterval(0.25f);
                sequence.Insert(0.25f, dObject.transform.DOLocalMove(new Vector3(21.9f, -28.4f), 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.transform.DOScale(100, 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.OutQuart));
                //    createjs.Tween.get(bitmap, { ignoreGlobalPause: true }).wait(250).from({ alpha: 0, scaleX: 2, scaleY: 2, y: bitmap.y - 50 }, 500, createjs.Ease.quartOut);
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

                //sequence.AppendInterval(0.25f);
                sequence.Insert(0.5f, dObject.transform.DOLocalMove(new Vector3(-10.8f, 17.8f), 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.transform.DOScale(100, 0.5f).SetEase(Ease.OutQuart));
                sequence.Join(dObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetEase(Ease.OutQuart));
            }

            

            //if (Progress.starsAllLevels.length > Progress.levelsCompleted && Progress.currentLevel + 1 == Progress.levelsCompleted)
            //{
            //    Progress.levelsCompleted++;
            //}
        }

        private void RotateMill()
        {
            vane.transform.DORotate(new Vector3(0, 0, -395), 3.0f).SetLoops(-1).SetEase(Ease.Linear);
        }

    }
}
