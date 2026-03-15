using UnityEngine;

public interface IAnimatorUI
{
    public void Show();

    public void Hide();

    public YieldInstruction GetYieldAnimation();
}