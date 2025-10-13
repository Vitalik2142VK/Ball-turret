using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollAdapter : MonoBehaviour
{
    [SerializeField] private ContentSizeFitter _contentSizeFitter;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    private ICameraAdapter _cameraAdapter;
    private ScrollRect _scrollRect;
    private RectTransform _contentRectTransform;

    private void OnValidate()
    {
        if (_contentSizeFitter == null)
            _contentSizeFitter = GetComponentInChildren<ContentSizeFitter>();

        if (_contentSizeFitter == null)
            throw new NullReferenceException(nameof(_contentSizeFitter));

        if (_gridLayoutGroup == null)
            _gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();

        if (_gridLayoutGroup == null)
            throw new NullReferenceException(nameof(_gridLayoutGroup));
    }

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _contentRectTransform = _contentSizeFitter.GetComponent<RectTransform>();

        Camera camera = Camera.main;

        if (camera.TryGetComponent(out ICameraAdapter cameraAdapter) == false)
            throw new InvalidOperationException($"The main camera does not contain the component: <{nameof(ICameraAdapter)}>");

        _cameraAdapter = cameraAdapter;
    }

    private void OnEnable()
    {
        _cameraAdapter.OrientationChanged += OnChangeScrollSetting;
    }


    private void Start()
    {
        OnChangeScrollSetting();
    }

    private void OnDisable()
    {
        _cameraAdapter.OrientationChanged -= OnChangeScrollSetting;
    }

    private void OnChangeScrollSetting()
    {
        if (_cameraAdapter.IsPortraitOrientation)
            EstablishVerticalSettings();
        else
            EstablishHorisontalSettings();
    }

    private void EstablishVerticalSettings()
    {
        _scrollRect.vertical = true;
        _scrollRect.horizontal = false;
        _contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        _contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        _gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
        var rect = _contentRectTransform.rect;
        rect.height = 0f;
    }

    private void EstablishHorisontalSettings()
    {
        _scrollRect.vertical = false;
        _scrollRect.horizontal = true;
        _contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        _contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        _gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
        var rect = _contentRectTransform.rect;
    }
}
