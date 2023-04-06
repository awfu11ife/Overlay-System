using UnityEngine;
using SaveSystem;
using DG.Tweening;

public class HintArrowView : OverlayObjectView<HintOverlay>, IDataPersistence
{
    [SerializeField] private InteractablePlace _objectToHint;
    [SerializeField] private float _targetPoseY = 20f;
    [SerializeField] private float _loopTime = .5f;

    private Tween _tweener;
    private bool _isViewed = false;

    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        if (_isViewed == false)
        {
            OpenOverlay();
            _objectToHint.OnEnter += CloseOverlay;
        }
    }

    private void OnDisable()
    {
        _objectToHint.OnEnter -= CloseOverlay;
    }

    public override void OpenOverlay()
    {
        base.OpenOverlay();
        _tweener = OverlayObject.gameObject.transform.DOLocalMoveY(_targetPoseY, _loopTime).SetLoops(-1, LoopType.Yoyo);
    }

    public override void CloseOverlay()
    {
        _isViewed = true;
        Save();
        _tweener.Pause();
        base.CloseOverlay();
    }

    public void Save()
    {
        SaverLoader.SaveData(gameObject.name, GetSaveSnapshot());
    }

    public void Load()
    {
        var data = SaverLoader.LoadData<SaveSystem.HintData>(gameObject.name);
        _isViewed = data.IsViewed;
    }

    public DataToSave GetSaveSnapshot()
    {
        var data = new SaveSystem.HintData()
        {
            IsViewed = _isViewed
        };

        return data;
    }
}
