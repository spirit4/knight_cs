using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Unity
{
    public class PanelAchs : MonoBehaviour
    {

        public GameObject vane;
        public GameObject smoke;

        public Text text;
        public Image hint;

        //public static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        void Awake()
        {
            //if (sprites.Count > 0)
            //    return;

            //Sprite[] ss = Resources.LoadAll<Sprite>("images/ui/Achs");
            ////Debug.Log(sprites.Length);
            //foreach (var s in ss)
            //{
            //    sprites.Add(s.name, s);
            //    //Debug.Log("[PanelAchs] " + s.name);
            //}
        }

        // Start is called before the first frame update
        void Start()
        {
            new MainView(vane, smoke);

            createIcons();
        }

        // Update is called once per frame
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                hint.enabled = false;
                text.enabled = false;

                RaycastHit2D hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (hit = Physics2D.Raycast(ray.origin, Vector2.zero))
                {
                    int index = int.Parse(Regex.Match(hit.collider.name, @"\d+").Value);
                    text.text = Progress.hintAchs[index - 1];

                    Vector2 movePos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        this.transform as RectTransform,
                        Input.mousePosition, Camera.main,
                        out movePos);

                    hint.enabled = true;
                    text.enabled = true;
                    //Debug.Log(Input.mousePosition);
                    //Debug.Log(movePos);
                    //Debug.Log(hint.GetComponent<RectTransform>().rect.width + "" + hint.GetComponent<RectTransform>().rect.height);
                    hint.transform.localPosition = new Vector3(movePos.x + hint.GetComponent<RectTransform>().rect.width / 2 + 20,
                        movePos.y - hint.GetComponent<RectTransform>().rect.height / 2 - 10);
                }
            }
        }


        private void createIcons()
        {
            GameObject dObject;
            Sprite sprite;
            int col;
            int row;

            for (int i = 0; i < Progress.achs.Length; i++)
            {
                col = i % 2;
                row = (i - col) / 2;

                dObject = new GameObject("a" + (i + 1));
                dObject.AddComponent<SpriteRenderer>();
                dObject.AddComponent<BoxCollider2D>(); //for click and hint
                dObject.GetComponent<BoxCollider2D>().size = new Vector2(0.72f, 0.72f);

                dObject.GetComponent<SpriteRenderer>().sprite = ImagesRes.getImage("a" + (i + 1));
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

                if (Progress.achs[i] == 0)
                    dObject.GetComponent<SpriteRenderer>().color = Color.gray;

                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(-246 + col * 292, 219 - row * 80);


                dObject = new GameObject("at" + (i + 1));
                dObject.AddComponent<SpriteRenderer>();
                sprite = ImagesRes.getImage("at" + (i + 1));
                dObject.GetComponent<SpriteRenderer>().sprite = sprite;
                dObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

                dObject.transform.SetParent(this.gameObject.transform);
                dObject.transform.localPosition = new Vector3(-198 + col * 293 + sprite.rect.width / 2, 224 - row * 80);
            }
        }

    }
}
