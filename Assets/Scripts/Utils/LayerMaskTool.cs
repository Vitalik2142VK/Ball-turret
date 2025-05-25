using UnityEngine;

public static class LayerMaskTool
{
    public static bool IsInLayerMask(GameObject gameObject, LayerMask layerMask)
    {
        return (1 << gameObject.layer & layerMask) != 0;
    }
}