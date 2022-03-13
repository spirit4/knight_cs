using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UnityEditor
{
    public class PanelLevels : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("PanelLeveks Start");
            ShowButtons();
        }

        private void ShowButtons()
        {
            Text level;
            Transform transform;
            for (int i = 3; i < this.transform.childCount; i++)
            {

                transform = this.transform.GetChild(i);
                if (i - 3 < Progress.LevelsCompleted)
                {
                    level = transform.GetChild(0).GetChild(0).GetComponent<Text>() as Text;
                    level.text = (i - 2).ToString();

                    if (Progress.StarsAllLevels[i - 3, 0] == 0)
                        transform.GetChild(0).GetChild(2).gameObject.SetActive(false);//helmet deeper for swayer

                    if (Progress.StarsAllLevels[i - 3, 2] == 0)
                        transform.GetChild(2).gameObject.SetActive(false);

                    if (Progress.StarsAllLevels[i - 3, 1] == 0)
                        transform.GetChild(4).gameObject.SetActive(false);
                }
                else
                {
                    transform.gameObject.SetActive(false);
                }

                if (i - 3 == Progress.LevelsCompleted - 1)
                {
                    transform.GetChild(0).gameObject.AddComponent<Swayer>();
                }
            }
        }
    }
}
