using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Pause : MonoBehaviour
{
    private const float EnableTimeScale = 1f;
    private const float DisableTimeScale = 0f;

    private CanvasGroup _canvasGroup;
    private OpenWindowButton _pauseButton;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    public void Initialize(OpenWindowButton pauseButton)
    {
        _pauseButton = pauseButton != null ? pauseButton : throw new System.ArgumentNullException(nameof(pauseButton));

        Disable();
    }

    public void Enable()
    {
        Time.timeScale = DisableTimeScale;

        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    public void Disable()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _pauseButton.Show();

        Time.timeScale = EnableTimeScale;
    }
}
