using System;
using UnityEngine;

public static class MathTool
{
    public static int GetStepIndex(float value, float minValue, float maxValue, float step)
    {
        if (value < minValue)
            throw new ArgumentOutOfRangeException(nameof(value));

        int index = Mathf.RoundToInt((value - minValue) / step);
        int maxIndex = Mathf.RoundToInt((maxValue - minValue) / step);

        return Mathf.Clamp(index, 0, maxIndex);
    }

    public static float Pow(float value, int exponent)
    {
        if (exponent < 0)
            throw new ArgumentOutOfRangeException(nameof(exponent));

        float result = 1f;

        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
                result *= value;

            value *= value;
            exponent >>= 1;
        }

        return result;
    }
}
