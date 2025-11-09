using System;

public class PrepareActorsStep : IStep
{
    private IActorsController _actorsController;
    private IDynamicEndStep _dynamicEndStep;
    private IStep _defaultNextStep;


    public PrepareActorsStep(IActorsController actorsController, IDynamicEndStep dynamicEndStep, IStep defaultNextStep)
    {
        _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
        _dynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
        _defaultNextStep = defaultNextStep ?? throw new ArgumentNullException(nameof(defaultNextStep));

        _dynamicEndStep.SetNextStep(_defaultNextStep);
    }

    public void Action()
    {
        _actorsController.Count();

        if (_actorsController.AreNoEnemies)
            _dynamicEndStep.SetNextStep(_defaultNextStep);

        _actorsController.Prepare();
        _dynamicEndStep.End();
    }
}
