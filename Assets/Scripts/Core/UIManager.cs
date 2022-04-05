using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Utils;
using com.ootii.Messages;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
    /** <summary>Canvas script</summary> */
    public class UIManager : MonoBehaviour
    {
        private List<GameObject> _windows;

        private void Awake()
        {
            MessageDispatcher.ClearListeners();//after scene reload

            if (ImagesRes.Prefabs.Count == 0) // loading resources to static
                ImagesRes.Init();

            this.gameObject.AddComponent<AudioSource>();
            SoundManager.Instance.Init(this.gameObject.GetComponent<AudioSource>());

            Resize();
        }


        void Start()
        {
            _windows = new List<GameObject>(this.transform.childCount);
            for (int i = 0; i < this.transform.childCount; i++)
            {
                _windows.Add(this.transform.GetChild(i).gameObject);
            }
            ShowNewWindow(_windows[0]); //TODO levels screen from game

            Saver.LoadProgress();

            if (SceneManager.GetActiveScene().name != "GameScene")
                SoundManager.Instance.SetLocation(SoundManager.MUSIC_MENU);
            else
                SoundManager.Instance.SetLocation(SoundManager.MUSIC_GAME);

            MessageDispatcher.AddListener(GameEvent.RESTART, RestartGame);
        }

        //logic in Editor's buttons
        public void ShowNewWindow(GameObject window)
        {
            foreach (var item in _windows)
            {
                item.SetActive(false);
            }
            window.SetActive(true);
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


        private void Resize()
        {
            Camera.main.orthographicSize = Screen.height * Config.CAMERA_SIZE * 640 / Screen.width / 1138;
            if (Camera.main.orthographicSize < Config.CAMERA_SIZE_MIN)
                Camera.main.orthographicSize = Config.CAMERA_SIZE_MIN;

            //Debug.Log($"w: {Screen.width} h: {Screen.height} size: {Camera.main.orthographicSize}" );
        }
    }
}

