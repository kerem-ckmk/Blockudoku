using System;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [Header("References")]
    public GameObject blockPrefab;
    public Transform tileTransform;
    public Vector2 blockSize;
    public List<Vector2> blockOffsets;
    public int rowCount;
    public int columnCount;
    [HideInInspector] public BoolCollection[] shape;

    public GridController GridController { get; private set; }
    public bool IsInitialized { get; private set; }
    public List<GameObject> BlockList { get; private set; }

    private Vector3 _screenSpace;
    private Vector3 _offset;
    private Camera _camera;

    public event Action<TileController> OnDestroyTile;

    public void Initialize(GridController gridController, Camera camera)
    {
        _camera = camera;
        GridController = gridController;
        CreateShape();
        IsInitialized = true;
    }

    public void CreateShape()
    {
        BlockList = new List<GameObject>();

        List<Vector3> blockPositions = CalculateBlockPositions();

        foreach (Vector3 localPosition in blockPositions)
        {
            GameObject block = CreateBlockObject(localPosition);
            BlockList.Add(block);
        }
    }

    private GameObject CreateBlockObject(Vector2 localPosition)
    {
        GameObject block = Instantiate(blockPrefab, tileTransform);
        block.transform.localPosition = localPosition;
        return block;
    }

    private List<Vector3> CalculateBlockPositions()
    {
        List<Vector3> blockPositions = new List<Vector3>();

        Vector3 startPosition = Vector3.zero;

        float offsetX = (columnCount - 1) * blockSize.x * 0.5f;
        float offsetY = (rowCount - 1) * blockSize.y * 0.5f;

        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < columnCount; c++)
            {
                if (shape[r].Collection[c])
                {
                    Vector3 position = startPosition + new Vector3(c * blockSize.x - offsetX, -r * blockSize.y + offsetY, 0f);
                    blockPositions.Add(position);
                }
            }
        }

        return blockPositions;
    }

    public void OnMouseDown()
    {
        _screenSpace = _camera.WorldToScreenPoint(transform.position);
        _offset = transform.position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y - GameConfigs.Instance.TileMouseDragOffset, _screenSpace.z));
        transform.localScale *= GameConfigs.Instance.TileDragScale;
    }

    public void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z);
        Vector3 curPosition = _camera.ScreenToWorldPoint(curScreenSpace) + _offset;

        transform.position = curPosition;
        transform.SetLocalPositionZ(1f);
    }

    public void OnMouseUp()
    {
        bool canPlace = true;
        List<CellController> cellsToFill = new List<CellController>();

        foreach (var block in BlockList)
        {
            CellController cell = GetCellUnderneath(block.transform.position);
            if (cell == null || cell.IsFull)
            {
                canPlace = false;
                break;
            }
            cellsToFill.Add(cell);
        }

        if (canPlace)
        {
            foreach (var cell in cellsToFill)
            {
                cell.SetFull(true);
            }

            OnDestroyTile?.Invoke(this);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }

        transform.localScale = Vector3.one;
    }

    private CellController GetCellUnderneath(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up);
        if (hit.collider != null)
        {
            CellController cell = hit.collider.GetComponent<CellController>();
            if (cell != null && !cell.IsFull)
            {
                return cell;
            }
        }
        return null;
    }

    [System.Serializable]
    public class BoolCollection
    {
        public bool[] Collection;
    }
}