public interface IDebuffReceiver : IDebuffable
{
    public void RemoveCompletedDebuffs();

    public void Clean();
}