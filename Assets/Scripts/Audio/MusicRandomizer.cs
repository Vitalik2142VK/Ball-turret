using System;
using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musics;
    [SerializeField] private AudioSource _groundMusic;

    private void OnValidate()
    {
        if (_musics == null || _musics.Length == 0)
            throw new InvalidOperationException(nameof(_musics));

        foreach (var music in _musics)
            if (music == null)
                throw new NullReferenceException($"Null in the collection - {_musics}");

        if (_groundMusic == null)
            throw new NullReferenceException(nameof(_groundMusic));
    }

    private void Start()
    {
        _groundMusic.clip = GetRandomAudioClip();
        _groundMusic.Play();
    }

    private AudioClip GetRandomAudioClip()
    {
        int randomIndex = UnityEngine.Random.Range(0, _musics.Length);

        return _musics[randomIndex];
    }
}
