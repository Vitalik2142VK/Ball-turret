using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Move attributes", fileName = "MoveAttributes", order = 51)]
    public class MoveAttributes : ScriptableObject, IMoveAttributes
    {
        [SerializeField] private Vector3 _distance;
        [SerializeField] private float _speed;

        Vector3 IMoveAttributes.Distance => _distance;
        float IMoveAttributes.Speed => _speed;
    }
}
