using InteractiveObjects.Notifications;
using System;
using UnityEngine;

public class OverlayObjectView<T> : MonoBehaviour where T : OverlayObject
{
    [SerializeField] private OverlayObjectCreator _overlayObjectCreator;

    public bool IsOpend { get; private set; }

    public T OverlayObject { get; private set; }

    public virtual void OpenOverlay()
    {
        var overlayAnimator = _overlayObjectCreator.Activate();
        IsOpend = true;

        if (overlayAnimator.Object is T productionOverlay)
            OverlayObject = productionOverlay;
        else
            throw new ArgumentException(nameof(overlayAnimator));
    }

    public virtual void CloseOverlay()
    {
        IsOpend = false;
        _overlayObjectCreator.Deactivate();
    }
}
