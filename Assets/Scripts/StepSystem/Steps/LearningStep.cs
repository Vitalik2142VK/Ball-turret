using System;

public class LearningStep : IStep, IEndPointStep
{
    private ILearningUI _learningUI;
    private ILevel _level;
    private IEndStep _endStep;
    private bool _isFinished;

    public LearningStep(ILearningUI learningUI, ILevel level)
    {
        _learningUI = learningUI ?? throw new ArgumentNullException(nameof(learningUI));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _isFinished = true;
    }

    public void Action()
    {
        if (_isFinished && _learningUI.IsFinished == false)
        {
            if (_level.CurrentWaveNumber == _learningUI.WaveNumberStage && _learningUI.IsProcess == false)
                _learningUI.ShowLearning(_level.CurrentWaveNumber);

            _isFinished = false;
        }

        if (_learningUI.IsProcess == false)
        {
            _isFinished = true;
            _endStep.End();
        }
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}