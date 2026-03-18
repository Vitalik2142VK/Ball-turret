using System;

namespace CannonTurret.Effects
{
    public interface IRocketView
    {
        public event Action RocketFinished;

        public void Play();
    }
}