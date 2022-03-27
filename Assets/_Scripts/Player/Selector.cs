using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private PlayerStack playerStack;
    [SerializeField] private FocusChecker focusChecker;
    [SerializeField] private StackablePreview previews;
    [SerializeField] private Material selectedMaterial;
    private IStackable selectedStackable;

    private GameObject focus { get; set; }
    
    public bool isPreviewing { get; private set; }


    private void Update()
    {
        focus = focusChecker.focus;

        isPreviewing = HandlePreview();

        if (isPreviewing)
        {
            if (selectedStackable != null)
            {
                selectedStackable.Deselect();
                selectedStackable = null;
            }
            return;
        }

        HandleSelection();
    }

    private bool HandlePreview()
    {
        if (playerStack.isStackEmpty)
        {
            previews.Disable();
            return false;
        }

        if (focus != null)
        {
            previews.Disable();
            return false;
        }

        if (playerStack.stackedType != StackableType.RAIL)
        {
            previews.SetPosition(focusChecker.worldPosition, playerStack.stackedType);
            previews.SetPreview(playerStack.stackedType);
            return true;
        }

        previews.SetPosition(focusChecker.worldPosition, StackableType.RAIL);
        previews.rail.Reset();
        
        if (previews.rail.upperNeighbor != null)
        {
            if (previews.rail.upperNeighbor.previous != null && previews.rail.upperNeighbor.next == null)
            {    
                previews.SetPreview(StackableType.RAIL);
                return true;
            }
        }
        if (previews.rail.lowerNeighbor != null)
        {
            if (previews.rail.lowerNeighbor.previous != null && previews.rail.lowerNeighbor.next == null)
            {    
                previews.SetPreview(StackableType.RAIL);
                return true;
            }
        }
        if (previews.rail.rightNeighbor != null)
        {
            if (previews.rail.rightNeighbor.previous != null && previews.rail.rightNeighbor.next == null)
            {    
                previews.SetPreview(StackableType.RAIL);
                return true;
            }
        }
        if (previews.rail.leftNeighbor != null)
        {
            if (previews.rail.leftNeighbor.previous != null && previews.rail.leftNeighbor.next == null)
            {    
                previews.SetPreview(StackableType.RAIL);
                return true;
            }
        }

        previews.Disable();
        return false;
    }

    private void HandleSelection()
    {
        if (focus != null)
        {
            if (focus.TryGetComponent(out IStackable stackable))
            {
                stackable = stackable.Peek();
                
                if (stackable == selectedStackable) return;

                if (selectedStackable != null)
                {
                    selectedStackable.Deselect();
                    selectedStackable = null;
                }

                try
                {
                    if (((ILinkable)stackable).next != null) return;
                }
                catch (System.Exception){}

                stackable.Select(selectedMaterial);
                selectedStackable = stackable;
            }
        }
        else
        {
            if (selectedStackable != null)
            {
                selectedStackable.Deselect();
                selectedStackable = null;
            }
        }
    }
}

[System.Serializable]
public struct StackablePreview
{
    public Rail rail;
    public GameObject wood;

    public void SetPreview(StackableType type)
    {
        switch (type)
        {
            case StackableType.RAIL:
                rail.gameObject.SetActive(true);
                wood.SetActive(false);
                break;
            case StackableType.WOOD:
                rail.gameObject.SetActive(false);
                wood.SetActive(true);
                break;
        }
    }

    public void Disable()
    {
        rail.gameObject.SetActive(false);
        wood.SetActive(false);
    }

    public void SetPosition(Vector3 position, StackableType type)
    {
        switch (type)
        {
            case StackableType.RAIL:
                rail.GetComponent<IGrid>().SnapToGrid(position);
                break;
            case StackableType.WOOD:
                wood.GetComponent<IGrid>().SnapToGrid(position);
                break;
        }
    }
}
