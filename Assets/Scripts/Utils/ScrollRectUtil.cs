using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectUtil
{
    public static void ScrollToElement(ScrollRect scroll, RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 viewportLocalPos = scroll.viewport.InverseTransformPoint(scroll.viewport.position);
        Vector2 targetLocalPos = scroll.viewport.InverseTransformPoint(target.position);

        float deltaY = targetLocalPos.y - viewportLocalPos.y;

        Vector2 newPos = scroll.content.anchoredPosition + new Vector2(0f, deltaY);

        float contentHeight = scroll.content.rect.height;
        float viewportHeight = scroll.viewport.rect.height;

        newPos.y = Mathf.Clamp(newPos.y, contentHeight - viewportHeight, 0f);
        scroll.content.anchoredPosition = newPos;
    }
}
