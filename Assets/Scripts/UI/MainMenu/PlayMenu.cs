using System;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private SelectLevelScroll _selectLevelScroll;
    [SerializeField] private GameObject[] _interferingUI;

    private IMenu _previousMenu;
    private ILevelFactory _levelFactory;
    private PlaySceneLoader _sceneLoader;
    private int _achievedLevelIndex;

    private void OnValidate()
    {
        if (_selectLevelScroll == null)
            throw new NullReferenceException(nameof(_selectLevelScroll));
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(IPlayer user, ILevelFactory levelFactory, PlaySceneLoader sceneLoader)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _sceneLoader = sceneLoader != null ? sceneLoader : throw new ArgumentNullException(nameof(sceneLoader));
        _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));

        _selectLevelScroll.Initialize(levelFactory.LevelsCount, user.AchievedLevelIndex);
        _achievedLevelIndex = user.AchievedLevelIndex;
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        _previousMenu.Enable();

        foreach (var ui in _interferingUI)
            ui.SetActive(true);
    }

    public void OnPlay()
    {
        var indexLevel = _selectLevelScroll.SelectedLevelIndex;
        var level = _levelFactory.Create(indexLevel);
        _sceneLoader.SetSelectedLevel(level);
        _sceneLoader.Load();
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));
        gameObject.SetActive(true);
        _selectLevelScroll.SelectButton(_achievedLevelIndex);

        foreach (var ui in _interferingUI)
            ui.SetActive(false);
    }
}
