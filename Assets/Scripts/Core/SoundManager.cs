using Assets.Scripts.Utils.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class SoundManager
    {
        public static Dictionary<string, AudioClip> tracks = new Dictionary<string, AudioClip>();

        //music
        public const string MUSIC_MENU = "menu";
        public const string MUSIC_GAME = "game";

        private static SoundManager instance;

        //    //false = true;
        //private bool _isMusic = true; //TODO has
        private bool _isMusic = false; //TODO has

        private MusicButton _currentButton;
        private string _currentLocation; //menu or game
        private AudioSource _audioSource;

        private Dictionary<string, float> _musicPositions = new Dictionary<string, float>();


        public SoundManager()
        {
            // SoundManager.instance = this;
        }

        public static SoundManager getInstance()
        {
            if (instance == null)
                instance = new SoundManager();

            return instance;
        }

        public void init(AudioSource audioSource)
        {
            _audioSource = audioSource;

            if (SoundManager.tracks.Count > 0)
                return;

            AudioClip[] aClips = Resources.LoadAll<AudioClip>("Music");

            foreach (var track in aClips)
            {
                tracks.Add(track.name, track);
            }
        }


        //TODO save position on scene reload
        public void setLocation(string type)//, bool isRestart)
        {
           // Debug.Log("[SoundManager] setLocation " + _currentLocation + "   " + type);
            if (_isMusic)
            {
                if (type != _currentLocation)
                {
                    stopMusicTrack();

                    _musicPositions[SoundManager.MUSIC_GAME] = 0;
                    _musicPositions[SoundManager.MUSIC_MENU] = 0;
                }
            }
     
            _currentLocation = type;

            if (_isMusic)
                playMusicTrack();
        }

        private void playMusicTrack()
        {
            //Debug.Log("playMusicTrack " + _musicPositions.ContainsKey(_currentLocation));

            if (!_musicPositions.ContainsKey(_currentLocation))
                _musicPositions[_currentLocation] = 0;

            _audioSource.clip = tracks[_currentLocation];
            _audioSource.loop = true;
            _audioSource.time = _musicPositions[_currentLocation];
            _audioSource.Play();

            //Debug.Log("playMusicTrack " + _audioSource.time);
        }

        public void stopMusicTrack()
        {
            //Debug.Log("stopMusicTrack " + _currentLocation);
            if (_currentLocation == null)
                return;

            //Debug.Log("stopMusicTrack" + _audioSource.time);
            _musicPositions[_currentLocation] = _audioSource.time;
            _audioSource.Stop();

            
        }

        public void muteOnOff()
        {
            //Debug.Log("[SoundManager] muteOnOff " + SoundManager.getInstance().isMusic);
            this.isMusic = !_isMusic;

            if (_currentButton)
                _currentButton.SetState();
        }

        public bool isMusic
        {
            get
            {
                return _isMusic;
            }
            set
            {
                _isMusic = value;

                if (_isMusic)
                {
                    playMusicTrack();
                }
                else
                {
                    stopMusicTrack();
                }
            }
        }

        //public set savingState(state: boolean)
        //    {
        //    this._isMusic = state;
        //    this._isSFX = state;
        //}

        public MusicButton CurrentButton
        {
            set
            {
                _currentButton = value;
            }
        }
    }
}
