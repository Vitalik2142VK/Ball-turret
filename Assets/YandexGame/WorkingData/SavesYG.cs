using System;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения
        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны

        public float HealthCoefficient = 1f;
        public float DamageCoefficient = 1f;
        public int CountCoins = 1000;
        public int AchievedLevelIndex = 0;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG() {}
    }
}
