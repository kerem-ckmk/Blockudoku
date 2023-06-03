using System.Collections.Generic;
using UnityEngine;

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

    private List<CellController> _cells = new List<CellController>();
    private List<GameObject> _lines = new List<GameObject>();

    public void Initialize()
    {
        _cells.Clear();
        _lines.Clear();
        IsInitialized = true;
    }

    public void Prepare()
    {
        CreateGrid();
        InstantiateLines();
    }

    private void CreateGrid()
    {
        CellGrid = new CellController[gridSize, gridSize];
        for (int row = 0; row < gridSize; row++)
            for (int col = 0; col < gridSize; col++)
                CellGrid[row, col] = SpawnCellController(new Vector2(col, gridSize - 1 - row));
    }

    private void InstantiateLines()
    {
        for (int i = 0; i < gridSize; i++)
            _lines.Add(Instantiate(linePrefab, lineTransform));
    }

    private CellController SpawnCellController(Vector2 gridInfo)
    {
        CellController cellController = FindInactiveCell() ?? CreateCellController();
        cellController.Initialize(gridInfo, cellSize);
        return cellController;
    }

    private CellController FindInactiveCell()
    {
        return _cells.Find(cellController => !cellController.gameObject.activeSelf);
    }

    private CellController CreateCellController()
    {
        var cellController = Instantiate(cellControllerPrefab, gridParent);
        _cells.Add(cellController);
        return cellController;
    }

    public void CheckGrid()
    {
        ClearCompleteRows();
        ClearCompleteColumns();
        ClearCompleteBlocks();
    }

    private void ClearCompleteRows()
    {
        for (int row = 0; row < gridSize; row++)
            if (IsRowComplete(row))
                SetRowActive(row, false);
    }

    private bool IsRowComplete(int row)
    {
        for (int col = 0; col < gridSize; col++)
            if (!CellGrid[row, col].IsFull)
                return false;
        return true;
    }

    private void SetRowActive(int row, bool isActive)
    {
        for (int col = 0; col < gridSize; col++)
            CellGrid[row, col].SetFull(isActive);
    }

    private void ClearCompleteColumns()
    {
        for (int col = 0; col < gridSize; col++)
            if (IsColumnComplete(col))
                SetColumnActive(col, false);
    }

    private bool IsColumnComplete(int col)
    {
        for (int row = 0; row < gridSize; row++)
            if (!CellGrid[row, col].IsFull)
                return false;
        return true;
    }

    private void SetColumnActive(int col, bool isActive)
    {
        for (int row = 0; row < gridSize; row++)
            CellGrid[row, col].SetFull(isActive);
    }

    private void ClearCompleteBlocks()
    {
        for (int row = 0; row < gridSize; row += 3)
            for (int col = 0; col < gridSize; col += 3)
                if (IsBlockComplete(row, col))
                    SetBlockActive(row, col, false);
    }

    private bool IsBlockComplete(int startRow, int startCol)
    {
        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
                if (!CellGrid[startRow + row, startCol + col].IsFull)
                    return false;
        return true;
    }

    private void SetBlockActive(int startRow, int startCol, bool isActive)
    {
        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
                CellGrid[startRow + row, startCol + col].SetFull(isActive);
    }

    public void Unload()
    {
        IsInitialized = false;
    }
}