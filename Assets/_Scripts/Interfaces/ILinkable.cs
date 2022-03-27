using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILinkable : IGrid
{
    ILinkable previous { get; set; }
    ILinkable next { get; set; }
    ILinkable upperNeighbor { get; }
    ILinkable lowerNeighbor { get; }
    ILinkable rightNeighbor { get; }
    ILinkable leftNeighbor { get; }
    bool isReversed { get; set; }
    bool isCorner { get; set; }
    void LinkWithPrevious();
    void LinkWithNext(ILinkable next);
}