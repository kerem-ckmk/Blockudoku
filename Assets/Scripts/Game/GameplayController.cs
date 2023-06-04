using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public CameraController gameCamera;
    public LevelManager levelManager; 
    public GridController gridController;
    public TileManager tileManager;

    public bool IsInitialized { get; private set; }
    public bool IsActive { get; private set; }

    public int TotalCurrencyScore
    {
        get { return _currencyScore; }
    }

    private int _currencyScore;

    public Action<bool> OnGameplayFinished;
    public Action<int> OnChangeHighScore;

    public void Initialize()
    {
        levelManager.Initialize();
        gameCamera.Initialize();
        gridController.Initialize();
        tileManager.Initialize(gridController);
        tileManager.OnCheckGrid += TileManager_OnCheckgrid;
        tileManager.OnFinish += TileManager_OnFinish;
        tileManager.OnChangeScore += TileManager_OnChangeScore;
        IsInitialized = true;
    }

    private void TileManager_OnChangeScore(int score)
    {
        _currencyScore += GameConfigs.Instance.BlockValue * score;
        OnChangeHighScore?.Invoke(TotalCurrencyScore);
    }

    private void TileManager_OnFinish()
    {
        FinishGameplay(true);
    }

    public void PrepareGameplay(int linearLevelIndex)
    {
        levelManager.CreateLevel(linearLevelIndex);
        gridController.Prepare();
        tileManager.Prepare();
    }

    public void UnloadGameplay()
    {
        _currencyScore = 0;
        levelManager.UnloadLevel();
        gridController.Unload();
        tileManager.Unload();
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


    private void TileManager_OnCheckgrid()
    {
        gridController.CheckGrid();
    }

    private void Update()
    {
        if (!IsActive)
            return;
        
    }
}
