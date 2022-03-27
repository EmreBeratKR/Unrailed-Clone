using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusChecker : MonoBehaviour
{
    public const float overlapBoxHeight = 5f;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform[] focusPoints;

    public Vector3 worldPosition { get; private set; }

    public GameObject focus
    {
        get
        {
            foreach (var focusPoint in focusPoints)
            {
                worldPosition = MyGrid.GetWorldPosition(MyGrid.GetGridPosition(focusPoint.position));

                var overlaps = Physics.OverlapBox(worldPosition + Vector3.up * overlapBoxHeight, new Vector3(0.005f, overlapBoxHeight, 0.005f), Quaternion.Euler(Vector3.zero), targetLayer, QueryTriggerInteraction.Collide);
                if (overlaps.Length == 0) continue;

                foreach (var overlap in overlaps)
                {
                    if (overlap.TryGetComponent(out IStackable stackable))
                    {
                        if (stackable.isGrabbed) continue;

                        return overlap.gameObject;
                    }
                    else if (overlap.TryGetComponent(out IHarvestable harvestable))
                    {
                        return overlap.gameObject;
                    }
                }
            }
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var focusPoint in focusPoints)
        {
            Gizmos.DrawSphere(focusPoint.position, 0.25f);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(worldPosition + Vector3.up * MyGrid.sizeX * 0.5f, Vector3.one * MyGrid.sizeX);
    }
}
