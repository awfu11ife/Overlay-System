using InteractiveObjects.Selection;
using UnityEngine;
using SaveSystem;

public class HintView : OverlayObjectView<HintOverlay>, IDataPersistence
{
    [SerializeField] private SelectableParentObject _objectToHint;

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
            _objectToHint.OnSelectAll += CloseOverlay;
        }
    }

    private void OnDisable()
    {
        _objectToHint.OnSelectAll -= CloseOverlay;
    }

    public override void OpenOverlay()
    {
        base.OpenOverlay();
    }

    public override void CloseOverlay()
    {
        _isViewed = true;
        Save();
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
