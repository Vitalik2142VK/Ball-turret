using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ComboCounter : MonoBehaviour, IComboCounter, IComboCounterResetter
{
    [SerializeField] private TextMeshProUGUI _comboCounterTitle;
    [SerializeField] private TextMeshProUGUI _comboCounter;
    [SerializeField] private Gradient _colorCombo;
    [SerializeField, Min(0.5f)] private float _timeRemove = 1f;
    [SerializeField, Min(9)] private int _maxCombo = 30;
    [SerializeField, Min(3)] private int _minCombo = 3;

    private WaitForSeconds _wait;
    private int _currentCombo;

    private void OnValidate()
    {
        if (_comboCounterTitle == null)
            throw new NullReferenceException(nameof(_comboCounterTitle));

        if (_comboCounter == null)
            throw new NullReferenceException(nameof(_comboCounter));
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
        Color color = _colorCombo.Evaluate(colorValue);
        _comboCounter.text = _currentCombo.ToString();
        _comboCounterTitle.color = color;
        _comboCounter.color = color;
    }

    public void ResetCombo()
    {
        _currentCombo = 0;

        if (gameObject.activeSelf)
            StartCoroutine(WaitTimer());
    }

    private IEnumerator WaitTimer()
    {
        yield return _wait;

        gameObject.SetActive(false);
    }
}
