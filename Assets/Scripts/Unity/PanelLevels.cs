using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Unity
{
    public class PanelLevels : MonoBehaviour
    {



        // Start is called before the first frame update
        void Start()
        {
            ShowButtons();
        }


        private void ShowButtons()
        {
            Text level;
            Transform transform;
            for (int i = 3; i < this.transform.childCount; i++)
            {
                //Debug.Log("ShowButtons: " + this.transform.GetChild(i).name);

                transform = this.transform.GetChild(i);
                if (i - 3 < Progress.levelsCompleted)
                {
                    level = transform.GetChild(0).GetChild(0).GetComponent<Text>() as Text;
                    level.text = (i - 2).ToString();

                    if (Progress.starsAllLevels[i - 3, 0] == 0)
                        transform.GetChild(2).gameObject.SetActive(false);

                    if (Progress.starsAllLevels[i - 3, 2] == 0)
                        transform.GetChild(4).gameObject.SetActive(false);

                    if (Progress.starsAllLevels[i - 3, 1] == 0)
                        transform.GetChild(6).gameObject.SetActive(false);
                }
                else
                {
                    transform.gameObject.SetActive(false);
                }
            }
        }

        private void OnMouseDown()// movedownHandler(e: createjs.MouseEvent) : void
        {
            Debug.Log("ShowButtons: " );
        }
    }
}
