using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractablePlaceView : OverlayObjectView<InteractablePlaceOverlay>
{
    private Tween _tween;

    public void UpdateSliderView(float time)
    {
        OverlayObject.WaitSlider.value = 0;
        OverlayObject.WaitSlider.maxValue = 1;
        _tween = OverlayObject.WaitSlider.DOValue(OverlayObject.WaitSlider.maxValue, time).OnComplete(Close);
    }

    private void Close()
    {
        _tween.Pause();
        CloseOverlay();
    }
}
