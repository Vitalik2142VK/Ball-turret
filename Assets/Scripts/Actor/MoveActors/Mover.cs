using UnityEngine;

public class Mover : MonoBehaviour, IMover
{
    private Transform _transform;
    private Vector3 _point;
    private float _speed;

    public bool IsFinished { get; private set; }

    private void Awake()
    {
        _transform = transform;
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        float y = _transform.position.y;
        _transform.position = new Vector3(startPosition.x, y, startPosition.z);
    }

    public void SetParameters(Vector3 distance, float speed)
    {
        if (speed <= 0f)
            throw new System.ArgumentOutOfRangeException();

        _point = _transform.position + distance;
        _speed = speed;

        IsFinished = false;
    }

    public void Move()
    {
        if (IsFinished)
            return;

        if (VectorTools.AreVectorsClose(_transform.position, _point) == false)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _point, _speed * Time.deltaTime);
        }
        else
        {
            _transform.position = _point;

            IsFinished = true;
        }
    }
}
