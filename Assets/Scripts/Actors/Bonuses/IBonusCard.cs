using CannonTurret.SDK.Localizations;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses
{
    public interface IBonusCard
    {
        public Sprite Icon { get; }

        public string Name { get; }

        public string GetDescription(Language language);
    }
}