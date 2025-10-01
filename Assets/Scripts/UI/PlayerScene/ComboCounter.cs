using System.Collections;
using TMPro;
using UnityEngine;

public class ComboCounter : MonoBehaviour, IComboCounter
{
    [SerializeField] private TextMeshProUGUI _textCombo;
    [SerializeField] private Gradient _colorCombo;
    [SerializeField, Min(0.5f)] private float _timeRemove = 1f;
    [SerializeField, Min(9)] private int _maxCombo = 30;
    [SerializeField, Min(3)] private int _minCombo = 3;

    private WaitForSeconds _wait;
    private Coroutine _coroutineTimer;
    private int _currentCombo;

    private void OnValidate()
    {
        if (_textCombo == null)
            throw new System.NullReferenceException(nameof(_textCombo));
    }

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeRemove);
        _currentCombo = 0;

        gameObject.SetActive(false);
    }

    public void Count()
    {
        _currentCombo++;

        if (_currentCombo < _minCombo)
            return;

        gameObject.SetActive(true);

        float colorValue = Mathf.Clamp01((float)_currentCombo / _maxCombo);
        _textCombo.text = _currentCombo.ToString();
        _textCombo.color = _colorCombo.Evaluate(colorValue);

        StartTimer();
    }

    private void StartTimer()
    {
        if (_coroutineTimer != null)
            StopCoroutine(_coroutineTimer);

        _coroutineTimer = StartCoroutine(WaitTimer());
    }

    private IEnumerator WaitTimer()
    {
        yield return _coroutineTimer;

        _currentCombo = 0;
        gameObject.SetActive(false);
    }
}
