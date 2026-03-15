using UnityEngine;

public interface IBonusCard
{
    public Sprite Icon { get; }
    public string Name { get; }

    public string GetDescription(Language language);
}