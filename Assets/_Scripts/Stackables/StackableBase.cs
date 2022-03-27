using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableBase : Resource, ILinkable
{
#region ILinkable interface
    public virtual ILinkable previous { get; set; }
    public virtual ILinkable next { get; set; }
    public virtual ILinkable upperNeighbor { get; }
    public virtual ILinkable lowerNeighbor { get; }
    public virtual ILinkable rightNeighbor { get; }
    public virtual ILinkable leftNeighbor { get; }
    public virtual bool isReversed { get; set; }
    public virtual bool isCorner { get; set; }
    public virtual void LinkWithPrevious()
    {
        throw new System.NotImplementedException();
    }
    public virtual void LinkWithNext(ILinkable next)
    {
        throw new System.NotImplementedException();
    }
#endregion
}
