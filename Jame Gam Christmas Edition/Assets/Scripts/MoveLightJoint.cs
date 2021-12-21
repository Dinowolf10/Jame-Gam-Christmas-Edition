using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: MoveLightJoints.cs
/// description: Allows player to drag and drop light joints using the mouse
/// author: Nathan Ballay
/// </summary>
public class MoveLightJoint : MonoBehaviour
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
    /// Drops the joint at its current location when the mouse is unclicked
    /// </summary>
    private void OnMouseUp()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            transform.position = initialPosition;
        }
    }

    /// <summary>
    /// Getter to see if the ornament has been properly placed
    /// </summary>
    /// <returns></returns>
    public bool isLocked()
    {
        return locked;
    }

    /// <summary>
    /// Locks the light from being picked up
    /// </summary>
    public void LockLight()
    {
        locked = true;
    }
}
