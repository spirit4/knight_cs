using Assets.Scripts.Events;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Unity
{
    public class Swayer : MonoBehaviour
    {
        private float _direction = 10.0f;
        public int direction3D = 1; //can be changed in editor
        public Swayer()
        {

        }

        void Start()
        {
            ChangePivot(new Vector2(0.5f, 1f));
            this.transform.Rotate(_direction, 25 * direction3D, 0);
            Sway();

        }
        private void Sway()
        {
            _direction = -_direction;
            this.transform.DORotate(new Vector3(_direction, 25 * direction3D, 0), 2.0f).SetEase(Ease.InOutQuad).OnComplete(Sway);
        }

        private void ChangePivot(Vector2 pivot)
        {
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            Vector2 deltaPivot = rectTransform.pivot - pivot;

            float deltaX = deltaPivot.x * rectTransform.sizeDelta.x * rectTransform.localScale.x;
            float deltaY = deltaPivot.y * rectTransform.sizeDelta.y * rectTransform.localScale.y;

            float rot = rectTransform.rotation.eulerAngles.z * Mathf.PI / 180;
            Vector3 deltaPosition = new Vector3(Mathf.Cos(rot) * deltaX - Mathf.Sin(rot) * deltaY, Mathf.Sin(rot) * deltaX + Mathf.Cos(rot) * deltaY);

            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }
    }
}