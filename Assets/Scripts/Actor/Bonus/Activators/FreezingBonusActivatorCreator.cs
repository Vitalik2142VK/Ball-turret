using System;
using UnityEngine;

public class FreezingBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    [SerializeField] private FreezingView _freezingView;

    private IDynamicEndStep _nextStepPrepareActors;
    private ActorsFreezeStep _freezeStep;

    private void OnValidate()
    {
        if (_freezingView == null)
            throw new NullReferenceException(nameof(_freezingView));
    }

    public void Initialize(IDynamicEndStep nextStepPrepareActors, ActorsFreezeStep freezeStep)
    {
        _nextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
        _freezeStep = freezeStep ?? throw new NullReferenceException(nameof(freezeStep));
    }

    public IBonusActivator Create()
    {
        return new FreezingBonusActivator(_nextStepPrepareActors, _freezingView, _freezeStep);
    }
}
