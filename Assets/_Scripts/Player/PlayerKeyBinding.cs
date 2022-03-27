using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyBinding : MonoBehaviour
{
    public KeyCode[] moveUpKeys;
    public KeyCode[] moveDownKeys;
    public KeyCode[] moveRightKeys;
    public KeyCode[] moveLeftKeys;
    public KeyCode[] dashKeys;
    public KeyCode[] dropOrGrabKeys;


    public static bool isPressed(IEnumerable<KeyCode> keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKey(key)) return true;
        }
        return false;
    }

    public static bool isDown(IEnumerable<KeyCode> keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }
}
