public interface IActorFactory
{
    public bool IsCanCreate(string nameTypeActor);

    public IActor Create(string nameTypeActor);
}
