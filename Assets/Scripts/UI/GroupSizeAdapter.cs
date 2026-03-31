using CannonTurret.CameraControl;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CannonTurret.UI
{
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
            _cameraAdapter.RatioChanged += OnUpdateCellSize;
        }

        private void Start()
        {
            OnUpdateCellSize();
        }

        private void OnDisable()
        {
            _cameraAdapter.OrientationChanged -= OnUpdateCellSize;
            _cameraAdapter.RatioChanged -= OnUpdateCellSize;
        }

        private void OnUpdateCellSize()
        {
            StartCoroutine(UpdateSize());
        }

        private IEnumerator UpdateSize()
        {
            yield return null;

            int offset = 1;
            var rect = _rectTransform.rect;
            var padding = _gridLayoutGroup.padding;
            var spacing = _gridLayoutGroup.spacing;
            int offsetsCount = _countElementsGroup - offset;
            float widthSpacing = spacing.x * offsetsCount;
            float heightSpacing = spacing.y * offsetsCount;
            float widthRect = rect.size.x - padding.right - padding.left - widthSpacing;
            float heightRect = rect.size.y - padding.bottom - padding.top - heightSpacing;
            var startAxis = _gridLayoutGroup.startAxis;

            if (startAxis == GridLayoutGroup.Axis.Vertical)
                heightRect = (heightRect - offset) / _countElementsGroup;
            else
                widthRect = (widthRect - offset) / _countElementsGroup;

            _gridLayoutGroup.cellSize = new Vector2(widthRect, heightRect);
        }
    }
}