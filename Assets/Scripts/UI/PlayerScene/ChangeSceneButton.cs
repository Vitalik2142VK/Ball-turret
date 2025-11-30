using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    private Button _button;
    private IChangeSceneStep _changeSceneStep;
    private ISceneLoader _sceneLoader;

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

    public void Initialize(IChangeSceneStep changeSceneStep, ISceneLoader sceneLoader)
    {
        _changeSceneStep = changeSceneStep ?? throw new ArgumentNullException(nameof(changeSceneStep));
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
    }

    private void OnChangeScene()
    {
        _changeSceneStep.SetSceneLoader(_sceneLoader);
    }
}
