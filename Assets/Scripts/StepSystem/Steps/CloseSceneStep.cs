using UnityEngine;

public class CloseSceneStep : IStep
{
    public void Action()
    {
        Debug.Log("Scene change");
    }
}