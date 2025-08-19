using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//todo Remove on realise
public class Console : MonoBehaviour
{
    private const string EndText = "\n...|||...\n\n";

    private static Console s_Console;

    [SerializeField] private ContentSizeFitter _content;
    [SerializeField] private TextMeshProUGUI _textPrefab;

    public static void GetLog(string message)
    {
        if (s_Console == null)
            CreateSingleton();

        if (s_Console == null)
            return;

        s_Console.Log(message);
    }

    public static void GetException(Exception ex)
    {
        string exceptionMessage = $"Exception: {ex.Message}\nStack:{ex.StackTrace}";

        GetLog(exceptionMessage);
    }

    private static void CreateSingleton()
    {
        if (s_Console == null)
            s_Console = FindAnyObjectByType<Console>();
    }

    private void OnValidate()
    {
        if (_content == null)
            _content = transform.GetComponentInChildren<ContentSizeFitter>();

        if (_content == null)
            throw new NullReferenceException(nameof(_content));

        if (_textPrefab == null)
            _textPrefab = transform.GetComponentInChildren<TextMeshProUGUI>();

        if (_textPrefab == null)
            throw new NullReferenceException(nameof(_textPrefab));
    }

    private void OnEnable()
    {
        if (s_Console == null)
            s_Console = this;
    }

    private void OnDisable()
    {
        s_Console = null;
    }

    private void Log(string message)
    {
        var text = Instantiate(_textPrefab, _content.transform);
        text.text = $"{message}{EndText}";
    }
}
