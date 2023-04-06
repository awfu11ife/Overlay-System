using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractablePlaceOverlay : OverlayObject
{
    [field: SerializeField] public Slider WaitSlider { get; private set; }
}
