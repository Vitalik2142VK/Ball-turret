using CannonTurret.Actors.Spawn;
using System;

namespace CannonTurret.LevelSystem
{
    public class Level : ILevel
    {
        private const float DefaultHealthCoefficient = 1f;

        private readonly ILevelActorsPlanner _actorsPlanner;
        private readonly CoinCountRandomizer _coinCountRandomizer;

        private int _passedWavesNumber;

        public Level(
            ILevelActorsPlanner actorsPlanner,
            float actorsHealthCoefficient = DefaultHealthCoefficient,
            int index = 0)
        {
            if (actorsHealthCoefficient < 0f)
                throw new ArgumentOutOfRangeException("The coefficient cannot be less than 0");

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            _actorsPlanner = actorsPlanner ?? throw new ArgumentNullException(nameof(actorsPlanner));

            _coinCountRandomizer = new CoinCountRandomizer();
            _passedWavesNumber = 0;

            CurrentWaveNumber = 0;
            HealthCoefficient = actorsHealthCoefficient;
            Index = index;
        }

        public int WavesCount => _actorsPlanner.WavesCount;

        public int CountCoinsForWin => _coinCountRandomizer.GetCountCoinsForWin(Index);

        public int CountCoinsForWaves => _coinCountRandomizer.GetCountCoinsForWave(Index) * _passedWavesNumber;

        public bool AreWavesOver => _actorsPlanner.WavesCount <= CurrentWaveNumber;

        public float HealthCoefficient { get; }

        public int Index { get; }

        public int CurrentWaveNumber { get; private set; }

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
        {
            if (AreWavesOver == false)
            {
                waveActorsPlanner = _actorsPlanner.GetWaveActorsPlanner(++CurrentWaveNumber);
                _passedWavesNumber = CurrentWaveNumber - 1;

                return true;
            }
            else
            {
                waveActorsPlanner = null;
                _passedWavesNumber = _actorsPlanner.WavesCount;

                return false;
            }
        }

        public ILevel Clone()
        {
            return new Level(_actorsPlanner, HealthCoefficient, Index);
        }
    }
}