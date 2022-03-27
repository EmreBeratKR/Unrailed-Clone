using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable : IGrid, ISelectable
{
    StackableType type { get; }
    Vector3 anchor { get; }
    bool isGrabbed { get; set; }
    IStackable upper { get; set; }
    IStackable lower { get; set; }
    IStackable Peek();
    void Clear();
    void Flip();
    void Reset();
    void SnapToStack(Vector3 position, Vector3 rotation);
}

public enum StackableType { RAIL, WOOD, ROCK }
