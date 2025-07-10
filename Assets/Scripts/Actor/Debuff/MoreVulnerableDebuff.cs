using UnityEngine;

public class MoreVulnerableDebuff : MonoBehaviour, IDebuffReceiver
{
    [SerializeField, SerializeIterface(typeof(IDebuffReceiver))] private GameObject _debuffReceiverGameObject;
    [SerializeField, Min(1f)] private float _gainFactor;
    [SerializeField] private DebuffType _vulnerableDebuffType;

    private IDebuffReceiver _debuffReceiver;

    private void OnValidate()
    {
        if (_debuffReceiverGameObject == null)
            throw new System.NullReferenceException(nameof(_debuffReceiverGameObject));
    }

    private void Awake()
    {
        _debuffReceiver = _debuffReceiverGameObject.GetComponent<DebuffReceiver>();
    }

    public void AddDebuff(IDebuff debaff)
    {
        if (debaff.DebuffType == _vulnerableDebuffType)
            debaff.Strengthen(_gainFactor);

        _debuffReceiver.AddDebuff(debaff);
    }

    public void ActivateDebuffs() => _debuffReceiver.ActivateDebuffs();

    public void RemoveCompletedDebuffs() => _debuffReceiver.RemoveCompletedDebuffs();

    public void Clean() => _debuffReceiver.Clean();
}