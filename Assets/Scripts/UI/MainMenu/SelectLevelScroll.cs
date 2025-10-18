using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelScroll : MonoBehaviour
{
    [SerializeField] private ContentSizeFitter _content;
    [SerializeField] private SelectLevelButton _selectLevelButtonPrefab;
    [SerializeField] private TextMeshProUGUI _currentLevelTest;

    private List<SelectLevelButton> _selectLevelButtons;

    public int SelectedLevelIndex { get; private set; }

    private void OnValidate()
    {
        if (_content == null)
        {
            _content = GetComponentInChildren<ContentSizeFitter>();

            if (_content == null)
                throw new NullReferenceException(nameof(_content));
        }

        if (_selectLevelButtonPrefab == null)
            throw new NullReferenceException(nameof(_selectLevelButtonPrefab));

        if (_currentLevelTest == null)
            throw new NullReferenceException(nameof(_currentLevelTest));
    }

    private void OnEnable()
    {
        foreach (var button in _selectLevelButtons)
            button.Clicked += OnSelectButton;
    }

    private void OnDisable()
    {
        foreach (var button in _selectLevelButtons)
            button.Clicked -= OnSelectButton;
    }

    public void Initialize(int countLevelPlanners, int achievedLevelIndex)
    {
        if (countLevelPlanners <= 0)
            throw new ArgumentOutOfRangeException(nameof(countLevelPlanners));

        if (achievedLevelIndex < 0 || achievedLevelIndex > countLevelPlanners)
            throw new ArgumentOutOfRangeException(nameof(achievedLevelIndex));

        _selectLevelButtons = new List<SelectLevelButton>(countLevelPlanners);

        for (int i = 0; i < countLevelPlanners; i++)
        {
            var button = Instantiate(_selectLevelButtonPrefab);
            button.SetIndex(i);
            button.SetBlock(achievedLevelIndex < i);
            button.transform.SetParent(_content.transform);
            button.transform.localScale = _selectLevelButtonPrefab.transform.localScale;
            _selectLevelButtons.Add(button);
        }
    }

    public void SelectButton(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex > _selectLevelButtons.Count)
            throw new ArgumentOutOfRangeException(nameof(levelIndex));

        foreach (var button in _selectLevelButtons)
            if (button.Index == levelIndex)
                button.Press();
    }

    private void OnSelectButton(int buttonIndex)
    {
        foreach (var button in _selectLevelButtons)
        {
            if (button.IsBocked)
                continue;

            if (button.Index != buttonIndex)
            {
                button.PressOut();
            }
            else
            {
                SelectedLevelIndex = button.Index;
                _currentLevelTest.text = button.TextIndex;
            }
        }
    }
}
