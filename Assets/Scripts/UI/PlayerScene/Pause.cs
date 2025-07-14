using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private const float EnableTimeScale = 1f;
    private const float DisableTimeScale = 0f;

    [SerializeField] private Button _menuButton;

    private void OnValidate()
    {
        if (_menuButton == null)
            throw new System.ArgumentNullException(nameof(_menuButton));
    }

    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        Time.timeScale = DisableTimeScale;

        _menuButton.gameObject.SetActive(false);

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);

        _menuButton.gameObject.SetActive(true);

        Time.timeScale = EnableTimeScale;
    }
}