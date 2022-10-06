using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseUtilities : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    public Vector2 GetMouseDirection(Vector2 origin)
    {
        //gets the screen position of our mouse
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        //this get the screen position, and converts into a world position(this helps performance across different devices)
        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);

        return mouseWorldPos - origin;
    }
}
