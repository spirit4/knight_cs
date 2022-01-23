using Assets.Scripts.Utils.Display;
using BayatGames.SaveGameFree;
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
        private bool _isMusic = true; //TODO has
        //private bool _isMusic = false; //TODO has

        private MusicButton _currentButton;
        private string _currentLocation = ""; //menu or game
        private AudioSource _audioSource;

        private Dictionary<string, float> _musicPositions = new Dictionary<string, float>();


        public SoundManager()
        {
            
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


        public void setLocation(string type)
        {
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
            if (!_musicPositions.ContainsKey(_currentLocation))
                _musicPositions[_currentLocation] = 0;

            _audioSource.clip = tracks[_currentLocation];
            _audioSource.loop = true;
            _audioSource.time = _musicPositions[_currentLocation];
            _audioSource.Play();
        }

        public void stopMusicTrack()
        {
            if (_currentLocation == null)
                return;

            _musicPositions[_currentLocation] = _audioSource.time;
            _audioSource.Stop();
        }

        public void muteOnOff()
        {
            this.isMusic = !_isMusic;

            if (_isMusic)
                playMusicTrack();
            else
                stopMusicTrack();

            if (_currentButton)
                _currentButton.SwitchState();
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
                SaveGame.Save<bool>("isMusic", _isMusic);

                if (_currentButton && _currentButton.isActive != _isMusic)
                    _currentButton.SwitchState();
            }
        }

        public MusicButton CurrentButton
        {
            set
            {
                _currentButton = value;
            }
        }
    }
}
