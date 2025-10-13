using UnityEngine;

public class Pause : MonoBehaviour
{
    private const float EnableTimeScale = 1f;
    private const float DisableTimeScale = 0f;

    private PauseButton _pauseButton;

    public void Initialize(PauseButton pauseButton)
    {
        _pauseButton = pauseButton != null ? pauseButton : throw new System.ArgumentNullException(nameof(pauseButton));

        Disable();
    }

    public void Enable()
    {
        Time.timeScale = DisableTimeScale;

        _pauseButton.gameObject.SetActive(false);

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);

        _pauseButton.gameObject.SetActive(true);

        Time.timeScale = EnableTimeScale;
    }
}
