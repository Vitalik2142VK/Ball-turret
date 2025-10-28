using System.Collections;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    [SerializeField] private Sound _sound;
    [SerializeField, Min(0)] private float _timeLoop;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeLoop);
    }

    private void Start()
    {
        StartCoroutine(PlayTestSound());
    }

    private IEnumerator PlayTestSound()
    {
        while (gameObject.activeSelf)
        {
            _sound.Play();

            yield return _wait;
        }
    }
}