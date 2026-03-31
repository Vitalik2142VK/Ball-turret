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

        private readonly ILevel _endlesslevel;
        private readonly ISavedLeaderBoard _savedLeaderBoard;
        private readonly float _healthMultiplierPerWave;
        private readonly float _countCoinsMultiplier;
        private readonly int _waveNumberReward;

        public EndlessLevel(ILevel endlesslevel, ISavedLeaderBoard savedLeaderBoard, float healthMultiplierPerWave)
        {
            if (healthMultiplierPerWave < MinHealthMultiplierPerWave)
                throw new ArgumentOutOfRangeException($"{nameof(healthMultiplierPerWave)} cannot be less than {MinHealthMultiplierPerWave}");

            _endlesslevel = endlesslevel ?? throw new ArgumentNullException(nameof(endlesslevel));
            _savedLeaderBoard = savedLeaderBoard ?? throw new ArgumentNullException(nameof(savedLeaderBoard));
            _healthMultiplierPerWave = healthMultiplierPerWave;
            _countCoinsMultiplier = DefaultCoefficient + healthMultiplierPerWave * ReducingCoefficientCoins;
            _waveNumberReward = WaveRepository.WaveDivider;

            CountCoinsForWin = 0;
        }

        public int CountCoinsForWin { get; private set; }

        public float HealthCoefficient => DefaultCoefficient + _healthMultiplierPerWave * CurrentWaveNumber;

        public int Index => IndexLevel;

        public int CurrentWaveNumber => _endlesslevel.CurrentWaveNumber;

        public int WavesCount => _endlesslevel.WavesCount;

        public int CountCoinsForWaves => (int)(AddedCoins * _countCoinsMultiplier);

        public bool AreWavesOver => _endlesslevel.AreWavesOver;

        private int AddedCoins => _endlesslevel.CountCoinsForWaves + CountCoinsForWin;

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
        {
            if (CurrentWaveNumber != 0 && CurrentWaveNumber % _waveNumberReward == 0)
                CountCoinsForWin += (int)(_endlesslevel.CountCoinsForWin * _countCoinsMultiplier);

            if (CurrentWaveNumber > _savedLeaderBoard.MaxAchievedWave)
                _savedLeaderBoard.SaveNextAchievedWave();

            return _endlesslevel.TryGetNextWaveActorsPlanner(out waveActorsPlanner);
        }

        public ILevel Clone()
        {
            return new EndlessLevel(_endlesslevel, _savedLeaderBoard, _healthMultiplierPerWave);
        }
    }
}