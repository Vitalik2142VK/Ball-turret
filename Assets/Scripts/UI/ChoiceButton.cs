using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ChoiceButton : MonoBehaviour, IChoiceButton
{
    private const float AphaEnable = 1f;
    private const float AphaDisable = 0.5f;

    private CanvasGroup _canvasGroup;

    public void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Enable()
    {
        _canvasGroup.alpha = AphaEnable;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Disable()
    {
        _canvasGroup.alpha = AphaDisable;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
