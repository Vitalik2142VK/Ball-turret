using System;
using UnityEngine;

public class SerializeIterfaceAttribute : PropertyAttribute
{
    public Type Type { get; }

    public SerializeIterfaceAttribute(Type type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}
