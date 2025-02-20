using System;

public class PrepareActorsStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IActorsController _dynamicObjectsController;

    public PrepareActorsStep(IActorsController dynamicObjectsController)
    {
        _dynamicObjectsController = dynamicObjectsController ?? throw new ArgumentNullException(nameof(dynamicObjectsController));
    }

    public void Action()
    {
        _dynamicObjectsController.PrepareActors();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}