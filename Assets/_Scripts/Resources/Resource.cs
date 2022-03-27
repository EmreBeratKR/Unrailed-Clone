using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour, IGrid, IStackable, ISelectable
{
    [SerializeField] private Transform stackAnchor;

#region IGrid interface
    public GVector2Int gridPosition => MyGrid.GetGridPosition(transform.position);
    public Vector3 worldPosition => MyGrid.GetWorldPosition(gridPosition);
    public Vector3 realPosition => transform.position;
    public Vector3 realRotation => transform.eulerAngles;

    public virtual void SnapToGrid(Vector3 position)
    {
        transform.position = position;
        transform.eulerAngles = Vector3.zero;
    }
#endregion

#region IStackable interface
    public abstract StackableType type { get; }
    public Vector3 anchor => stackAnchor.position;
    public bool isGrabbed { get; set; }
    public IStackable upper { get; set; }
    public IStackable lower { get; set; }

    public virtual void Clear()
    {
        this.upper = null;
        this.lower = null;
    }

    public virtual void Flip()
    {
        var temp = this.upper;
        this.upper = this.lower;
        this.lower = temp;
    }

    public virtual IStackable Peek()
    {
        IStackable result = this.GetComponent<IStackable>();
        while (result.upper != null)
        {
            result = result.upper;
        }
        return result;
    }

    public virtual void Reset()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public virtual void SnapToStack(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }
#endregion

#region ISelectable interface
    public List<Material> oldMaterials { get; set; }

    public virtual void Select(Material material)
    {   
        bool isInit = (oldMaterials == null);

        if (isInit) oldMaterials = new List<Material>();

        foreach (var mesh in this.GetComponentsInChildren<MeshRenderer>())
        {
            if (isInit) oldMaterials.Add(mesh.material);

            mesh.material = material;
        }
    }

    public virtual void Deselect()
    {
        int i = 0;
        foreach (var mesh in this.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material = oldMaterials[i];
            i++;
        }
    }
#endregion
}