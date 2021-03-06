using Assets.Scripts.Core;
using UnityEngine;
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
            _isActive = SoundManager.Instance.HasMusic;
            SoundManager.Instance.CurrentButton = this;

            if (!_isActive)
                SwitchState();
        }

        public void SwitchState()
        {
            _isActive = SoundManager.Instance.HasMusic;

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
            SoundManager.Instance.MuteOnOff();
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }
    }
}
