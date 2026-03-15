using CannonTurret.StepSystem.Steps;

namespace CannonTurret.StepSystem
{
    public interface IDynamicEndStep : IEndStep
    {
        public void SetNextStep(IStep step);
    }
}