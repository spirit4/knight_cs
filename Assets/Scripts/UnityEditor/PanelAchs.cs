using Assets.Scripts.Data;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityEditor
{
    public class PanelAchs : BasePanel
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Image _hint;

        // Start is called before the first frame update
       protected override void Start()
        {
            base.Start();
            CreateIcons();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hint.enabled = false;
                _text.enabled = false;

                RaycastHit2D hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (hit = Physics2D.Raycast(ray.origin, Vector2.zero))
                {
                    int index = int.Parse(Regex.Match(hit.collider.name, @"\d+").Value);
                    _text.text = Progress.HintAchievements[index - 1];

                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        this.transform as RectTransform,
                        Input.mousePosition, Camera.main,
                        out Vector2 movePos);

                    _hint.enabled = true;
                    _text.enabled = true;

                    _hint.transform.localPosition = new Vector3(movePos.x + _hint.GetComponent<RectTransform>().rect.width / 2 + 20,
                        movePos.y - _hint.GetComponent<RectTransform>().rect.height / 2 - 10);
                }
            }
        }

        private void CreateIcons()
        {
            GameObject dObject;
            Sprite sprite;
            int col;
            int row;

            for (int i = 0; i < Progress.Achievements.Length; i++)
            {
                col = i % 2;
                row = (i - col) / 2;

                dObject = new GameObject("a" + (i + 1));
                dObject.AddComponent<SpriteRenderer>();
                dObject.AddComponent<BoxCollider2D>(); //for click and hint
                dObject.GetComponent<BoxCollider2D>().size = new Vector2(0.72f, 0.72f);

                dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.GetImage("a" + (i + 1));
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

                if (Progress.Achievements[i] == 0)
                    dObject.GetComponent<SpriteRenderer>().color = Color.gray;

                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(-246 + col * 292, 219 - row * 80);


                dObject = new GameObject("at" + (i + 1));
                dObject.AddComponent<SpriteRenderer>();
                sprite = ImagesRes.GetImage("at" + (i + 1));
                dObject.GetComponent<SpriteRenderer>().sprite = sprite;
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(-198 + col * 293 + sprite.rect.width / 2, 224 - row * 80);
            }
        }

    }
}
