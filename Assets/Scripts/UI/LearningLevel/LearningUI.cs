using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LearningUI : MonoBehaviour, ILearningUI, IPointerClickHandler
{
    [SerializeField] private LearningStage[] _learningStages;

    private Queue<LearningStage> _stages;
    private LearningStage _currentStage;

    public int WaveNumberStage => _currentStage.WaveNumber;
    public bool IsProcess => gameObject.activeSelf;
    public bool IsFinished => _stages.Count == 0 && _currentStage == null;

    private void OnValidate()
    {
        if (_learningStages == null || _learningStages.Length == 0)
            throw new InvalidOperationException($"{nameof(_learningStages)} should not be empty");
    }

    private void Awake()
    {
        _stages = new Queue<LearningStage>(_learningStages);
        _currentStage = _stages.Dequeue();
        gameObject.SetActive(false);
    }

    public void ShowLearning(int waveNumber)
    {
        if (gameObject.activeSelf || _currentStage == null)
            return;

        if (waveNumber != WaveNumberStage)
            throw new InvalidOperationException(nameof(waveNumber));

        gameObject.SetActive(true);
        _currentStage.Enable();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _currentStage.HandleСlick();

        if (_currentStage.IsActive == false)
        {
            if (_stages.Count != 0)
                _currentStage = _stages.Dequeue();
            else
                _currentStage = null;

            gameObject.SetActive(false);
        }
    }
}
