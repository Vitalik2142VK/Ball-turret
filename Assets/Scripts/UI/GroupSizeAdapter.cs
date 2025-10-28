using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
public class GroupSizeAdapter : MonoBehaviour
{
    private ICameraAdapter _cameraAdapter;
    private RectTransform _rectTransform;
    private GridLayoutGroup _gridLayoutGroup;
    private int _countElementsGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _countElementsGroup = transform.childCount;

        Camera camera = Camera.main;

        if (camera.TryGetComponent(out ICameraAdapter cameraAdapter) == false)
            throw new System.InvalidOperationException($"The main camera does not contain the component: <{nameof(ICameraAdapter)}>");

        _cameraAdapter = cameraAdapter;
    }

    private void OnEnable()
    {
        _cameraAdapter.OrientationChanged += OnUpdateCellSize;
    }

    private void Start()
    {
        OnUpdateCellSize();
    }

    private void OnDisable()
    {
        _cameraAdapter.OrientationChanged -= OnUpdateCellSize;
    }

    private void OnUpdateCellSize()
    {
        StartCoroutine(UpdateSize());
    }

    private IEnumerator UpdateSize()
    {
        yield return null;

        float widthRect = (_rectTransform.rect.size.x - _gridLayoutGroup.padding.right - _gridLayoutGroup.padding.left);
        float heightRect = (_rectTransform.rect.size.y - _gridLayoutGroup.padding.bottom - _gridLayoutGroup.padding.top);
        var startAxis = _gridLayoutGroup.startAxis;

        if (startAxis == GridLayoutGroup.Axis.Vertical)
            heightRect /= _countElementsGroup;
        else
            widthRect /= _countElementsGroup;

        _gridLayoutGroup.cellSize = new Vector2(widthRect, heightRect);
    }
}