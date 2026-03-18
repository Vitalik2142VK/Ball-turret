using CannonTurret.Actors.Spawn;
using CannonTurret.SDK.LeaderBoards;
using System;

namespace CannonTurret.LevelSystem
{
    public class EndlessLevel : ILevel
    {
        public const int IndexLevel = 0;

        private const float DefaultCoefficient = 1f;
        private const float MinHealthMultiplierPerWave = 0.1f;
        private const float ReducingCoefficientCoins = 1.25f;

        private readonly ILevel Endlesslevel;
        private readonly ISavedLeaderBoard SavedLeaderBoard;
        private readonly float HealthMultiplierPerWave;
        private readonly float CountCoinsMultiplier;
        private readonly int WaveNumberReward;

        public EndlessLevel(ILevel endlesslevel, ISavedLeaderBoard savedLeaderBoard, float healthMultiplierPerWave)
        {
            if (healthMultiplierPerWave < MinHealthMultiplierPerWave)
                throw new ArgumentOutOfRangeException($"{nameof(healthMultiplierPerWave)} cannot be less than {MinHealthMultiplierPerWave}");

            Endlesslevel = endlesslevel ?? throw new ArgumentNullException(nameof(endlesslevel));
            SavedLeaderBoard = savedLeaderBoard ?? throw new ArgumentNullException(nameof(savedLeaderBoard));
            HealthMultiplierPerWave = healthMultiplierPerWave;
            CountCoinsMultiplier = DefaultCoefficient + healthMultiplierPerWave * ReducingCoefficientCoins;
            WaveNumberReward = WaveRepository.WaveDivider;

            CountCoinsForWin = 0;
        }

        public int CountCoinsForWin { get; private set; }

        public float HealthCoefficient => DefaultCoefficient + HealthMultiplierPerWave * CurrentWaveNumber;

        public int Index => IndexLevel;

        public int CurrentWaveNumber => Endlesslevel.CurrentWaveNumber;

        public int WavesCount => Endlesslevel.WavesCount;

        public int CountCoinsForWaves => (int)((Endlesslevel.CountCoinsForWaves + CountCoinsForWin) * CountCoinsMultiplier);

        public bool AreWavesOver => Endlesslevel.AreWavesOver;

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
        {
            if (CurrentWaveNumber != 0 && CurrentWaveNumber % WaveNumberReward == 0)
                CountCoinsForWin += (int)(Endlesslevel.CountCoinsForWin * CountCoinsMultiplier);

            if (CurrentWaveNumber > SavedLeaderBoard.MaxAchievedWave)
                SavedLeaderBoard.SaveNextAchievedWave();

            return Endlesslevel.TryGetNextWaveActorsPlanner(out waveActorsPlanner);
        }

        public ILevel Clone()
        {
            return new EndlessLevel(Endlesslevel, SavedLeaderBoard, HealthMultiplierPerWave);
        }
    }
}