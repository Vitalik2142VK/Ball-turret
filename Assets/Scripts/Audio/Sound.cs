using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour, ISound
{
    [SerializeField, Range(0, 0.3f)] private float _rangePitch = 0f;
    [SerializeField, Min(0)] private float _duration = 0f;

    private AudioSource _audioSource;
    private float _pitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _pitch = _audioSource.pitch;
    }

    public void Play()
    {
        if (_rangePitch != 0)
            _audioSource.pitch = _pitch + GetRandomPitch();

        if (_duration != 0)
            Invoke(nameof(StopSound), _duration);

        _audioSource.Play();
    }

    private float GetRandomPitch()
    {
        return Random.Range(-_rangePitch, _rangePitch);
    }

    private void StopSound()
    {
        _audioSource.Stop();
    }
}
