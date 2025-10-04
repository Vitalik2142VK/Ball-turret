using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MenuAnimator))]
public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider _volumeSound;
    [SerializeField] private Slider _volumeMusic;
    [SerializeField] private Toggle _isEnableSound;

    private IMenu _previousMenu;
    private IAudioSetting _audioSetting;
    private IUIAnimator _animator;

    private void OnValidate()
    {
        if (_volumeSound == null)
            throw new NullReferenceException(nameof(_volumeSound));

        if (_volumeMusic == null)
            throw new NullReferenceException(nameof(_volumeMusic));

        if (_isEnableSound == null)
            throw new NullReferenceException(nameof(_isEnableSound));
    }

    private void Awake()
    {
        _animator = GetComponent<MenuAnimator>();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _volumeSound.onValueChanged.AddListener(OnChangeVolumeEffects);
        _volumeMusic.onValueChanged.AddListener(OnChangeVolumeMusic);
        _isEnableSound.onValueChanged.AddListener(OnEnableSound);
    }

    private void Start()
    {
        _volumeSound.value = _audioSetting.EffectsVolumeCoefficient;
        _volumeMusic.value = _audioSetting.MusicVolumeCoefficient;
        _isEnableSound.isOn = _audioSetting.IsEnableSound;
    }

    private void OnDisable()
    {
        _volumeSound.onValueChanged.RemoveListener(OnChangeVolumeEffects);
        _volumeMusic.onValueChanged.RemoveListener(OnChangeVolumeMusic);
        _isEnableSound.onValueChanged.RemoveListener(OnEnableSound);
    }

    public void Initialize(IAudioSetting audioSetting)
    {
        _audioSetting = audioSetting ?? throw new ArgumentNullException(nameof(audioSetting));
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));

        gameObject.SetActive(true);
        _animator.PlayOpen();
    }

    public void OnClose()
    {
        _audioSetting.AcceptChanges();

        StartCoroutine(WaitClosure());
    }

    private void OnChangeVolumeEffects(float value) => _audioSetting.ChangeVolumeEffects(value);

    private void OnChangeVolumeMusic(float value) => _audioSetting.ChangeVolumeMusic(value);

    private void OnEnableSound(bool isEnable) => _audioSetting.ChangeEnableSound(isEnable);

    private IEnumerator WaitClosure()
    {
        yield return _animator.PlayClose();

        gameObject.SetActive(false);
        _previousMenu.Enable();
    }
}
