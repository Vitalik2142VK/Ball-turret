using DG.Tweening;
using System;
using UnityEngine;

public class TweenController
{
    private Tween _animation;

    public void PlayAnimation(Tween tween)
    {
        _animation = tween ?? throw new ArgumentNullException(nameof(tween));
        _animation.Play();
    }

    public YieldInstruction GetYieldAnimation()
    {
        if (_animation == null || _animation.active == false)
            throw new InvalidOperationException("Animation was not launched");

        return _animation.WaitForCompletion();
    }

    public void KillCurrentAnimation()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
}