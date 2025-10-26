using System;
using UnityEngine;

public class UILearningStage : MonoBehaviour, ILearningStage
{
    [SerializeField, SerializeIterface(typeof(ILearningStage))] private GameObject _learningStageGameObject;
    [SerializeField] private StageElements[] _stageElements;

    private ILearningStage _learningStage;

    private void OnValidate()
    {
        if (_learningStageGameObject == null)
            throw new NullReferenceException(nameof(_learningStageGameObject));

        ILearningStage learningStage = _learningStageGameObject.GetComponent<ILearningStage>();
        _stageElements = new StageElements[learningStage.NumberStages];
    }

    private void Awake()
    {
        _learningStage = _learningStageGameObject.GetComponent<ILearningStage>();
    }

    public int NumberStages => _learningStage.NumberStages;
    public int CurrentStage => _learningStage.CurrentStage;
    public int WaveNumber => _learningStage.WaveNumber;
    public bool IsActive => _learningStage.IsActive;

    public void Enable()
    {
        gameObject.SetActive(true);

        _learningStage.Enable();
        ShowStageUI();
    }

    public void HandleСlick()
    {
        _learningStage.HandleСlick();
    }

    private void ShowStageUI()
    {
        int index = CurrentStage - 1;

        StageElements stage = _stageElements[index];

        if (stage.UIs.Length > 0)
            foreach (var ui in stage.UIs)
                ui.gameObject.SetActive(true);
    }

    [Serializable]
    private struct StageElements
    {
        public RectTransform[] UIs;
    }
}