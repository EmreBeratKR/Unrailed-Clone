using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private Mesh mesh;
    [SerializeField] private float blockThreshold;


    public Direction? blockedDirection
    {
        get
        {
            if (Physics.BoxCast(transform.position, boxSize * 0.5f, transform.forward, out RaycastHit hit, transform.rotation, blockThreshold))
            {
                Vector3 dir = hit.point - transform.position;
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
                {
                    if (dir.x > 0)
                    {
                        return Direction.RIGHT;
                    }
                    return Direction.LEFT;
                }
                else
                {
                    if (dir.z > 0)
                    {
                        return Direction.UP;
                    }
                    return Direction.DOWN;
                }
            }
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (Physics.BoxCast(transform.position, boxSize * 0.5f, transform.forward, out RaycastHit hit, transform.rotation, blockThreshold))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireMesh(mesh, transform.position + transform.forward * hit.distance, transform.rotation, boxSize);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * blockThreshold);
            Gizmos.DrawWireMesh(mesh, transform.position + transform.forward * blockThreshold, transform.rotation, boxSize);
        }
    }
}
