using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Unity
{
    public class PanelMainMenu : MonoBehaviour  //TODO new one for cresits, achs?!
    {

        public GameObject vane;
        public GameObject smoke;

        // Start is called before the first frame update
        void Start()
        {
            new MainView(vane, smoke);
        }

        // Update is called once per frame
        public void Update()
        {

        }


    }
}
