using System.Collections;
using UnityEngine;

namespace RecorderLevel
{
    //[RequireComponent(typeof(RecordedTurretView))]
    public class ParticleStarter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _recordedTurretView;
        [SerializeField, Min(0f)] private float _timeWait = 0f;

        private void Start()
        {
            if (_timeWait != 0)
                StartCoroutine(WaitStart());
            else
                _recordedTurretView.Play();
        }

        private IEnumerator WaitStart()
        {
            yield return new WaitForSeconds(_timeWait);

            _recordedTurretView.Play();
        }
    }
}
