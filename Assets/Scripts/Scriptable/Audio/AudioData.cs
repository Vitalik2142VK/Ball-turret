using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Data/Audio", fileName = "AudioData", order = 51)]
    public class AudioData : ScriptableObject
    {
        public const string MasterVolumeGroupName = "MasterVolume";
        public const string MusicVolumeGroupName = "MusicVolume";
        public const string EffectsVolumeGroupName = "EffectsVolume";

        public const float MaxVolueMaster = -10f;
        public const float ValueOffVolume = -80f;

        private const string EnableSound = nameof(EnableSound);
        private const int SoundEnableValue = 1;
        private const int SoundDisableValue = 0;

        [SerializeField, Range(0, MaxVolueMaster)] private float _maxVolume = 0f;
        [SerializeField, Range(MaxVolueMaster, ValueOffVolume)] private float _minVolume = -50f;

        private bool _wasChanges;

        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }
        public bool IsEnableSound { get; private set; }

        public float MaxVolume => _maxVolume;
        public float MinVolume => _minVolume;

        public void Load()
        {
            MusicVolume = PlayerPrefs.GetFloat(MusicVolumeGroupName, _maxVolume);
            EffectsVolume = PlayerPrefs.GetFloat(EffectsVolumeGroupName, _maxVolume);
            IsEnableSound = PlayerPrefs.GetInt(EnableSound, SoundEnableValue) == SoundEnableValue;

            _wasChanges = false;
        }

        public void Save()
        {
            if (_wasChanges == false)
                return;

            PlayerPrefs.SetFloat(MusicVolumeGroupName, MusicVolume);
            PlayerPrefs.SetFloat(EffectsVolumeGroupName, EffectsVolume);
            PlayerPrefs.SetInt(EnableSound, IsEnableSound ? SoundEnableValue : SoundDisableValue);
            PlayerPrefs.Save();
        }

        public void SetMusicVolume(float volume)
        {
            _wasChanges = true;

            MusicVolume = volume;
        }

        public void SetEffectsVolume(float volume)
        {
            _wasChanges = true;

            EffectsVolume = volume;
        }

        public void SetEnableSound(bool isEnable)
        {
            _wasChanges = true;

            IsEnableSound = isEnable;
        }
    }
}
