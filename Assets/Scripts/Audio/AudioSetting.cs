using Scriptable;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSetting : MonoBehaviour, IAudioSetting
{
    [SerializeField] private AudioData _audioData;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Sound _soundEffectExample;

    public float MusicVolumeCoefficient { get; private set; }
    public float EffectsVolumeCoefficient { get; private set; }

    public bool IsEnableSound => _audioData.IsEnableSound;

    private void OnValidate()
    {
        if (_audioData == null)
            throw new System.NullReferenceException(nameof(_audioData));

        if (_audioMixerGroup == null)
            throw new System.NullReferenceException(nameof(_audioMixerGroup));

        if (_soundEffectExample == null)
            throw new System.NullReferenceException(nameof(_soundEffectExample));
    }

    private void Awake()
    {
        _audioData.Load();

        ChangeMasterValue(_audioData.IsEnableSound);

        _audioMixerGroup.audioMixer.SetFloat(AudioData.MusicVolumeGroupName, _audioData.MusicVolume);
        _audioMixerGroup.audioMixer.SetFloat(AudioData.EffectsVolumeGroupName, _audioData.EffectsVolume);

        MusicVolumeCoefficient = CalsulateVolumeCoefficient(_audioData.MusicVolume);
        EffectsVolumeCoefficient = CalsulateVolumeCoefficient(_audioData.EffectsVolume);
    }

    public void ChangeVolumeMusic(float value)
    {
        float volume = CalsulateVolume(value);
        MusicVolumeCoefficient = volume;

        _audioMixerGroup.audioMixer.SetFloat(AudioData.MusicVolumeGroupName, volume);
        _audioData.SetMusicVolume(volume);
    }

    public void ChangeVolumeEffects(float valueCoefficient)
    {
        float volume = CalsulateVolume(valueCoefficient);
        EffectsVolumeCoefficient = volume;

        _audioMixerGroup.audioMixer.SetFloat(AudioData.EffectsVolumeGroupName, volume);
        _audioData.SetEffectsVolume(volume);
        _soundEffectExample.Play();
    }

    public void ChangeEnableSound(bool valueCoefficient)
    {
        ChangeMasterValue(valueCoefficient);

        _audioData.SetEnableSound(valueCoefficient);
    }

    public void AcceptChanges()
    {
        _audioData.Save();
    }

    private float CalsulateVolume(float valueCoefficient)
    {
        valueCoefficient = Mathf.Clamp01(valueCoefficient);
        float volume = Mathf.Lerp(_audioData.MinVolume, _audioData.MaxVolume, valueCoefficient);

        if (volume <= _audioData.MinVolume)
            volume = AudioData.ValueOffVolume;

        return volume;
    }

    private float CalsulateVolumeCoefficient(float value)
    {
        float volumeCoefficient;

        if (value <= _audioData.MinVolume)
            volumeCoefficient = 0f;
        else
            volumeCoefficient = Mathf.InverseLerp(_audioData.MinVolume, _audioData.MaxVolume, value);

        return volumeCoefficient;
    }

    private void ChangeMasterValue(bool isEnable)
    {
        float volume;

        if (isEnable)
            volume = AudioData.MaxVolueMaster;
        else
            volume = AudioData.ValueOffVolume;

        _audioMixerGroup.audioMixer.SetFloat(AudioData.MasterVolumeGroupName, volume);
    }
}
