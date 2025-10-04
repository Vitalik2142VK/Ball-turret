using UnityEngine;

public interface IUIAnimator
{
    public YieldInstruction PlayOpen();

    public YieldInstruction PlayClose();
}
