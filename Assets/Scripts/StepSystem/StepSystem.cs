using UnityEngine;

public class StepSystem : MonoBehaviour, IStepSystem
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
