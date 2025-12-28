using UnityEngine;

namespace RecorderLevel
{
    public class RecordingTurretView : MonoBehaviour, ITurretView
    {
        public void PlayShoot() => Debug.Log($"{nameof(RecordingTurretView)}Shot");

        public void PlayDestroy() => Debug.Log($"{nameof(RecordingTurretView)}PlayDestroy");

        public void PlayTakeDamage() => Debug.Log($"{nameof(RecordingTurretView)}PlayTakeDamage");
    }
}
