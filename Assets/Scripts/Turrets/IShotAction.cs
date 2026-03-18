using System;

namespace CannonTurret.Turrets
{
    public interface IShotAction
    {
        public event Action Fired;
    }
}