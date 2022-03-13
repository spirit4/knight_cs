using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UnityEditor
{
    public class PanelCredits : MonoBehaviour 
    {
        [SerializeField]
        private GameObject _vane;
        [SerializeField]
        private GameObject _smoke;

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("PanelMainMenu Start");
            new MainView(_vane, _smoke);

            Text version = GameObject.Find("Canvas/PanelCredits/TextVersion").GetComponent<Text>();
            version.text = Config.GAME_VERSION;
        }

    }
}
