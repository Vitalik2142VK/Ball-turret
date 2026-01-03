using System;

public interface IRocketView
{
    public event Action RocketFinished;

    public void Play();
}