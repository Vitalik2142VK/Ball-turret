using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollerToElement : MonoBehaviour
{
    [SerializeField] private float _offset;

    private ScrollRect _scrollRect;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    public void ScrollToElement(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 viewportLocalPos = _scrollRect.viewport.InverseTransformPoint(_scrollRect.viewport.position);
        Vector2 targetLocalPos = _scrollRect.viewport.InverseTransformPoint(target.position);

        float deltaY = Mathf.Abs(targetLocalPos.y - viewportLocalPos.y);

        Vector2 newPos = _scrollRect.content.anchoredPosition + new Vector2(0f, deltaY);

        float contentHeight = _scrollRect.content.rect.height;
        float viewportHeight = _scrollRect.viewport.rect.height;
        float maxY = contentHeight - viewportHeight;

        float newPosY = Mathf.Clamp(newPos.y, 0f, maxY);

        if (newPosY != maxY)
            newPosY += _offset;

        newPos.y = newPosY;
        _scrollRect.content.anchoredPosition = newPos;
    }
}
