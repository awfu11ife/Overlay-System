using UnityEngine;
using DG.Tweening;

public class SpecialCustomerView : OverlayObjectView<SpecialCustomerOverlay>
{
    private Tween _imageTween;

    public void SetWaitImage(Sprite foodSprite, float waitTime)
    {
        OverlayObject.FoodImage.sprite = foodSprite;
        OverlayObject.FoodImage.fillAmount = 1;
        _imageTween = OverlayObject.FoodImage.DOFillAmount(0, waitTime).SetEase(Ease.Linear);
    }

    public override void CloseOverlay()
    {
        _imageTween.Pause();
        base.CloseOverlay();
    }
}
