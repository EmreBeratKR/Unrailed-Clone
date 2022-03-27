using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Vector3 Vector3QLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab, bc, t);
    }
}
