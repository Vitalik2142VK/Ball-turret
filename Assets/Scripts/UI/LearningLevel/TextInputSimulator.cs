using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class TextInputSimulator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField, Range(0.01f, 1f)] private float _inputSpeed = 0.1f;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private string _outputText;

    public bool IsTextEntered { get; private set; }

    private void OnValidate()
    {
        if (_text == null)
            throw new NullReferenceException(nameof(_text));
    }

    private void Awake()
    {
        _wait = new WaitForSeconds(_inputSpeed);
        _outputText = "";

        IsTextEntered = true;
    }

    public void SimulateInput(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentOutOfRangeException(nameof(text));

        IsTextEntered = false;
        _outputText = text;
        _text.text = "";
        _coroutine = StartCoroutine(BeginSimulate());
    }

    public void OutputEntireText()
    {
        if (string.IsNullOrEmpty(_outputText))
            throw new IndexOutOfRangeException($"{_outputText} is null or empty");

        if (IsTextEntered == false)
        {
            StopCoroutine(_coroutine);

            _text.text = _outputText;
            IsTextEntered = true;
        }
    }

    private IEnumerator BeginSimulate()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < _outputText.Length; i++)
        {
            stringBuilder.Append(_outputText[i]);
            _text.text = stringBuilder.ToString();

            yield return _wait;
        }

        IsTextEntered = true;
    }
}