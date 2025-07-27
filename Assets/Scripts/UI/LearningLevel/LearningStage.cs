using UnityEngine;
using UnityEngine.EventSystems;

public class LearningStage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Min(1)] private int _waveNumber;

    private Pause _pause;

    public int WaveNumber => _waveNumber;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(Pause pause)
    {
        if (pause == null) 
            throw new System.ArgumentNullException(nameof(pause));

        _pause = pause;
    }

    public void Enable()
    {
        _pause.Enable();
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData _)
    {
        _pause.Disable();
        gameObject.SetActive(false);
    }
}