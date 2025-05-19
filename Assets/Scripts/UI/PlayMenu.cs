using System;
using TMPro;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private SelectLevelScroll _selectLevelScroll;

    private GameObject _gameObject;
    private PlaySceneLoader _sceneLoader;
    private IMenu _previousMenu;
    private ILevelFactory _levelFactory;
    private int _achievedLevelIndex;

    private void OnValidate()
    {
        if (_selectLevelScroll == null)
            throw new NullReferenceException(nameof(_selectLevelScroll));
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
    }

    public void Initialize(IUser user, ILevelFactory levelFactory, PlaySceneLoader sceneLoader)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _sceneLoader = sceneLoader != null ? sceneLoader : throw new ArgumentNullException(nameof(sceneLoader));
        _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));

        _selectLevelScroll.Initialize(levelFactory.LevelsCount, user.AchievedLevelIndex);
        _achievedLevelIndex = user.AchievedLevelIndex;
    }

    public void Close()
    {
        _gameObject.SetActive(false);
        _previousMenu.Enable();
    }

    public void Play()
    {
        var indexLevel = _selectLevelScroll.SelectedLevelIndex;
        var level = _levelFactory.Create(indexLevel);
        _sceneLoader.SetSelectedLevel(level);
        _sceneLoader.Load();
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));
        _gameObject.SetActive(true);
        _selectLevelScroll.SelectButton(_achievedLevelIndex);
    }
}
