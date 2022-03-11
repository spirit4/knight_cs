using Assets.Scripts.Utils.Display;
using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class SoundManager
    {
        public static Dictionary<string, AudioClip> tracks = new Dictionary<string, AudioClip>();

        //music
        public const string MUSIC_MENU = "Menu";
        public const string MUSIC_GAME = "Game";

        private static SoundManager instance;

        private bool _hasMusic = true;
        //private bool _isMusic = false;

        private MusicButton _currentButton;
        private string _currentLocation = ""; //menu or game
        private AudioSource _audioSource;

        private Dictionary<string, float> _musicPositions = new Dictionary<string, float>();


        public SoundManager()
        {
            
        }

        public static SoundManager GetInstance()
        {
            if (instance == null)
                instance = new SoundManager();

            return instance;
        }

        public void Init(AudioSource audioSource)
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


        public void SetLocation(string type)
        {
            if (_hasMusic)
            {
                if (type != _currentLocation)
                {
                    StopMusicTrack();

                    _musicPositions[SoundManager.MUSIC_GAME] = 0;
                    _musicPositions[SoundManager.MUSIC_MENU] = 0;
                }
            }

            _currentLocation = type;

            if (_hasMusic)
                PlayMusicTrack();
        }

        private void PlayMusicTrack()
        {
            if (!_musicPositions.ContainsKey(_currentLocation))
                _musicPositions[_currentLocation] = 0;

            _audioSource.clip = tracks[_currentLocation];
            _audioSource.loop = true;
            _audioSource.time = _musicPositions[_currentLocation];
            _audioSource.Play();
        }

        public void StopMusicTrack()
        {
            if (_currentLocation == null)
                return;

            _musicPositions[_currentLocation] = _audioSource.time;
            _audioSource.Stop();
        }

        public void MuteOnOff()
        {
            HasMusic = !_hasMusic;

            if (_hasMusic)
                PlayMusicTrack();
            else
                StopMusicTrack();

            if (_currentButton)
                _currentButton.SwitchState();
        }

        public bool HasMusic
        {
            get
            {
                return _hasMusic;
            }
            set
            {
                _hasMusic = value;
                SaveGame.Save<bool>("isMusic", _hasMusic);

                if (_currentButton && _currentButton.IsActive != _hasMusic)
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
