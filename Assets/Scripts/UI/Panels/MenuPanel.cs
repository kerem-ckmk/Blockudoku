using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : UIPanel
{
    [Header("References - UI")]
    public Button startButton;
    public Button settingsButton;
    public SettingsPopup settingsPopup;



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
