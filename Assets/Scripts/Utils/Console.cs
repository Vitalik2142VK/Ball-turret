using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private static Console s_Console;

    [SerializeField] private ContentSizeFitter _content;
    [SerializeField] private TextMeshProUGUI _textPrefab;

    public static void GetLog(string message)
    {
        if (s_Console == null)
            CreateSingleton();

        s_Console.Log(message);
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
            throw new System.NullReferenceException(nameof(_content));

        if (_textPrefab == null)
            _textPrefab = transform.GetComponentInChildren<TextMeshProUGUI>();

        if (_textPrefab == null)
            throw new System.NullReferenceException(nameof(_textPrefab));
    }

    private void Awake()
    {
        if (s_Console == null)
            s_Console = this;
    }

    private void Log(string message)
    {
        var text = Instantiate(_textPrefab, _content.transform);
        text.text = message;
    }
}
