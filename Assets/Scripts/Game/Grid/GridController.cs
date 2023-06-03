using System.Collections.Generic;
using UnityEditor.PackageManager;
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
    public CellController[,] Cells { get; private set; }

    private List<GameObject> _lines;

    public void Initialize()
    {
        Cells = new CellController[gridSize, gridSize];
        _lines = new List<GameObject>();
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
        for (int row = 0; row < gridSize; row++)
            for (int col = 0; col < gridSize; col++)
                Cells[row, col] = SpawnCellController(new Vector2Int(col, gridSize - 1 - row));
    }

    private void InstantiateLines()
    {
        if (_lines.Count > 0)
            return;

        for (int i = 0; i < gridSize; i++)
            _lines.Add(Instantiate(linePrefab, lineTransform));
    }

    private CellController SpawnCellController(Vector2Int gridInfo)
    {
        CellController cellController = null;

        foreach (var cell in Cells)
        {
            if (cell != null && !cell.gameObject.activeSelf)
            {
                cellController = cell;
                break;
            }
        }

        if (cellController == null)
        {
            cellController = CreateCellController();
        }

        cellController.Initialize(gridInfo, cellSize);

        return cellController;
    }

    private CellController CreateCellController()
    {
        var cellController = Instantiate(cellControllerPrefab, gridParent);
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
            if (!Cells[row, col].IsFull)
                return false;
        return true;
    }

    private void SetRowActive(int row, bool isActive)
    {
        for (int col = 0; col < gridSize; col++)
            Cells[row, col].SetFull(isActive);
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
            if (!Cells[row, col].IsFull)
                return false;
        return true;
    }

    private void SetColumnActive(int col, bool isActive)
    {
        for (int row = 0; row < gridSize; row++)
            Cells[row, col].SetFull(isActive);
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
                if (!Cells[startRow + row, startCol + col].IsFull)
                    return false;
        return true;
    }

    private void SetBlockActive(int startRow, int startCol, bool isActive)
    {
        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
                Cells[startRow + row, startCol + col].SetFull(isActive);
    }

    public void Unload()
    {
        foreach (var cell in Cells)
        {
            cell.SetFull(false);
            cell.gameObject.SetActive(false);
        }

        IsInitialized = false;
    }
    public CellController GetCell(Vector2Int gridInfo)
    {
        if (gridInfo.x < 0 || gridInfo.y < 0 || gridInfo.x >= gridSize || gridInfo.y >= gridSize)
            return null;

        return Cells[gridInfo.y, gridInfo.x];
    }
    public bool IsCellFull(Vector2Int gridInfo)
    {
        CellController cellController = GetCell(gridInfo);
        return cellController?.IsFull ?? false;
    }
}