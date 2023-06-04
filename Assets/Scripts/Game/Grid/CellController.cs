using DG.Tweening;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public GameObject fullCell;
    public GameObject defaultCell;
    public bool IsInitiailized { get; private set; }
    public bool IsFull { get; private set; }
    public Vector2Int GridInfo { get; private set; }
    private BoxCollider2D _collider;

    private Sequence _sequence;
    public void Initialize(Vector2Int gridInfo, float size)
    {
        gameObject.SetActive(true);
        defaultCell.SetActive(true);
        fullCell.SetActive(false);

        _collider = GetComponent<BoxCollider2D>();
        _collider.size = size * Vector2.one;
        GridInfo = gridInfo;
        IsInitiailized = true;
    }

    public void SetFull(bool full)
    {
        if (full == IsFull)
            return;

        IsFull = full;

        fullCell.SetActive(IsFull);
        defaultCell.SetActive(!IsFull);

        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        if (IsFull)
            AnimateScaling(fullCell, Vector3.one, 0.3f);
        else
            AnimateScaling(defaultCell, Vector3.one, 0.45f);
    }

    private void AnimateScaling(GameObject target, Vector3 targetScale, float duration)
    {
        target.transform.localScale = 0.5f * targetScale;
        _sequence.Append(target.transform.DOScale(targetScale, duration).SetEase(Ease.OutQuart));
        _sequence.Play();
    }
}
