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
    public int rowCount;
    public int columnCount;
    [HideInInspector] public BoolCollection[] shape;

    public bool IsInitialized { get; private set; }
    public List<GameObject> BlockList { get; private set; }

    public void Initialize()
    {
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

    [System.Serializable]
    public class BoolCollection
    {
        [SerializeField]
        public bool[] Collection;
    }
}