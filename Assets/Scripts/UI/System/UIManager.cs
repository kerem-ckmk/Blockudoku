using Lofelt.NiceVibrations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;

    [Header("References - Panels")]
    public LoadingPanel loadingPanel;
    public GameplayPanel gameplayPanel;
    public FinishSuccessPanel finishSuccessPanel;
    public FinishFailPanel finishFailPanel;

    [Header("References - Common HUD")]
    public TextMeshProUGUI levelText;
    public CurrencyHudWidget currencyHudWidget;

    private List<UIPanel> allPanels = new List<UIPanel>();

    private void Awake()
    {
        allPanels.Add(loadingPanel);
        allPanels.Add(gameplayPanel);
        allPanels.Add(finishSuccessPanel);
        allPanels.Add(finishFailPanel);

        HideAllPanels(true);

        gameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        gameManager.OnChangeScoreBoard += GameManagerOnChangeScoreBoard;
        loadingPanel.OnLoadingFinished += LoadingPanelOnLoadingFinished;

        currencyHudWidget.OnCurrencyParticleMovementFinished += CurrencyHudWidget_OnCurrencyParticleMovementFinished;
    }

    private void LoadingPanelOnLoadingFinished(bool extended)
    {
        if (!extended/* && !MarketingSDK.RemoteConfig.IsRemoteFetched*/)
        {
            loadingPanel.ExtendLoading();
        }
        else if (extended/* || MarketingSDK.RemoteConfig.IsRemoteFetched*/)
        {
            gameManager.InitializeAfterLoading();

            foreach (var panel in allPanels)
            {
                panel.Initialize(gameManager);
            }

            gameManager.PrepareGameplay();
        }
    }

    private void GameManagerOnGameStateChanged(GameState oldGameState, GameState newGameState)
    {
        if (newGameState == GameState.Loading)
        {
            ShowPanel(loadingPanel);
        }
        else if (newGameState == GameState.Gameplay)
        {
            currencyHudWidget.gameObject.SetActive(true);
            ShowPanel(gameplayPanel);
        }
        else if (newGameState == GameState.FinishSuccess)
        {
            currencyHudWidget.gameObject.SetActive(false);
            ShowPanel(finishSuccessPanel);
        }
        else if (newGameState == GameState.FinishFail)
        {
            ShowPanel(finishFailPanel);
        }
        else
        {
            HideAllPanels();
        }
    }

    private void HideAllPanels(bool forceHide = false)
    {
        foreach (var panel in allPanels)
        {
            if (panel.GetType() != typeof(GameplayPanel))
            {
                if (panel.IsShown || forceHide)
                    panel.HidePanel();
            }
        }
    }

    private void ShowPanel(UIPanel panel)
    {
        HideAllPanels();
        currencyHudWidget.SetCurrencyAmount(0);
        panel.ShowPanel();
    }

    private void GameManagerOnChangeScoreBoard(int score)
    {
        currencyHudWidget.SetCurrencyAmount(score);
    }

    private void CurrencyHudWidget_OnCurrencyParticleMovementFinished()
    {
        GameManager.DoHaptic(HapticPatterns.PresetType.SoftImpact);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TimeSpan timeSpan = TimeSpan.FromDays(4) + TimeSpan.FromHours(12) + TimeSpan.FromMinutes(43);
            string timeSpanStr = timeSpan.ToString(@"d'g 'hh'sa 'mm'dk'");

            Debug.Log(timeSpanStr);
        }
    }
}
