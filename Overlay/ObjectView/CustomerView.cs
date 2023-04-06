using DG.Tweening;

public class CustomerView : OverlayObjectView<CustomerOverlay>
{
    public void SetEatingSlider(float time)
    {
        OverlayObject.EatSlider.value = 0;
        OverlayObject.EatSlider.DOValue(OverlayObject.EatSlider.maxValue, time);
    }
}
