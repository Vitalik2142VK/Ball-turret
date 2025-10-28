using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(ImageLoadYG))]
public class AuthPlayerView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;

    private ImageLoadYG _imageLoad;

    public bool IsAuthorized { get; private set; }

    private void OnValidate()
    {
        if (_icon == null)
            throw new NullReferenceException(nameof(_icon));

        if (_name == null)
            throw new NullReferenceException(nameof(_name));
    }

    private void Awake()
    {
        _imageLoad = GetComponent<ImageLoadYG>();

        IsAuthorized = false;
    }

    public void SetDataAuthPlayer(string urlIcon, string name)
    {
        if (urlIcon == null)
            throw new ArgumentNullException(nameof(urlIcon));

        _name.text = name ?? throw new ArgumentNullException(nameof(name));
        _imageLoad.spriteImage = _icon;
        _imageLoad.Load(urlIcon);

        IsAuthorized = true;
    }
}
