using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductionOverlay : OverlayObject
{
    [field: SerializeField] public Image ProgressImage { get; private set; }
    [field: SerializeField] public Button SpeedUpButton { get; private set; }
    [field: SerializeField] public TMP_Text MaxText { get; private set; }
    [field: SerializeField] public TMP_Text SpeedUpText { get; private set; }
}
