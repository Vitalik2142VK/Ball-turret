using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ChangeSceneButton))]
public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private PlaySceneLoader _sceneLoader;
    [SerializeField] private ChangeSceneButton _changeSceneButton;

    private Button _button;
    private ILevelFactory _levelFactory;
    private ILevel _currentLevel;
    private IWinStatus _winStatus;
    private int _nextIndex;

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

    private void Start()
    {
        if (_currentLevel == null || _levelFactory == null || _winStatus == null)
            return;

        _nextIndex = _currentLevel.Index + 1;

        if (_nextIndex > _levelFactory.LevelsCount || _winStatus.IsWin == false)
        {
            gameObject.SetActive(false);
            _nextIndex = 0;
        }
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnEstablishLevel);
    }

    public void Initialize(IChangeSceneStep changeSceneStep, ILevelFactory levelFactory, ILevel currentLevel, IWinStatus winStatus)
    {
        _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));
        _currentLevel = currentLevel ?? throw new ArgumentNullException(nameof(currentLevel));
        _winStatus = winStatus ?? throw new ArgumentNullException(nameof(winStatus));

        _changeSceneButton.Initialize(changeSceneStep, _sceneLoader);
    }

    private void OnEstablishLevel()
    {
        ILevel level = _levelFactory.Create(_nextIndex);
        _sceneLoader.SetSelectedLevel(level);
    }
}
