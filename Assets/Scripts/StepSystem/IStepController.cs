using CannonTurret.StepSystem.Steps;

namespace CannonTurret.StepSystem
{
    public interface IStepController
    {
        public void EstablishNextStep(IStep step);
    }
}