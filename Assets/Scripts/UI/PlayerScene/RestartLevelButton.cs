using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ChangeSceneButton))]
public class RestartLevelButton : MonoBehaviour
{
    [SerializeField] private PlaySceneLoader _sceneLoader;
    [SerializeField] private ChangeSceneButton _changeSceneButton;

    private Button _button;
    private ILevel _currentLevel;

    private void OnValidate()
    {
        if (_sceneLoader == null)
            throw new NullReferenceException(nameof(_sceneLoader));

        if (_changeSceneButton == null)
            _changeSceneButton = GetComponent<ChangeSceneButton>();
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnEstablishLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnEstablishLevel);
    }

    public void Initialize(IChangeSceneStep changeSceneStep, ILevel currentLevel)
    {
        _currentLevel = currentLevel ?? throw new ArgumentNullException(nameof(currentLevel));

        _changeSceneButton.Initialize(changeSceneStep, _sceneLoader);
    }

    private void OnEstablishLevel()
    {
        ILevel level = _currentLevel.Clone();
        _sceneLoader.SetSelectedLevel(level);
    }
}