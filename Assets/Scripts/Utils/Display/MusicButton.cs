using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Utils.Display
{
    public class MusicButton : MonoBehaviour
    {

        //off images
        public Sprite image;
        public Sprite pressedImage;

        private bool _isActive;

        void Start()
        {
            //Debug.Log("[MusicButton] Start " + SoundManager.getInstance().isMusic);
            _isActive = SoundManager.getInstance().isMusic;
            SoundManager.getInstance().CurrentButton = this;

            if (!_isActive)
                SetState();
        }

        public void Update()
        {

        }

        public void SetState()
        {
            //Debug.Log("[MusicButton] SetState " + SoundManager.getInstance().isMusic);
            _isActive = SoundManager.getInstance().isMusic;

            Sprite image = this.image;
            Sprite pressedImage = this.pressedImage;
            var ss = new SpriteState();

            Button button = this.GetComponent<Button>();

            this.image = button.image.sprite;
            button.image.sprite = image;

            this.pressedImage = button.spriteState.pressedSprite;
            ss.pressedSprite = pressedImage;
            button.spriteState = ss;
        }

        public void ClickHandler()
        {
            _isActive = !_isActive;
            SoundManager.getInstance().muteOnOff();
        }
    }
}
