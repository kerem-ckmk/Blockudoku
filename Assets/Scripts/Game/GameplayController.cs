using System;
using UnityEngine;
using Lofelt.NiceVibrations;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public CameraController gameCamera;
    public LevelManager levelManager; 
    public GridController gridController;

    public bool IsInitialized { get; private set; }
    public bool IsActive { get; private set; }

    public int TotalCurrencyReward
    {
        get { return 100; }
    }

    public Action<bool> OnGameplayFinished;

    public void Initialize()
    {
        levelManager.Initialize();
        gameCamera.Initialize();
        gridController.Initialize();
        IsInitialized = true;
    }

    public void PrepareGameplay(int linearLevelIndex)
    {
        levelManager.CreateLevel(linearLevelIndex);
        gridController.Prepare();
    }

    public void UnloadGameplay()
    {
        levelManager.UnloadLevel();
    }

    public void StartGameplay()
    {
        IsActive = true;
    }

    private void FinishGameplay(bool success)
    {
        IsActive = false;

        OnGameplayFinished?.Invoke(success);
    }

    private void Update()
    {
        if (!IsActive)
            return;
    }
}
