using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class CanvasPointerChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;
    private List<RaycastResult> _raycastResults;

    private void Awake()
    {
        _eventSystem = GetComponent<EventSystem>();
        _pointerEventData = new PointerEventData(_eventSystem);
        _raycastResults = new List<RaycastResult>();
    }

    public bool IsPointerOverUI(Vector2 touchPosition)
    {
        _pointerEventData.Reset();
        _pointerEventData.position = touchPosition;

        _raycastResults.Clear();
        _eventSystem.RaycastAll(_pointerEventData, _raycastResults);

        if (_raycastResults.Count == 0)
            return false;

        return LayerMaskTool.IsInLayerMask(_raycastResults[0].gameObject, _layerMask);
    }
}