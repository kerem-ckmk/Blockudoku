using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<TileController> tilePrefabs;
    public List<Transform> tileTransforms;
    public Camera cameraUI;
    public bool IsInitiailized { get; private set; }
    public List<TileController> ActiveTiles { get; private set; }
    public GridController GridController { get; private set; }

    public event Action OnCheckGrid;
    public event Action OnFinish;
    public void Initialize(GridController gridController)
    {
        GridController = gridController;
        ActiveTiles = new List<TileController>();
        IsInitiailized = true;
    }

    public void Prepare()
    {
        ActiveTiles.Clear();
        SpawnTiles();
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < 3; i++)
        {
            var tile = CreateTileController(i);
            tile.OnDestroyTile += Tile_OnDestroyTile;
            ActiveTiles.Add(tile);
        }
    }

    private void Tile_OnDestroyTile(TileController tileController)
    {
        ActiveTiles.Remove(tileController);
        Destroy(tileController.gameObject);

        if (ActiveTiles.Count == 0)
        {
            ActiveTiles.Clear();
            SpawnTiles();
        }

        OnCheckGrid?.Invoke();

        if (IsGameOver())
        {
            OnFinish?.Invoke();
        }

    

    }

    public void Unload()
    {
        foreach (var tile in ActiveTiles)
            Destroy(tile.gameObject);

        ActiveTiles.Clear();
    }

    private TileController CreateTileController(int transformIndex)
    {
        int random = UnityEngine.Random.Range(0, tilePrefabs.Count);
        var tileControllerObject = Instantiate(tilePrefabs[random], tileTransforms[transformIndex]);
        tileControllerObject.Initialize(GridController, cameraUI);
        return tileControllerObject;
    }

    public bool IsGameOver()
    {
        foreach (var tile in ActiveTiles)
        {
            if (tile.CanPlaceInGrid())
            {
                return false;
            }
        }
        return true;
    }

}

