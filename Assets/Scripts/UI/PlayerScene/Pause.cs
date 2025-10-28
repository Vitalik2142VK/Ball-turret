using UnityEngine;

public class Pause : MonoBehaviour
{
    private const float EnableTimeScale = 1f;
    private const float DisableTimeScale = 0f;

    private OpenWindowButton _pauseButton;

    public void Initialize(OpenWindowButton pauseButton)
    {
        _pauseButton = pauseButton != null ? pauseButton : throw new System.ArgumentNullException(nameof(pauseButton));

        Disable();
    }

    public void Enable()
    {
        Time.timeScale = DisableTimeScale;

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _pauseButton.Show();

        Time.timeScale = EnableTimeScale;
    }
}
