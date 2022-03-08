using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
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

        private GameObject _victoryPanel;

        private static bool isFirstLoad = true;

        private void Awake()
        {
            MessageDispatcher.ClearListeners();//after scene reload

            if (ImagesRes.prefabs.Count == 0) // loading resources to static
                ImagesRes.init();

            //Debug.Log("UIManager awake");
            this.gameObject.AddComponent<AudioSource>();
            SoundManager.getInstance().init(this.gameObject.GetComponent<AudioSource>());

            if (!_levelsPanel)
            {
                _levelsPanel = GameObject.Find("PanelLevels");
                _mainPanel = GameObject.Find("PanelMainMenu");
                _creditsPanel = GameObject.Find("PanelCredits");
                _achsPanel = GameObject.Find("PanelAchs");
                _introPanel = GameObject.Find("PanelIntro");
            }

            if (_levelsPanel)
            {
                _levelsPanel.SetActive(false);
                _creditsPanel.SetActive(false);
                _achsPanel.SetActive(false);
                _introPanel.SetActive(false);
            }

            //menu from game
            if (_levelsPanel && !UIManager.isFirstLoad)
            {
                _mainPanel.SetActive(false);
                _levelsPanel.SetActive(true);
            }

            _victoryPanel = GameObject.Find("PanelVictory");
            if (_victoryPanel)
            {
                MessageDispatcher.AddListener(GameEvent.LEVEL_COMPLETE, _victoryPanel.GetComponent<PanelVictory>().Activate);
                _victoryPanel.SetActive(false);
            }
            UIManager.isFirstLoad = false;
        }
        void Start()
        {
            Controller.instance.model.loadProgress();

            if (GameObject.Find("PanelMainMenu") != null || GameObject.Find("PanelLevels") !=null)
                SoundManager.getInstance().setLocation(SoundManager.MUSIC_MENU);
            else
                SoundManager.getInstance().setLocation(SoundManager.MUSIC_GAME);

            MessageDispatcher.AddListener(GameEvent.RESTART, RestartGame);
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
            MessageDispatcher.RemoveListener(GameEvent.RESTART, RestartGame);
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
            Controller.instance.model.saveProgress();

            MessageDispatcher.RemoveListener(GameEvent.RESTART, RestartGame);
            MessageDispatcher.SendMessage(GameEvent.QUIT); //reloading whole scene without destroying units?
            //Debug.Log("StartGame");
            SoundManager.getInstance().stopMusicTrack();
            SceneManager.LoadScene("GameScene");
        }

        public void EndGame()
        {
            DOTween.Clear();//PanelVictory tweens

            MessageDispatcher.RemoveListener(GameEvent.RESTART, RestartGame);
            MessageDispatcher.SendMessage(GameEvent.QUIT);

            SoundManager.getInstance().stopMusicTrack();
            SceneManager.LoadScene("MainScene");
        }

    }
}

