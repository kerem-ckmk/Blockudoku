using UnityEngine;
using UnityEngine.UI;
public class GameplayPanel : UIPanel
{
    [Header("References - UI")]
    public Button settingsButton;
    public SettingsPopup settingsPopup;

    private void Awake()
    {
        settingsButton.onClick.AddListener(SettingsButtonClicked);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        settingsPopup.Initialize(GameManager);
        settingsPopup.HidePanel();
    }

    protected override void OnShowPanel()
    {
        base.OnShowPanel();
    }

    protected override void OnHidePanel()
    {
        base.OnHidePanel();
    }

    private void SettingsButtonClicked()
    {
        settingsPopup.ShowPanel();
    }
}
