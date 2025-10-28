using System.Collections.Generic;
using UnityEngine;

public class HiderUI : MonoBehaviour
{
    [SerializeField, SerializeIterface(typeof(IAnimatorUI))] private GameObject[] _interferingUI;

    private Dictionary<GameObject, IAnimatorUI> _animators;


    private void Awake()
    {
        _animators = new Dictionary<GameObject, IAnimatorUI>();

        foreach (var animator in _interferingUI)
            if (animator != null)
                _animators.Add(animator, animator.GetComponent<IAnimatorUI>());
    }

    public void Enable()
    {
        foreach (var animator in _animators)
            if (animator.Key != null)
                animator.Value.Show();
    }

    public void Disable()
    {
        foreach (var animator in _animators)
            if (animator.Key != null)
                animator.Value.Hide();
    }
}
