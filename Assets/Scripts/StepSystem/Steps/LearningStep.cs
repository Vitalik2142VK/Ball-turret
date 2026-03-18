using CannonTurret.LevelSystem;
using CannonTurret.UI.LearningLevel;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class LearningStep : IStep, IEndPointStep
    {
        private readonly ILearningUI LearningUI;
        private readonly ILevel Level;

        private IEndStep _endStep;
        private bool _isFinished;

        public LearningStep(ILearningUI learningUI, ILevel level)
        {
            LearningUI = learningUI ?? throw new ArgumentNullException(nameof(learningUI));
            Level = level ?? throw new ArgumentNullException(nameof(level));
            _isFinished = true;
        }

        public void Action()
        {
            if (_isFinished && LearningUI.IsFinished == false)
            {
                if (Level.CurrentWaveNumber == LearningUI.WaveNumberStage && LearningUI.IsProcess == false)
                    LearningUI.ShowLearning(Level.CurrentWaveNumber);

                _isFinished = false;
            }

            if (LearningUI.IsProcess == false)
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
}