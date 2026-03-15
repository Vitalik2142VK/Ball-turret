using System;

public interface IImprovementShop
{
    public bool TryMakeTransaction(IGamePayTransaction transaction);

    public IGamePayTransaction GetTransaction(Type type);

    public IImprovementProduct GetProduct(Type type);
}