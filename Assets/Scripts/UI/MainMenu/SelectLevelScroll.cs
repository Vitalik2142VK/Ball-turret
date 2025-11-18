using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class SelectLevelScroll : MonoBehaviour
{
    [SerializeField] private ContentSizeFitter _content;
    [SerializeField] private SelectLevelButton _selectLevelButtonPrefab;
    [SerializeField, Range(0.5f, 1.5f)] private float _scaleButton = 1f;

    private List<SelectLevelButton> _selectLevelButtons;
    private ScrollRect _scrollRect;

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
    }

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
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
            button.SetSize(_scaleButton);
            button.transform.SetParent(_content.transform);
            button.transform.localScale = _selectLevelButtonPrefab.transform.localScale;
            _selectLevelButtons.Add(button);
        }
    }

    public void SelectButton(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex > _selectLevelButtons.Count)
            throw new ArgumentOutOfRangeException(nameof(levelIndex));

        if (levelIndex == _selectLevelButtons.Count)
            levelIndex = 0;

        foreach (var button in _selectLevelButtons)
        {
            if (button.Index == levelIndex)
            {
                button.Select();

                ScrollToButton(button);

                return;
            }
        }
    }

    private void OnSelectButton(int buttonIndex)
    {
        foreach (var button in _selectLevelButtons)
        {
            if (button.IsBocked)
                continue;

            if (button.Index != buttonIndex)
                button.CancelSelection();
            else
                SelectedLevelIndex = button.Index;
        }
    }

    private void ScrollToButton(SelectLevelButton button)
    {
        if (button.TryGetComponent(out RectTransform rectTransform) == false)
            throw new InvalidOperationException($"{nameof(button)} does not contain the '{nameof(RectTransform)}' component");

        ScrollRectUtil.ScrollToElement(_scrollRect, rectTransform);
    }
}
