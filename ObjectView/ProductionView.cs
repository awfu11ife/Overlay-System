using UnityEngine;

public class ProductionView : OverlayObjectView<ProductionOverlay>
{
    [field: SerializeField] public Sprite FoodSprite { get; private set; }

    private float _maxValue;

    public void SetWorkView()
    {
        OverlayObject.SpeedUpButton.interactable = true;
        OverlayObject.MaxText.gameObject.SetActive(false);
        OverlayObject.SpeedUpText.gameObject.SetActive(true);
    }

    public void SetMaxStorageVeiw()
    {
        OverlayObject.SpeedUpButton.interactable = false;
        OverlayObject.SpeedUpText.gameObject.SetActive(false);
        OverlayObject.MaxText.gameObject.SetActive(true);

    }

    public void SetWorkSliderValue(double value)
    {
        OverlayObject.ProgressImage.sprite = FoodSprite;
        OverlayObject.ProgressImage.fillAmount = 0f;
        _maxValue = (float)value;
    }

    public void UpdateWorkSlider(float targetValue)
    {
        if (OverlayObject != null)
            OverlayObject.ProgressImage.fillAmount = targetValue / _maxValue;
    }
}