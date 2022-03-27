using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : StackableBase
{
    public const float overlapBoxHeight = 50f;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject normalRail;
    [SerializeField] private GameObject cornerRail;
    public RailPath normalPath;
    public RailPath cornerPath;
    
    public override StackableType type => StackableType.RAIL;

    public override ILinkable previous { get; set; }
    public override ILinkable next { get; set; }
    public override bool isReversed { get; set; }

    public override ILinkable upperNeighbor => GetNeighbor(transform.position + (Vector3.forward * MyGrid.sizeZ));
    public override ILinkable lowerNeighbor => GetNeighbor(transform.position + (Vector3.back * MyGrid.sizeZ));
    public override ILinkable rightNeighbor => GetNeighbor(transform.position + (Vector3.right * MyGrid.sizeX));
    public override ILinkable leftNeighbor => GetNeighbor(transform.position + (Vector3.left * MyGrid.sizeX));


    public override void Reset()
    {
        base.Reset();
        this.isCorner = false;
        normalRail.SetActive(true);
        cornerRail.SetActive(false);
    }

    public override void LinkWithPrevious()
    {
        if (upperNeighbor != null && upperNeighbor.previous != null && upperNeighbor.next == null)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            this.isReversed = false;

            this.previous = upperNeighbor;
            this.previous.LinkWithNext(this as ILinkable);
        }
        else if (lowerNeighbor != null && lowerNeighbor.previous != null && lowerNeighbor.next == null)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            this.isReversed = true;

            this.previous = lowerNeighbor;
            this.previous.LinkWithNext(this as ILinkable);
        }
        else if (rightNeighbor != null && rightNeighbor.previous != null && rightNeighbor.next == null)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.isReversed = true;

            this.previous = rightNeighbor;
            this.previous.LinkWithNext(this as ILinkable);
        }
        else if (leftNeighbor != null && leftNeighbor.previous != null && leftNeighbor.next == null)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.isReversed = false;

            this.previous = leftNeighbor;
            this.previous.LinkWithNext(this as ILinkable);
        }
    }

    public override void LinkWithNext(ILinkable next)
    {
        this.next = next;
        StartCoroutine(LinkWithNext_Co());
    }

    private IEnumerator LinkWithNext_Co()
    {
        yield return new WaitForFixedUpdate();

        if (this.previous == this.upperNeighbor)
        {
            if (this.next == this.leftNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                this.isReversed = false;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.rightNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                this.isReversed = true;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.lowerNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                this.isReversed = false;
                this.isCorner = false;
                this.normalRail.SetActive(true);
                this.cornerRail.SetActive(false);
            }
        }
        else if (this.previous == this.lowerNeighbor)
        {
            if (this.next == this.leftNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.isReversed = true;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.rightNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 270, 0);
                this.isReversed = false;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.upperNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                this.isReversed = true;
                this.isCorner = false;
                this.normalRail.SetActive(true);
                this.cornerRail.SetActive(false);
            }
        }
        else if (this.previous == this.rightNeighbor)
        {
            if (this.next == this.upperNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                this.isReversed = false;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.lowerNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 270, 0);
                this.isReversed = true;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.leftNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.isReversed = true;
                this.isCorner = false;
                this.normalRail.SetActive(true);
                this.cornerRail.SetActive(false);
            }
        }
        else if (this.previous == this.leftNeighbor)
        {
            if (this.next == this.upperNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                this.isReversed = true;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.lowerNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.isReversed = false;
                this.isCorner = true;
                this.normalRail.SetActive(false);
                this.cornerRail.SetActive(true);
            }
            else if (this.next == this.rightNeighbor)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.isReversed = false;
                this.isCorner = false;
                this.normalRail.SetActive(true);
                this.cornerRail.SetActive(false);
            }
        }
    }

    private ILinkable GetNeighbor(Vector3 pos)
    {
        if (this.GetComponent<IStackable>().lower != null) return null;

        var colliders = Physics.OverlapBox(pos + Vector3.up * overlapBoxHeight, new Vector3(0.5f, overlapBoxHeight, 0.5f), Quaternion.identity, targetLayer, QueryTriggerInteraction.Collide);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out ILinkable rail))
            {
                if (collider.GetComponent<IStackable>().isGrabbed) return null;

                return rail;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct RailPath
{
    public Transform start;
    public Transform end;
    public Transform curveAnchor;
}