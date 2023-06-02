using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

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

    private Vector2 _firstTransform;
    private Vector3 _screenSpace;
    private Vector3 _offset;
    public void Initialize(GridController gridController)
    {
        _firstTransform = transform.position;
        GridController = gridController;
        CreateShape();
        IsInitialized = true;
    }

    public void CreateShape()
    {
        BlockList = new List<GameObject>();
        BlockList.Clear();

        List<Vector3> blockPositions = CalculateBlockPositions();

        foreach (Vector3 localPosition in blockPositions)
        {
            CreateBlockObject(localPosition);
        }
    }

    private GameObject CreateBlockObject(Vector2 localPosition)
    {
        GameObject block = Instantiate(blockPrefab,tileTransform);
        block.transform.localPosition = localPosition;
        BlockList.Add(block);
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
        _screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y - GameConfigs.Instance.TileMouseDragOffset, _screenSpace.z));
        transform.localScale *= GameConfigs.Instance.TileDragScale;
    }

    public void OnMouseDrag()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + _offset;

        transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime * GameConfigs.Instance.TileDragSpeed);
    }
    //public void OnMouseUp()
    //{
    //    if (CanPlaceOnGrid())
    //    {
    //        Debug.Log("Success");
    //        // Place the tile on the grid
    //        // This should include marking the grid cells as full
    //        // and possibly anchoring the tile to its grid cells
    //    }
    //    else
    //    {
    //        transform.position = _firstTransform;
    //    }

    //    transform.localScale = Vector3.one;
    //}

    [System.Serializable]
    public class BoolCollection
    {
        [SerializeField]
        public bool[] Collection;
    }
}