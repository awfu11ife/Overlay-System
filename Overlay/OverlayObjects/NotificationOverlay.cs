using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NotificationOverlay : OverlayObject
{
    [SerializeField] private Sprite _sprite;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprite;
    }
}
