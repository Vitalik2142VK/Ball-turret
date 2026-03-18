namespace CannonTurret.Actors.Spawn
{
    public interface IActorPlanner
    {
        public string NameActor { get; }

        public int LineNumber { get; }

        public int ColumnNumber { get; }
    }
}