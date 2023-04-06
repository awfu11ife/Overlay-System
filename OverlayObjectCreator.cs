using UnityEngine;

namespace InteractiveObjects.Notifications
{
    public class OverlayObjectCreator : MonoBehaviour
    {
        [SerializeField] private OverlayAnimator _animatorPrefab;

        public OverlayAnimator Activate()
        {
            var newPointerIcon = Instantiate(_animatorPrefab, OverlayViewContainer.Instance.transform);
            OverlayViewContainer.Instance.AddToList(this, newPointerIcon);
            return newPointerIcon;
        }

        public void Deactivate()
        {
            OverlayViewContainer.Instance.RemoveFromList(this);
        }
    }
}