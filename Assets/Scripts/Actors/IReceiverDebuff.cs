using CannonTurret.Effects;

namespace CannonTurret.Actors
{
    public interface IDebuffReceiver
    {
        public void AddDebuff(IDebuff debaff);
    }
}