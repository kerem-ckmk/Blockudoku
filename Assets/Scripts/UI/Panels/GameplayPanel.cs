using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameplayPanel : UIPanel
{
    [Header("References - UI")]
    public Button settingsButton;
    public SettingsPopup settingsPopup;
    public TextMeshProUGUI highScoreTMP;

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

        highScoreTMP.text = "High Score: " + GameManager.PlayerHighScore.KiloFormatNumber();
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
