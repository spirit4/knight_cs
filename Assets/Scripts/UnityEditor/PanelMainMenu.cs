using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UnityEditor
{
    public class PanelMainMenu : MonoBehaviour 
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
        }

    }
}