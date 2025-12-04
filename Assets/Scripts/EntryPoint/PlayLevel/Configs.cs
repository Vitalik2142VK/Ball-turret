namespace PlayLevel
{
    public class Config
    {
        public Config(StepSystemConfigurator stepSystemConfigurator, ActorsConfigurator actorsConfigurator, UIConfigurator uiConfigurator, FinishWindowConfigurator finishWindowConfigurator, IWinStatus winStatus)
        {
            StepSystemConfigurator = stepSystemConfigurator;
            ActorsConfigurator = actorsConfigurator;
            UIConfigurator = uiConfigurator;
            FinishWindowConfigurator = finishWindowConfigurator;
            WinStatus = winStatus;
        }

        public StepSystemConfigurator StepSystemConfigurator { get; }
        public ActorsConfigurator ActorsConfigurator { get; }
        public UIConfigurator UIConfigurator { get; }
        public FinishWindowConfigurator FinishWindowConfigurator { get; }
        public IWinStatus WinStatus { get; }
    }
}
