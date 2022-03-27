using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    private static Vector3 origin;
    public const int sizeX = 4;
    public const int sizeZ = 4;


    private void Awake()
    {
        origin = transform.position;
    }

    
    public static GVector2Int GetGridPosition(Vector3 position)
    {
        return new GVector2Int(
            Mathf.FloorToInt((position.x - origin.x) / sizeX),
            Mathf.FloorToInt((position.z - origin.z) / sizeZ)
        );
    }

    public static Vector3 GetWorldPosition(GVector2Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * sizeX + origin.x + sizeX * 0.5f,
            origin.y,
            gridPosition.z * sizeZ + origin.z + sizeZ * 0.5f
        );
    }
}

[System.Serializable]
public struct GVector2Int
{
    public int x;
    public int z;

    public GVector2Int(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static GVector2Int operator +(GVector2Int a, GVector2Int b)
    {
        return new GVector2Int(a.x + b.x, a.z + b.z);
    }
    public static GVector2Int operator -(GVector2Int a, GVector2Int b)
    {
        return new GVector2Int(a.x - b.x, a.z - b.z);
    }
    public static GVector2Int operator *(GVector2Int a, int b)
    {
        return new GVector2Int(a.x * b, a.z * b);
    }
    public static GVector2Int operator /(GVector2Int a, int b)
    {
        return new GVector2Int(a.x / b, a.z / b);
    }

    public static bool operator ==(GVector2Int a, GVector2Int b)
    {
        return a.x == b.x && a.z == b.z;
    }
    public static bool operator !=(GVector2Int a, GVector2Int b)
    {
        return a.x != b.x || a.z != b.z;
    }

    public override bool Equals(object obj)
    {
        return obj is GVector2Int && this == (GVector2Int)obj;
    }
    public override int GetHashCode()
    {
        return x.GetHashCode() ^ z.GetHashCode();
    }

    public override string ToString()
    {
        return $"({x},{z})";
    }

    public static GVector2Int zero { get; } = new GVector2Int(0, 0);
    public static GVector2Int one { get; } = new GVector2Int(1, 1);
    public static GVector2Int up { get; } = new GVector2Int(0, 1);
    public static GVector2Int down { get; } = new GVector2Int(0, -1);
    public static GVector2Int right { get; } = new GVector2Int(1, 0);
    public static GVector2Int left { get; } = new GVector2Int(-1, 0);
}
