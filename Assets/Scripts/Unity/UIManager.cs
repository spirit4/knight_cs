using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Unity
{
    public class UIManager : MonoBehaviour
    {
        public const string MAIN_MENU = "ButtonPlay_MainMenu";
        public const string LEVELS = "ButtonBack_Levels";
        //public const GAME string = "game";
        //public const CREDITS string = "credits";
        //public const INTRO string = "intro";
        //public const ACHS string = "achs";

        private GameObject _levelsPanel;
        private GameObject _popup;

        // Start is called before the first frame update
        void Start()
        {
            if (!_levelsPanel)
                _levelsPanel = GameObject.Find("Panel_Levels");

            if (_levelsPanel)
                _levelsPanel.SetActive(false);

            //if (!_popup)
            //{
            //    _popup = GameObject.Find("PanelVictory");
            //    _popup.SetActive(false);
            //}

            //if(MessageDispatcher.) // TODO ?
            MessageDispatcher.AddListener(GameEvent.RESTART, RestartGame);
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

        private void RestartGame(IMessage m)
        {
            StartGame();
        }


        public void GoToNextLevel()
        {
            Progress.currentLevel++;
            StartGame();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void EndGame()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
