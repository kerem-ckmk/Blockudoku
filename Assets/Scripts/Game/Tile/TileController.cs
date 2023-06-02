using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class TileController : MonoBehaviour
{
    public BoolCollection[] Shape;
    public GameObject BlockPrefab;
    public Transform TileTransform;
    public Vector2 blockSize = new Vector2(100f, 100f);

    public int rowCount;
    public int columnCount;

    public void UpdateShape()
    {
        List<Vector3> blockPositions = CalculateBlockPositions();

        foreach (Vector3 localPosition in blockPositions)
        {
            GameObject newBlock = Instantiate(BlockPrefab, localPosition, Quaternion.identity, TileTransform);
        }
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
                if (Shape[r].Collection[c])
                {
                    Vector3 position = startPosition + new Vector3(c * blockSize.x - offsetX, -r * blockSize.y + offsetY, 0f);
                    Vector3 localPosition = transform.TransformPoint(position);
                    blockPositions.Add(localPosition);
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