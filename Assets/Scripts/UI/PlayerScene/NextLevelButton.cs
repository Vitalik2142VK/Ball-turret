using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private PlaySceneLoader _sceneLoader;

    private Button _button;
    private IChangeSceneStep _changeSceneStep;

    private void OnValidate()
    {
        if (_sceneLoader == null)
            throw new NullReferenceException(nameof(_sceneLoader));
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnChangeScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnChangeScene);
    }

    public void Initialize(IChangeSceneStep changeSceneStep)
    {
        _changeSceneStep = changeSceneStep ?? throw new ArgumentNullException(nameof(changeSceneStep));
    }

    private void OnChangeScene()
    {
        //_sceneLoader.SetSelectedLevel();
        _changeSceneStep.SetSceneLoader(_sceneLoader);
    }
}
