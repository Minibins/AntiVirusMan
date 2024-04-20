using System;
using UnityEngine;

[Serializable]
public struct ValueBounds
{
    public float max, min;

    public ValueBounds(float max, float min)
    {
        this.max = max;
        this.min = min;
    }
}

public static class ValueBoundsExtentions
{
    public static float Clamp(this ValueBounds bounds, float Float) => Mathf.Clamp(Float, bounds.min, bounds.max);
    public static bool isInBounds(this ValueBounds bounds, float Float) => Float <= bounds.max && Float >= bounds.min;

    public static bool isInBounds(this ValueBounds bounds, float Float, bool countEquality) =>
        countEquality ? isInBounds(bounds, Float) : Float < bounds.max && Float > bounds.min;
}