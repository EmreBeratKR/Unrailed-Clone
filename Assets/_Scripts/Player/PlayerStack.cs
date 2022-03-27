using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : MonoBehaviour
{
    public const int MAX_STACK_SIZE = 3;

    [SerializeField] private FocusChecker focusChecker;
    [SerializeField] private PlayerKeyBinding binding;
    [SerializeField] private Selector selector;
    [SerializeField] private Transform stackOrigin;
    private Stack<IStackable> stack = new Stack<IStackable>();
    private StackEvent stackEvent = StackEvent.NONE;
    
    public bool isDropOrGrab => PlayerKeyBinding.isDown(binding.dropOrGrabKeys);
    public StackableType stackedType => stack.Peek().type;
    public bool isStackEmpty => stack.Count == 0;
    public bool isStackFull => stack.Count >= MAX_STACK_SIZE;

    private GameObject focus { get; set; }


    private void Update()
    {
        stackEvent = StackEvent.NONE;
        focus = focusChecker.focus;

        HandleRailLinking();
        HandleGrab();
        HandleDrop();
        CarryStackables();
    }

    private void HandleGrab()
    {
        if (stackEvent != StackEvent.NONE) return;

        if (isStackFull) return;

        if (focus == null) return;

        if (focus.TryGetComponent(out IStackable stackable))
        {
            var grabbed = stackable.Peek();

            if (!((!isStackEmpty) || isDropOrGrab)) return;
            
            try
            {
                if ((!isDropOrGrab) && (((ILinkable)grabbed).previous != null)) return;
    
                if (isDropOrGrab && ((ILinkable)grabbed).next != null) return;
            }
            catch (System.Exception){}

            stackEvent = StackEvent.GRAB;

            while (!isStackFull)
            {
                if (!isStackEmpty)
                {
                    if (grabbed.type != stackedType) break;

                    if (stack.Contains(grabbed)) break;
                }

                if (grabbed.lower == null)
                {
                    grabbed.Clear();
                    if (!isStackEmpty)
                    {
                        grabbed.lower = stack.Peek();
                        stack.Peek().upper = grabbed;
                    }
                    stack.Push(grabbed);
                    grabbed.isGrabbed = true;
                    try
                    {    
                        if (((ILinkable)grabbed).previous != null)
                        {
                            ((ILinkable)grabbed).previous.next = null;
                            ((ILinkable)grabbed).previous = null;
                        }
                    }
                    catch (System.Exception){}
                    grabbed.Reset();
                    break;
                }
                else
                {
                    grabbed.lower.upper = null;
                    var next = grabbed.lower;
                    grabbed.Clear();
                    if (!isStackEmpty)
                    {
                        grabbed.lower = stack.Peek();
                        stack.Peek().upper = grabbed;
                    }
                    stack.Push(grabbed);
                    grabbed.isGrabbed = true;
                    grabbed.Reset();

                    grabbed = next;
                }
            }
        }
    }

    private void HandleDrop()
    {
        if (stackEvent != StackEvent.NONE) return;

        if (!isDropOrGrab) return;

        if (focus != null)
        {
            if (focus.TryGetComponent(out IStackable pivot))
            {
                stackEvent = StackEvent.DROP;

                IStackable peek = pivot.Peek();

                if (peek.type != stackedType) return;

                try
                {
                    if (((ILinkable)peek).previous != null) return;
                }
                catch (System.Exception){}

                while (true)
                {
                    if (!stack.TryPop(out IStackable stackable)) break;
                    
                    stackable.isGrabbed = false;

                    stackable.Clear();
                    stackable.lower = peek;
                    peek.upper = stackable;

                    stackable.SnapToGrid(peek.anchor);

                    stackable.Reset();

                    peek = stackable;
                }
            }
        }
        else
        {
            stackEvent = StackEvent.DROP;

            IStackable peek = null;

            while (true)
            {
                if (!stack.TryPop(out IStackable stackable)) break;

                stackable.isGrabbed = false;

                stackable.Clear();
                stackable.lower = peek;
                if (peek != null)
                {
                    peek.upper = stackable;
                    stackable.SnapToGrid(peek.anchor);
                }
                else
                {
                    stackable.SnapToGrid(focusChecker.worldPosition);
                }

                stackable.Reset();

                peek = stackable;
            }
        }
    }

    private void HandleRailLinking()
    {
        if (!selector.isPreviewing) return;

        if (!isDropOrGrab) return;

        if (stack.TryPeek(out IStackable test))
        {
            try
            {
                var temp = (ILinkable)test;
            }
            catch (System.Exception)
            {
                return;
            }
        }

        if (stack.TryPop(out IStackable stackable))
        {
            ILinkable linkable = stackable as ILinkable;

            if (linkable == null) return;

            stackEvent = StackEvent.LINK;

            if (stack.TryPeek(out IStackable peek)) peek.upper = null;
            stackable.isGrabbed = false;
            stackable.Clear();
            stackable.SnapToGrid(focusChecker.worldPosition);
            linkable.LinkWithPrevious();
        }
    }

    private void CarryStackables()
    {
        if (isStackEmpty) return;
        
        foreach (var stackable in stack)
        {
            stackable.SnapToStack(stackable.lower == null ? stackOrigin.position : stackable.lower.anchor, transform.eulerAngles);
        }
    }
}

public enum StackEvent { NONE, GRAB, DROP, LINK }
