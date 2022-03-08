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

        //turned off images
        [SerializeField]
        private Sprite _image;
        [SerializeField]
        private Sprite _pressedImage;

        private bool _isActive;

        void Start()
        {
            _isActive = SoundManager.getInstance().hasMusic;
            SoundManager.getInstance().CurrentButton = this;

            if (!_isActive)
                SwitchState();
        }

        public void Update()
        {

        }

        public void SwitchState()
        {
            _isActive = SoundManager.getInstance().hasMusic;

            Sprite image = _image;
            Sprite pressedImage = _pressedImage;
            var ss = new SpriteState();

            Button button = this.GetComponent<Button>();

            _image = button.image.sprite;
            button.image.sprite = image;

            _pressedImage = button.spriteState.pressedSprite;
            ss.pressedSprite = pressedImage;
            button.spriteState = ss;
        }

        public void ClickHandler()
        {
            _isActive = !_isActive;
            SoundManager.getInstance().muteOnOff();
        }

        public bool isActive
        {
            get
            {
                return _isActive;
            }
        }
    }
}
