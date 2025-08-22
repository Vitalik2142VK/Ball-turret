using UnityEngine;

public class HiderUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _interferingUI;

    public void Enable()
    {
        foreach (var ui in _interferingUI)
            if (ui != null)
                ui.SetActive(true);
    }

    public void Disable()
    {
        foreach (var ui in _interferingUI)
            if (ui != null)
                ui.SetActive(false);
    }
}