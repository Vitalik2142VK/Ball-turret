using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
public class GroupSizeAdapter : MonoBehaviour
{
    private ICameraAdapter _cameraAdapter;
    private RectTransform _rectTransform;
    private GridLayoutGroup _gridLayoutGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();

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

        _rectTransform.offsetMin = Vector2.zero;
        _rectTransform.offsetMax = Vector2.zero;

        float widthRect = _rectTransform.rect.size.x;
        float heightRect = _rectTransform.rect.size.y;
        float widthByAnchors = (_rectTransform.anchorMax.x - _rectTransform.anchorMin.x) * widthRect - _gridLayoutGroup.padding.right - _gridLayoutGroup.padding.left;
        float heightByAnchors = (_rectTransform.anchorMax.y - _rectTransform.anchorMin.y) * heightRect - _gridLayoutGroup.padding.bottom - _gridLayoutGroup.padding.top;

        _gridLayoutGroup.cellSize = new Vector2(widthRect, heightRect);
    }
}