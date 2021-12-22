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
        private GameObject _mainPanel;
        private GameObject _levelsPanel;
        private GameObject _creditsPanel;
        private GameObject _achsPanel;
        private GameObject _introPanel;

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
                _introPanel = GameObject.Find("Panel_Intro");
            }

            if (_levelsPanel)
            {
                _levelsPanel.SetActive(false);
                _creditsPanel.SetActive(false);
                _achsPanel.SetActive(false);
                _introPanel.SetActive(false);
            }

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

    }
}

