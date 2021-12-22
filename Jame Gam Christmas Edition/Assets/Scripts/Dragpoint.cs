using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: Dragpoint.cs
/// description: Allows player to drag and drop sides of wrapping paper using the mouse
/// author: Nathan Ballay
/// </summary>
public class Dragpoint : MonoBehaviour
{
    // Vectors
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    // Variables
    private float deltaX, deltaY;
    private bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    /// <summary>
    /// Calculates mouse movement delta while the button is held down
    /// </summary>
    private void OnMouseDown()
    {
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    /// <summary>
    /// Redraws the joint at the new mouse location
    /// </summary>
    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }

    /// <summary>
    /// Moves joint back to initial position if placed off screen
    /// </summary>
    private void OnMouseUp()
    {
        if (!locked)
        {
            transform.position = initialPosition;
        }
    }

    public void LockDragpoint()
    {
        locked = true;
    }

    public bool isLocked()
    {
        return locked;
    }
}
