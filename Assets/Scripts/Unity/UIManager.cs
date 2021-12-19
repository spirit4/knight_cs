using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Unity
{
    public class UIManager : MonoBehaviour
    {
        //public const string MAIN_MENU = "ButtonPlay_MainMenu";
        //public const string LEVELS = "ButtonBack_Levels";
        //public const GAME string = "game";
        //public const CREDITS string = "credits";
        //public const INTRO string = "intro";
        //public const ACHS string = "achs";

        private GameObject _mainPanel;
        private GameObject _levelsPanel;
        private GameObject _creditsPanel;
        private GameObject _achsPanel;
        //private GameObject _popup;//// all inside

        private static bool isFirstLoad = true;

        private void Awake()
        {
            //Debug.Log("UIManager awake");
            this.gameObject.AddComponent<AudioSource>();
            SoundManager.getInstance().init(this.gameObject.GetComponent<AudioSource>());
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!_levelsPanel)
            {
                _levelsPanel = GameObject.Find("Panel_Levels");
                _mainPanel = GameObject.Find("Panel_MainMenu");
                _creditsPanel = GameObject.Find("Panel_Credits");
                _achsPanel = GameObject.Find("Panel_Achs");
            }

            if (_levelsPanel)
            {
                _levelsPanel.SetActive(false);
                _creditsPanel.SetActive(false);
                _achsPanel.SetActive(false);
            }
            //if (!_popup)
            //{
            //    _popup = GameObject.Find("PanelVictory");
            //    //_popup.SetActive(false);
            //}

            if (GameObject.Find("Panel_MainMenu") != null)
                SoundManager.getInstance().setLocation(SoundManager.MUSIC_MENU);
            else
                SoundManager.getInstance().setLocation(SoundManager.MUSIC_GAME);

            //if(MessageDispatcher.) // TODO ?
            MessageDispatcher.AddListener(GameEvent.RESTART, RestartGame);

            
            //menu from game
            if (_levelsPanel && !UIManager.isFirstLoad)
            {
                _mainPanel.SetActive(false);
                _levelsPanel.SetActive(true);
            }

            UIManager.isFirstLoad = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SwitchPanels(GameObject newPanel)//, GameObject oldPanel)
        {

            Debug.Log(newPanel);
            //switch (button.name)
            //{
            //    case UIManager.MAIN_MENU:
            //        LevelsPanel = GameObject.Find("Panel_MainMenu");
            //        LevelsPanel.SetActive(false);
            //        LevelsPanel = GameObject.Find("LevelsPanel");
            //        break;
            //case View.CREDITS:
            //    this._currentView = new Credits();
            //    break;
            //case UIManager.LEVELS:
            //    LevelsPanel = GameObject.Find("LevelsPanel");
            //    LevelsPanel.SetActive(false);
            //    LevelsPanel = GameObject.Find("Panel_MainMenu");
            //    break;
            //case View.INTRO:
            //    this._currentView = new Intro();
            //    break;
            //case View.ACHS:
            //    this._currentView = new Achievements();
            //    break;
            //default:
            //console.log("need addView", type);
            //}
            //LevelsPanel.SetActive(true);
        }

        //logic in Editor's buttons
        public void ShowNewWindow(GameObject window)
        {
            window.SetActive(true);
        }

        public void HideOldWindow(GameObject window)
        {
            window.SetActive(false);
        }

        //not by button
        private void RestartGame(IMessage m)
        {
            StartGame();
        }


        public void GoToNextLevel()
        {
            Progress.currentLevel++;
            StartGame();
        }

        public void GoToLevel(Text level)
        {
            Progress.currentLevel = int.Parse(level.text) - 1;
            StartGame();
        }

        public void StartGame()
        {
            //Debug.Log("StartGame");
            SceneManager.LoadScene("GameScene");
            SoundManager.getInstance().setLocation(SoundManager.MUSIC_GAME);
        }

        public void EndGame()
        {
            //Debug.Log("EndGame");
            SceneManager.LoadScene("MainScene");
            SoundManager.getInstance().setLocation(SoundManager.MUSIC_MENU);
        }

        public void PostponeAnimation()
        {

        }

    }
}
