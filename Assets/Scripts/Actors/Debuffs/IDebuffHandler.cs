namespace CannonTurret.Actors.Debuffs
{
    public interface IDebuffHandler : IDebuffable
    {
        public void RemoveCompletedDebuffs();

        public void Clean();
    }
}