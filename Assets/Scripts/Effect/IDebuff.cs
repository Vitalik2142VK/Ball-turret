﻿
public interface IDebuff
{
    public DebuffType DebuffType { get; }
    public bool IsExecutionCompleted { get; }

    public void Activate();
}
