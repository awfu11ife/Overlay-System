using System;
using System.Collections;
using UnityEngine;

namespace InteractiveObjects.Notifications
{
    public class OverlayAnimator : MonoBehaviour
    {
        [field: SerializeField] public OverlayObject Object { get; private set; }
        [SerializeField] private float _showSpeed = 4f;
        [SerializeField] private float _showTime = 1f;

        private bool _isShown = true;

        [field: SerializeField] public bool HideOnOutOfScreen { get; private set; }
        [field: SerializeField] public bool AlwaysOnScreen { get; private set; } = false;

        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            gameObject.SetActive(false);
            _isShown = false;
            RectTransform = GetComponent<RectTransform>();
        }

        public void SetPosition(Vector3 position)
        {
            RectTransform.position = new Vector3(position.x, position.y, 0);
        }

        public void Show()
        {
            if (_isShown)
                return;

            _isShown = true;
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(ShowProcess());
        }

        public void Hide()
        {
            if (!_isShown)
                return;

            _isShown = false;
            StopAllCoroutines();
            StartCoroutine(HideProcess(Deactivate));
        }

        public void Destoy()
        {
            if (gameObject.activeSelf == true)
                StartCoroutine(HideProcess(DestroyAfterHide));
            else
                Destroy(gameObject);
        }

        private void DestroyAfterHide()
        {
            Destroy(gameObject);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator ShowProcess()
        {
            transform.localScale = Vector3.zero;

            for (float time = 0; time < _showTime; time += Time.deltaTime * _showSpeed)
            {
                transform.localScale = Vector3.one * time;
                yield return null;
            }

            transform.localScale = Vector3.one;
        }

        private IEnumerator HideProcess(Action hideProcessEnd)
        {
            for (float time = 0; time < _showTime; time += Time.deltaTime * _showSpeed)
            {
                transform.localScale = Vector3.one * (_showTime - time);
                yield return null;
            }

            hideProcessEnd();
        }
    }
}