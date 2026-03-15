using CannonTurret.StepSystem.Steps;
using CannonTurret.UI;

namespace CannonTurret.StepSystem
{
    public interface IChangeSceneStep : IStep
    {
        public void SetSceneLoader(ISceneLoader sceneLoader);
    }
}