using System;
using UnityEngine;

public class UILearningStage : MonoBehaviour, ILearningStage
{
    [SerializeField, SerializeIterface(typeof(ILearningStage))] private GameObject _learningStageGameObject;
    [SerializeField] private StageElements[] _stages;

    private ILearningStage _learningStage;
    private int _currentIndexStage;

    private void OnValidate()
    {
        if (_learningStageGameObject == null)
            throw new NullReferenceException(nameof(_learningStageGameObject));

        ILearningStage learningStage = _learningStageGameObject.GetComponent<ILearningStage>();

        if (_stages == null || _stages.Length != learningStage.NumberStages)
            _stages = new StageElements[learningStage.NumberStages];
    }

    public int NumberStages => _learningStage.NumberStages;
    public int CurrentStage => _learningStage.CurrentStage;
    public int WaveNumber => _learningStage.WaveNumber;
    public bool IsActive => _learningStage.IsActive;

    public void Initialize()
    {
        _learningStage = _learningStageGameObject.GetComponent<ILearningStage>();
        _learningStage.Initialize();
        _currentIndexStage = 0;

        foreach (var stage in _stages)
            stage.SetActiveElements(false);

        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);

        _learningStage.Enable();
        SetActiveStage(true);
    }

    public void HandleСlick()
    {
        _learningStage.HandleСlick();

        if (IsActive)
        {
            ChangeStage();
        }
        else
        {
            SetActiveStage(false);
            gameObject.SetActive(false);
        }
    }

    private void ChangeStage()
    {
        int nextIndexStage = CurrentStage - 1;

        if (_currentIndexStage == nextIndexStage)
            return;

        SetActiveStage(false);

        _currentIndexStage = nextIndexStage;

        SetActiveStage(true);
    }

    private void SetActiveStage(bool isAvtive)
    {
        if (CurrentStage > NumberStages)
            throw new InvalidOperationException($"{nameof(CurrentStage)} cannot be greater than {nameof(NumberStages)}");

        StageElements stage = _stages[_currentIndexStage];

        if (stage.IsEmpty == false)
            stage.SetActiveElements(isAvtive);
    }

    [Serializable]
    private class StageElements
    {
        [SerializeField] private RectTransform[] _elements;

        public bool IsEmpty => _elements.Length == 0;

        public void SetActiveElements(bool isActive)
        {
            foreach (var element in _elements)
                element.gameObject.SetActive(isActive);
        }
    }
}