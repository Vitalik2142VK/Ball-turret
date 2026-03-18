using CannonTurret.Actors.Spawn;
using System;

namespace CannonTurret.LevelSystem
{
    public class Level : ILevel
    {
        private const float DefaultHealthCoefficient = 1f;

        private readonly ILevelActorsPlanner ActorsPlanner;
        private readonly ICoinCountRandomizer CoinCountRandomizer;

        private int _passedWavesNumber;

        public Level(ILevelActorsPlanner actorsPlanner, ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficient = DefaultHealthCoefficient, int index = 0)
        {
            if (actorsHealthCoefficient < 0f)
                throw new ArgumentOutOfRangeException("The coefficient cannot be less than 0");

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            ActorsPlanner = actorsPlanner ?? throw new ArgumentNullException(nameof(actorsPlanner));
            CoinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
            _passedWavesNumber = 0;

            CurrentWaveNumber = 0;
            HealthCoefficient = actorsHealthCoefficient;
            Index = index;
        }

        public int WavesCount => ActorsPlanner.WavesCount;

        public int CountCoinsForWin => CoinCountRandomizer.GetCountCoinsForWin(Index);

        public int CountCoinsForWaves => CoinCountRandomizer.GetCountCoinsForWave(Index) * _passedWavesNumber;

        public bool AreWavesOver => ActorsPlanner.WavesCount <= CurrentWaveNumber;

        public float HealthCoefficient { get; }

        public int Index { get; }

        public int CurrentWaveNumber { get; private set; }

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
        {
            if (AreWavesOver == false)
            {
                waveActorsPlanner = ActorsPlanner.GetWaveActorsPlanner(++CurrentWaveNumber);
                _passedWavesNumber = CurrentWaveNumber - 1;

                return true;
            }
            else
            {
                waveActorsPlanner = null;
                _passedWavesNumber = ActorsPlanner.WavesCount;

                return false;
            }
        }

        public ILevel Clone()
        {
            return new Level(ActorsPlanner, CoinCountRandomizer, HealthCoefficient, Index);
        }
    }
}