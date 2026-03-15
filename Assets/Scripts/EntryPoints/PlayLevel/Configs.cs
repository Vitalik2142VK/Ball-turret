using CannonTurret.EntryPoints.PlayLevel.Configurators;
using CannonTurret.LevelSystem;

namespace CannonTurret.EntryPoints.PlayLevel
{
    public class Config
    {
        public Config(StepControllerConfigurator stepControllerConfigurator, ActorsConfigurator actorsConfigurator, UIConfigurator uiConfigurator, FinishWindowConfigurator finishWindowConfigurator, IWinStatus winStatus)
        {
            StepSystemConfigurator = stepControllerConfigurator;
            ActorsConfigurator = actorsConfigurator;
            UIConfigurator = uiConfigurator;
            FinishWindowConfigurator = finishWindowConfigurator;
            WinStatus = winStatus;
        }

        public StepControllerConfigurator StepSystemConfigurator { get; }
        public ActorsConfigurator ActorsConfigurator { get; }
        public UIConfigurator UIConfigurator { get; }
        public FinishWindowConfigurator FinishWindowConfigurator { get; }
        public IWinStatus WinStatus { get; }
    }
}
