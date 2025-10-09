using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HiderUI), typeof(ShiftAnimatorUI))]
public class PlayMenu : MonoBehaviour
{
    [SerializeField] private PlaySceneLoader _sceneLoader;
    [SerializeField] private SelectLevelScroll _selectLevelScroll;

    private IMenu _previousMenu;
    private ILevelFactory _levelFactory;
    private IAnimatorUI _animator;
    private HiderUI _hiderUI;
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
        _hiderUI = GetComponent<HiderUI>();
        _animator = GetComponent<IAnimatorUI>();

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

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));

        gameObject.SetActive(true);
        _hiderUI.Disable();
        _animator.Show();

        StartCoroutine(WaitOpening());
    }

    public void OnClose()
    {
        _hiderUI.Enable();
        _animator.Hide();

        StartCoroutine(WaitClosure());
    }

    public void OnPlay()
    {
        var indexLevel = _selectLevelScroll.SelectedLevelIndex;
        var level = _levelFactory.Create(indexLevel);
        _sceneLoader.SetSelectedLevel(level);
        _sceneLoader.Load();
    }

    private IEnumerator WaitOpening()
    {
        yield return _animator.GetYieldAnimation();

        _selectLevelScroll.SelectButton(_achievedLevelIndex);
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);
        _previousMenu.Enable();
    }
}
