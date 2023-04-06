using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects.Notifications
{
    public class OverlayViewContainer : MonoBehaviour
    {
        private const int LEFT_PLANE_INDEX = 0;
        private const int Right_PLANE_INDEX = 1;
        private const int DOWN_PLANE_INDEX = 2;
        private const int UPPER_PLANE_INDEX = 3;

        [SerializeField] Transform _playerTransform;
        [SerializeField] Camera _camera;

        private Dictionary<OverlayObjectCreator, OverlayAnimator> _dictionary = new Dictionary<OverlayObjectCreator, OverlayAnimator>();

        public static OverlayViewContainer Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void LateUpdate()
        {
            // Left, Right, Down, Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (var pointerDictionaty in _dictionary)
            {
                OverlayObjectCreator notificationPointer = pointerDictionaty.Key;
                OverlayAnimator pointerIcon = pointerDictionaty.Value;

                Vector3 toPosition = notificationPointer.transform.position - _playerTransform.position;
                Ray ray = new Ray(_playerTransform.position, toPosition);


                float rayMinDistance = Mathf.Infinity;
                int index = 0;

                for (int plane = 0; plane < 4; plane++)
                {
                    if (planes[plane].Raycast(ray, out float distance))
                    {
                        if (distance < rayMinDistance)
                        {
                            rayMinDistance = distance;
                            index = plane;
                        }
                    }
                }

                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toPosition.magnitude);
                Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                Vector3 position = _camera.WorldToScreenPoint(worldPosition);

                if (pointerIcon.AlwaysOnScreen == false)
                {
                    if (pointerIcon.HideOnOutOfScreen == true)
                    {
                        if (toPosition.magnitude > rayMinDistance)
                            pointerIcon.Hide();
                        else
                            pointerIcon.Show();

                        pointerIcon.SetPosition(position);
                    }
                    else
                    {
                        if (toPosition.magnitude > rayMinDistance)
                            pointerIcon.Show();
                        else
                            pointerIcon.Hide();

                        pointerIcon.SetPosition(CalculatePointerOffset(index, position, pointerIcon));
                    }
                }
                else
                {
                    pointerIcon.Show();

                    if (toPosition.magnitude <= rayMinDistance)
                        pointerIcon.SetPosition(position);
                    else
                        pointerIcon.SetPosition(CalculatePointerOffset(index, position, pointerIcon));
                }
            }
        }

        public void AddToList(OverlayObjectCreator notificationPointer, OverlayAnimator pointerIcon)
        {
            if (!_dictionary.ContainsKey(notificationPointer))
            {
                _dictionary.Add(notificationPointer, pointerIcon);
            }
        }

        public void RemoveFromList(OverlayObjectCreator notificationPointer)
        {
            if (_dictionary.TryGetValue(notificationPointer, out OverlayAnimator pointerIcon))
            {
                pointerIcon.Destoy();
                _dictionary.Remove(notificationPointer);
            }
        }

        private Vector3 CalculatePointerOffset(int planeIndex, Vector3 position, OverlayAnimator pointerIcon)
        {
            Vector3 positionWithOffset;

            switch (planeIndex)
            {
                case LEFT_PLANE_INDEX:
                    positionWithOffset = new Vector3(position.x + pointerIcon.RectTransform.rect.width / 2, position.y, position.z);
                    break;

                case Right_PLANE_INDEX:
                    positionWithOffset = new Vector3(position.x - pointerIcon.RectTransform.rect.width / 2, position.y, position.z);
                    break;

                case DOWN_PLANE_INDEX:
                    positionWithOffset = new Vector3(position.x, position.y + pointerIcon.RectTransform.rect.height / 2, position.z);
                    break;

                case UPPER_PLANE_INDEX:
                    positionWithOffset = new Vector3(position.x, position.y - pointerIcon.RectTransform.rect.height / 2, position.z);
                    break;

                default:
                    positionWithOffset = position;
                    break;
            }

            return positionWithOffset;
        }
    }
}