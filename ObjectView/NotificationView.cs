using UnityEngine;
using DG.Tweening;

public class NotificationView : OverlayObjectView<NotificationOverlay>
{
    [SerializeField] private float _targetScale = 1.2f;
    [SerializeField] private float _loopDuration = 1f;

    private Tweener _tweener;

    public override void OpenOverlay()
    {
        base.OpenOverlay();
        _tweener = OverlayObject.gameObject.transform.DOScale(_targetScale, _loopDuration).SetLoops(-1, LoopType.Yoyo);
    }

    public override void CloseOverlay()
    {
        _tweener.Pause();
        base.CloseOverlay();
    }
}
