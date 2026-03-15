public interface IViewDebuff
{
    public DebuffType DebuffType { get; }

    public void SetActive(bool isActive);
}
