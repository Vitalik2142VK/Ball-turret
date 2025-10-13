using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PushButtonAnimator), typeof(Button))]
public class PauseButton : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;

    private Button _button;

    private void OnValidate()
    {
        if (_pauseMenu == null)
            throw new NullReferenceException(nameof(_pauseMenu));
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void SetPauseMenu(PauseMenu pauseMenu)
    {
        _pauseMenu = pauseMenu != null ? pauseMenu : throw new ArgumentNullException(nameof(pauseMenu));
    }

    private void OnClick()
    {
        _pauseMenu.Enable();
    }
}