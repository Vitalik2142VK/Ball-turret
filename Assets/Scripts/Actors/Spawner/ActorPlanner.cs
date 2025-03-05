using UnityEngine;

[System.Serializable]
public class ActorPlanner : IActorPlanner
{
    private const int MinSizeLine = 1;
    private const int MaxSizeLine = 3;
    private const int MinSizeColumn = 1;
    private const int MaxSizeColumn = 3;

    [SerializeField, SerializeIterface(typeof(IActor))] private GameObject _actor;
    [SerializeField, Range(MinSizeLine, MaxSizeLine)] private int _lineNumber;
    [SerializeField, Range(MinSizeColumn, MaxSizeColumn)] private int _columnNumber;

    public string NameActor => _actor.GetComponent<IActor>().Name;
    public int LineNumber => _lineNumber - 1;
    public int ColumnNumber => _columnNumber - 1;
}
