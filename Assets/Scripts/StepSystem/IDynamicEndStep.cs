public interface IDynamicEndStep : IEndStep
{
    public void SetNextStep(IStep step);
}