using System;
using System.Collections;
using UnityEngine;
using YG;

public class LocalisatorService : MonoBehaviour, ILocalisatorService
{
    private const string EnglishLanguage = "en";
    private const string RussianLanguage = "ru";
    private const string TurkishLanguage = "tr";

    [SerializeField] private Scriptable.LocalizationData _date;
    [SerializeField, Range(0.01f, 2f)] private float _timeWaitingResponse = 0.2f;

    private WaitForSeconds _waiting;

    public Language Language { get; private set; }

    private void OnValidate()
    {
        if (_date == null)
            throw new NullReferenceException(nameof(_date));
    }

    private void Awake()
    {
        _waiting = new WaitForSeconds(_timeWaitingResponse);

        if (_date.IsLanguageEstablished == false)
            _date.EstablishLanguage(this);
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnGetLocalization;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= OnGetLocalization;
    }

    private void OnGetLocalization()
    {
        StartCoroutine(GetLocalization());
    }

    private IEnumerator GetLocalization()
    {
        while (YandexGame.SDKEnabled == false)
            yield return _waiting;

        var language = YandexGame.EnvironmentData.language;

        switch (language)
        {
            case EnglishLanguage:
                Language = Language.EN;
                break;

            case RussianLanguage:
                Language = Language.RU;
                break;

            case TurkishLanguage:
                Language = Language.TR;
                break;

            default:
                Language = Language.EN;
                break;
        }

        _date.EstablishLanguage(this);
    }
}
