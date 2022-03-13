using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.UnityEditor;
using Assets.Scripts.Utils;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Core
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

            if (ImagesRes.Prefabs.Count == 0) // loading resources to static
                ImagesRes.Init();

            Debug.Log("UIManager Awake");
            this.gameObject.AddComponent<AudioSource>();
            SoundManager.Instance.Init(this.gameObject.GetComponent<AudioSource>());

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
            Debug.Log("UIManager Start");
            Saver.LoadProgress();

            if (GameObject.Find("PanelMainMenu") != null || GameObject.Find("PanelLevels") !=null)
                SoundManager.Instance.SetLocation(SoundManager.MUSIC_MENU);
            else
                SoundManager.Instance.SetLocation(SoundManager.MUSIC_GAME);

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
            Progress.CurrentLevel++;
            StartGame();
        }

        public void GoToLevel(Text level)
        {
            Progress.CurrentLevel = int.Parse(level.text) - 1;

            StartGame();
        }

        public void StartGame()
        {
            Saver.SaveProgress();

            MessageDispatcher.RemoveListener(GameEvent.RESTART, RestartGame);
            MessageDispatcher.SendMessage(GameEvent.QUIT); //reloading whole scene without destroying units?
            //Debug.Log("StartGame");
            SoundManager.Instance.StopMusicTrack();
            SceneManager.LoadScene("GameScene");
        }

        public void EndGame()
        {
            DOTween.Clear();//PanelVictory tweens

            MessageDispatcher.RemoveListener(GameEvent.RESTART, RestartGame);
            MessageDispatcher.SendMessage(GameEvent.QUIT);

            SoundManager.Instance.StopMusicTrack();
            SceneManager.LoadScene("MainScene");
        }

    }
}

