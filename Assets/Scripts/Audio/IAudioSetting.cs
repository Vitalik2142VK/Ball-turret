public interface IAudioSetting
{
    public float MusicVolumeCoefficient { get; }
    public float EffectsVolumeCoefficient { get; }
    public bool IsEnableSound {  get; }

    public void ChangeVolumeEffects(float valueCoefficient);

    public void ChangeVolumeMusic(float valueCoefficient);

    public void ChangeEnableSound(bool isEnable);

    public void AcceptChanges();
}