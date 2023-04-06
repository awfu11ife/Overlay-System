using DG.Tweening;

public class CustomerServeView : OverlayObjectView<CustomerServeOverlay>
{
    private Tween _tween;

    public void UpdateImageView(float time)
    {
        OverlayObject.MoneyImage.fillAmount = 0;
        _tween = OverlayObject.MoneyImage.DOFillAmount(1f, time).OnComplete(Close);
    }

    private void Close()
    {
        _tween.Pause();
        CloseOverlay();
    }
}
