using System;

public class LearningStep : IStep, IEndPointStep
{
    private ILearningUI _learningUI;
    private ILevel _level;
    private IEndStep _endStep;

    public LearningStep(ILearningUI learningUI, ILevel level)
    {
        _learningUI = learningUI ?? throw new ArgumentNullException(nameof(learningUI));
        _level = level ?? throw new ArgumentNullException(nameof(level));
    }

    public void Action()
    {
        if (_level.CurrentWaveNumber == _learningUI.WaveNumberStage)
            _learningUI.ShowLearning(_level.CurrentWaveNumber);

        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}