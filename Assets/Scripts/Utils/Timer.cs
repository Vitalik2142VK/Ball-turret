using UnityEngine;

public class Timer
{
    private float _time;
    private float _waitingTime;

    public Timer(float waitTime)
    {
        _time = waitTime;
        _waitingTime = _time;
    }

    public bool IsTimeUp => _waitingTime <= 0;

    public void MakeCountdown(float deltaTime)
    {
        if (IsTimeUp == false)
            _waitingTime -= deltaTime;
    }

    public void UpdateWaitingTime()
    {
        _waitingTime = _time;
    }

    public void SetWaitTime(float waitTime)
    {
        _time = waitTime;
    }

    public void SetRandomWaitTime(float minTime, float maxTime)
    {
        _time = Random.Range(minTime, maxTime);
    }
}
