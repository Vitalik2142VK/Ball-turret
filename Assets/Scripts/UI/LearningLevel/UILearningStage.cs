using System;
using UnityEngine;

public class UILearningStage : MonoBehaviour, ILearningStage
{
    [SerializeField, SerializeIterface(typeof(ILearningStage))] private GameObject _learningStageGameObject;
    [SerializeField] private StageElements[] _stages;

    private ILearningStage _learningStage;

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
        SetActiveStage(false);

        _learningStage.HandleСlick();

        if (IsActive)
            SetActiveStage(true);
        else
            gameObject.SetActive(false);
    }

    private void SetActiveStage(bool isAvtive)
    {
        if (CurrentStage > NumberStages)
            throw new InvalidOperationException($"{nameof(CurrentStage)} cannot be greater than {nameof(NumberStages)}");

        int index = CurrentStage - 1;

        StageElements stage = _stages[index];

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