using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public GameObject fullCell;
    public GameObject defaultCell;
    public bool IsInitiailized { get; private set; }
    public bool IsFull { get; private set; }
    public Vector2 GridInfo { get; private set; }
    private BoxCollider2D _collider;

    public void Initialize(Vector2 gridInfo, float size)
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.size = size * Vector2.one;
        GridInfo = gridInfo;
        IsInitiailized = true;
    }

    public void SetFull(bool full)
    {
        IsFull = full;
        fullCell.SetActive(IsFull);
        defaultCell.SetActive(!IsFull);
    }
}
