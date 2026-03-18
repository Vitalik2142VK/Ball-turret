using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CannonTurret.Clip
{
    [RequireComponent(typeof(PlayableDirector))]
    public class RecordedClip : MonoBehaviour
    {
        [SerializeField] private TimelineAsset[] _timelineAssets;
        [SerializeField][Min(1f)] private float _waitTime;

        private PlayableDirector _playableDirector;
        private WaitForSeconds _wait;

        private void OnValidate()
        {
            if (_timelineAssets == null || _timelineAssets.Length == 0)
                throw new InvalidOperationException(nameof(_timelineAssets));

            foreach (var timelineAsset in _timelineAssets)
                if (timelineAsset == null)
                    throw new NullReferenceException($"{_timelineAssets} contains null objects");
        }

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();

            _wait = new WaitForSeconds(_waitTime);
        }

        private void OnEnable()
        {
            _playableDirector.stopped += OnSelectRandomTimeline;
        }

        private void Start()
        {
            _playableDirector.Stop();
        }

        private void OnDisable()
        {
            _playableDirector.stopped -= OnSelectRandomTimeline;
        }

        private void OnSelectRandomTimeline(PlayableDirector playableDirector)
        {
            int randomIndex = UnityEngine.Random.Range(0, _timelineAssets.Length);
            playableDirector.playableAsset = _timelineAssets[randomIndex];

            if (gameObject.activeSelf)
                StartCoroutine(WaitSpawn());
        }

        private IEnumerator WaitSpawn()
        {
            yield return _wait;

            _playableDirector.Play();
        }
    }
}