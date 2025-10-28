using UnityEngine;

public class DebuffAmplifier : MonoBehaviour, IDebuffHandler
{
    [SerializeField, SerializeIterface(typeof(IDebuffHandler))] private GameObject _debuffHandlerGameObject;
    [SerializeField, Min(1f)] private float _gainFactor;
    [SerializeField] private DebuffType _vulnerableDebuffType;

    private IDebuffHandler _debuffHandler;

    private void OnValidate()
    {
        if (_debuffHandlerGameObject == null)
            throw new System.NullReferenceException(nameof(_debuffHandlerGameObject));
    }

    private void Awake()
    {
        _debuffHandler = _debuffHandlerGameObject.GetComponent<DebuffHandler>();
    }

    public void AddDebuff(IDebuff debaff)
    {
        if (debaff.DebuffType == _vulnerableDebuffType)
            debaff.Strengthen(_gainFactor);

        _debuffHandler.AddDebuff(debaff);
    }

    public void ActivateDebuffs() => _debuffHandler.ActivateDebuffs();

    public void RemoveCompletedDebuffs() => _debuffHandler.RemoveCompletedDebuffs();

    public void Clean() => _debuffHandler.Clean();
}