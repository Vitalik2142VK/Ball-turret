using System;
using System.Collections.Generic;
using UnityEngine;

public class LearningStage : MonoBehaviour, ILearningStage
{
    [SerializeField] private TextInputSimulator _textInputSimulator;
    [SerializeField] private TextLocalizer[] _localizedTexts;
    [SerializeField, Min(1)] private int _waveNumber;

    private Queue<TextLocalizer> _texts;
    private TextLocalizer _currentTextLocalizer;


    public int NumberStages => _localizedTexts.Length;
    public int WaveNumber => _waveNumber;
    public bool IsActive => gameObject.activeSelf;

    public int CurrentStage { get; private set; }

    private void OnValidate()
    {
        if (_textInputSimulator == null)
            throw new NullReferenceException(nameof(_textInputSimulator));

        if (_localizedTexts == null || _localizedTexts.Length == 0)
            throw new InvalidOperationException(nameof(_localizedTexts));

        foreach (var localizedText in _localizedTexts)
            if (localizedText == null)
                throw new InvalidOperationException(nameof(_localizedTexts));
    }

    private void Awake()
    {
        gameObject.SetActive(false);

        if (_localizedTexts.Length > 1)
            _texts = new Queue<TextLocalizer>(_localizedTexts);
        else
            _currentTextLocalizer = _localizedTexts[0];

        CurrentStage = 0;
    }

    public void Enable()
    {
        gameObject.SetActive(true);

        SetNextText();
    }

    public void HandleСlick()
    {
        if (_textInputSimulator.IsTextEntered == false)
        {
            _textInputSimulator.OutputEntireText();

            return;
        }

        if (_texts == null || _texts.Count == 0)
            gameObject.SetActive(false);
        else
            SetNextText();
    }

    private void SetNextText()
    {
        if (_texts != null)
        {
            if (_texts.Count == 0)
                throw new InvalidOperationException("The number of texts is 0");

            _currentTextLocalizer = _texts.Dequeue();
        }

        _textInputSimulator.SimulateInput(_currentTextLocalizer.Text);

        CurrentStage++;
    }
}
