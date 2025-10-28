using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DebuffHandler : MonoBehaviour, IDebuffHandler
{
    private List<IDebuff> _debuffs;
    private Dictionary<DebuffType, IViewDebuff> _viewDebuffs;

    private void Awake()
    {
        _debuffs = new List<IDebuff>(); 
        _viewDebuffs = new Dictionary<DebuffType, IViewDebuff>();
    }

    private void Start()
    {
        FillViewDebuffs();
    }

    public void AddDebuff(IDebuff debuff)
    {
        if (debuff == null)
            throw new System.ArgumentNullException(nameof(debuff));

        var debuffType = debuff.DebuffType;

        if (_viewDebuffs.ContainsKey(debuffType))
        {
            _debuffs.Add(debuff);

            ActivateViewDebuff(debuff.DebuffType);
        }
    }

    public void ActivateDebuffs()
    {
        for (int i = 0; i < _debuffs.Count; i++)
            _debuffs[i].Activate();
    }

    public void RemoveCompletedDebuffs()
    {
        for (int i = _debuffs.Count - 1; i >= 0; i--)
        {
            if (_debuffs[i].IsExecutionCompleted)
                _debuffs.RemoveAt(i);
        }

        DeactivateViewDebuff();
    }

    public void Clean()
    {
        _debuffs.Clear();

        foreach (var viewDebaff in _viewDebuffs.Values)
            viewDebaff.SetActive(false);
    }

    private void ActivateViewDebuff(DebuffType debuffType)
    {
        IViewDebuff viewDebaff = _viewDebuffs[debuffType];
        viewDebaff.SetActive(true);
    }

    private void DeactivateViewDebuff()
    {
        foreach (var viewDebaff in _viewDebuffs.Values)
            viewDebaff.SetActive(false);

        DebuffType[] debuffTypes = _debuffs.Select(d => d.DebuffType).ToArray();

        foreach (var debuffType in debuffTypes)
            ActivateViewDebuff(debuffType);
    }

    private void FillViewDebuffs()
    {
        IViewDebuff[] viewDebaffs = GetComponentsInChildren<IViewDebuff>();

        foreach (var viewDebaff in viewDebaffs)
        {
            viewDebaff.SetActive(false);
            _viewDebuffs.Add(viewDebaff.DebuffType, viewDebaff);
        }
    }
}
