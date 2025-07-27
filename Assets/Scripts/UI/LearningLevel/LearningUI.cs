using System;
using System.Collections.Generic;
using UnityEngine;

public class LearningUI : MonoBehaviour, ILearningUI
{
    [SerializeField] private LearningStage[] _learningStages;

    private Queue<LearningStage> _stages;
    private LearningStage _currentStage;

    public int WaveNumberStage => _currentStage.WaveNumber;

    private void OnValidate()
    {
        if (_learningStages == null || _learningStages.Length == 0)
            throw new InvalidOperationException($"{nameof(_learningStages)} should not be empty");
    }

    private void Awake()
    {
        _stages = new Queue<LearningStage>(_learningStages);
        _currentStage = _stages.Dequeue();
    }

    public void Initialize(Pause pause)
    {
        if (pause == null)
            throw new ArgumentNullException(nameof(pause));

        foreach (var learningStage in _learningStages)
            learningStage.Initialize(pause);
    }

    public void ShowLearning(int waveNumber)
    {
        if (waveNumber != WaveNumberStage)
            throw new InvalidOperationException(nameof(waveNumber));

        _currentStage.Enable();

        if (_stages.Count != 0)
            _currentStage = _stages.Dequeue();
        else
            _currentStage = null;
    }
}
