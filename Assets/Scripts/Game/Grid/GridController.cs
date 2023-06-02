using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    [Header("References")]
    public int gridSize;
    public float cellSize;
    public CellController cellControllerPrefab;
    public GameObject linePrefab;
    public Transform lineTransform;
    public Transform gridParent;
    public bool IsInitialized { get; private set; }
    public CellController[,] CellGrid { get; private set; }

    private List<CellController> _cells;
    private List<GameObject> _lines;

    public void Initialize()
    {
        _cells = new List<CellController>();
        _cells.Clear();

        _lines = new List<GameObject>();
        _lines.Clear();

        IsInitialized = true;
    }

    public void Prepare()
    {
        CellGrid = new CellController[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector2 gridInfo = new Vector2(j, gridSize - 1 - i);
                CellController cellController = SpawnCellController(gridInfo);
                CellGrid[i, j] = cellController;
            }
        }

        for (int i = 0; i < gridSize; i++)
        {
            var lineObject = Instantiate(linePrefab, lineTransform);
            _lines.Add(lineObject);
        }
    }

    private CellController SpawnCellController(Vector2 gridInfo)
    {
        CellController cellControllerObject = null;

        foreach (CellController cellController in _cells)
        {
            if (!cellController.gameObject.activeSelf)
            {
                cellControllerObject = cellController;
                break;
            }
        }

        if (cellControllerObject == null)
            cellControllerObject = CreateCellController();

        cellControllerObject.Initialize(gridInfo, cellSize);

        return cellControllerObject;
    }

    private CellController CreateCellController()
    {
        var cellControllerObject = Instantiate(cellControllerPrefab, gridParent);
        _cells.Add(cellControllerObject);
        return cellControllerObject;
    }

    public void Unload()
    {
        IsInitialized = false;
    }
}