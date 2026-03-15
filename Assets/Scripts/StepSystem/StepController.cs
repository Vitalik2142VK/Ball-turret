using CannonTurret.StepSystem.Steps;
using UnityEngine;

namespace CannonTurret.StepSystem
{
    public class StepController : MonoBehaviour, IStepController
    {
        private IStep _step;

        private void Update()
        {
            if (Time.timeScale != 0f)
                _step.Action();
        }

        public void EstablishNextStep(IStep step)
        {
            _step = step ?? throw new System.ArgumentNullException(nameof(step));
        }
    }
}