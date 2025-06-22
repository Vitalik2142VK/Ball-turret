using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _timeScale;

    private void Start()
    {
#if UNITY_EDITOR
        Time.timeScale = _timeScale;
#endif
    }

    [ContextMenu("Set time scale")]
    private void SetTimeScale()
    {
#if UNITY_EDITOR
        Time.timeScale = _timeScale;
#endif
    }
}