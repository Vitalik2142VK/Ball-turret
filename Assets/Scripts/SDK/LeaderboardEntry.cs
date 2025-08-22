using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(LBPlayerDataYG), typeof(Image))]
public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private Image _playerImage;
    [SerializeField] private ImageLoadYG _playerImageLoad;
    [SerializeField] private Color _inTopColor;
    [SerializeField] private Color _currentPlayerColor;

    private LBPlayerDataYG _playerData;
    private Image _image;
    private Color _defaultColor;

    private void OnValidate()
    {
        if (_playerImage == null)
            throw new System.NullReferenceException(nameof(_playerImage));

        if (_playerImageLoad == null)
            throw new System.NullReferenceException(nameof(_playerImageLoad));
    }

    private void Awake()
    {
        _playerData = GetComponent<LBPlayerDataYG>();
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
    }

    private void Start()
    {
        ChangeColor();
        LoadImage();
    }

    private void ChangeColor()
    {
        var data = _playerData.data;

        if (data.inTop)
            _image.color = _inTopColor;
        else
            _image.color = _defaultColor;

        if (data.currentPlayer)
            _image.color = _currentPlayerColor;
    }

    private void LoadImage()
    {
        string photoUrl = _playerData.data.photoUrl;

        if (string.IsNullOrEmpty(photoUrl) == false)
            _playerImageLoad.Load(photoUrl);
        else
            _playerImage.enabled = true;
    }
}
