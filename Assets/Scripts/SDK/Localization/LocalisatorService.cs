using System;
using UnityEngine;
using YG;

public class LocalisatorService : MonoBehaviour, ILocalisatorService
{
    private const string EnglishLanguage = "en";
    private const string RussianLanguage = "ru";
    private const string TurkishLanguage = "tr";

    [SerializeField] private Scriptable.LocalizationData _date;

    public Language Language { get; private set; }

    private void OnValidate()
    {
        if (_date == null)
            throw new NullReferenceException(nameof(_date));
    }

    private void Awake()
    {
        if (_date.IsLanguageEstablished == false)
            _date.EstablishLanguage(this);
    }

    private void OnEnable()
    {
        YG2.onGetSDKData += OnGetLocalization;
    }

    private void Start()
    {
        if (YG2.isSDKEnabled)
            OnGetLocalization();
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnGetLocalization;
    }

    private void OnGetLocalization()
    {
        var language = YG2.envir.language;

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
