using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Range(0.1f, 2f)] private float _additionalSitchingTime = 1f;

    private List<AudioClip> _musics;
    private AudioClip _currentClip;

    private void OnValidate()
    {
        if (_audioClips == null || _audioClips.Length == 0)
            throw new InvalidOperationException(nameof(_audioClips));

        foreach (var music in _audioClips)
            if (music == null)
                throw new NullReferenceException($"Null in the collection - {_audioClips}");

        if (_audioSource == null)
            throw new NullReferenceException(nameof(_audioSource));
    }

    private void Awake()
    {
        _musics = new List<AudioClip>(_audioClips);
        _audioSource.loop = false;
    }

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        while (gameObject.activeSelf)
        {
            ChangeRandomAudioClip();

            _audioSource.clip = _currentClip;
            _audioSource.Play();

            yield return new WaitForSeconds(_currentClip.length + _additionalSitchingTime); ;
        }
    }

    private void ChangeRandomAudioClip()
    {
        if (_musics.Count == 0)
            return;

        int randomIndex = UnityEngine.Random.Range(0, _musics.Count);
        AudioClip nextClip = _musics[randomIndex];
        _musics.RemoveAt(randomIndex);

        if (_currentClip != null)
            _musics.Add(_currentClip);

        _currentClip = nextClip;
    }
}
