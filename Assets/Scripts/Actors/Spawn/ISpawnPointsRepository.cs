using UnityEngine;

namespace CannonTurret.Actors.Spawn
{
    public interface ISpawnPointsRepository
    {
        public Vector3 GetPositionSpawnPoint(int columnNum, int lineNum);

        public void FreeAllSpawnPoints();
    }
}