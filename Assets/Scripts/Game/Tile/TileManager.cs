using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<TileController> tilePrefabs;
    public List<Transform> tileTransforms;
    public bool IsInitiailized { get; private set; }
    public List<TileController> ActiveTiles { get; private set; }
    public GridController GridController { get; private set; }
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
            ActiveTiles.Add(tile);  
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
        int random = Random.Range(0, tilePrefabs.Count);
        var tileControllerObject = Instantiate(tilePrefabs[random], tileTransforms[transformIndex]);
        tileControllerObject.Initialize(GridController);
        return tileControllerObject;
    }

}

