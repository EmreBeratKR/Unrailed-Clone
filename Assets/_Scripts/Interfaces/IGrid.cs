using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    GVector2Int gridPosition { get; }
    Vector3 worldPosition { get; }
    Vector3 realPosition { get; }
    Vector3 realRotation { get; }
    void SnapToGrid(Vector3 position);
}
