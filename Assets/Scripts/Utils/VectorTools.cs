using UnityEngine;

public static class VectorTools
{
    public static bool AreVectorsClose(Vector3 a, Vector3 b, float distance = 0.01f)
    {
        return Vector3.Distance(a, b) < distance;
    }
}
