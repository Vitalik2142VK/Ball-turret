using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour, ISound
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        _audioSource.Play();
    }
}