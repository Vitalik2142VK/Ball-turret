using System;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private PlaySceneLoader _sceneLoader;
    [SerializeField] private SelectLevelScroll _selectLevelScroll;
    [SerializeField] private GameObject[] _interferingUI;

    private IMenu _previousMenu;
    private ILevelFactory _levelFactory;
    private int _achievedLevelIndex;

    private void OnValidate()
    {
        if (_sceneLoader == null)
            throw new NullReferenceException(nameof(_sceneLoader));

        if (_selectLevelScroll == null)
            throw new NullReferenceException(nameof(_selectLevelScroll));
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(IPlayer user, ILevelFactory levelFactory)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));

        _selectLevelScroll.Initialize(levelFactory.LevelsCount, user.AchievedLevelIndex);
        _achievedLevelIndex = user.AchievedLevelIndex;
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        _previousMenu.Enable();

        foreach (var ui in _interferingUI)
            if (ui != null)
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
            if (ui != null)
                ui.SetActive(false);
    }
}
