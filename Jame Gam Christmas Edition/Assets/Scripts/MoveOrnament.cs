using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: MoveOrnament.cs
/// description: Allows player to drag and drop ornaments onto the tree using the mouse
/// author: Nathan Ballay
/// </summary>
public class MoveOrnament : MonoBehaviour
{
    // References
    [SerializeField] GameObject tree;
    private SpriteRenderer treeSprite;

    // Vectors
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    // Variables
    private float deltaX, deltaY;
    private bool onTree = false;
    private bool locked = false;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        treeSprite = tree.GetComponent<SpriteRenderer>();

        DefineBounds();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnTree();
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
    /// Redraws the ornament at the new mouse location
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
    /// Places the ornament on the tree, or moves it back to its original location
    /// </summary>
    private void OnMouseUp()
    {
        if (onTree)
        {
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
            locked = true;
        } 
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }

    /// <summary>
    /// Determines if the ornament is within the bound of the tree
    /// </summary>
    private void CheckOnTree()
    {
        if (transform.position.x > minX && 
            transform.position.x < maxX &&
            transform.position.y > minY && 
            transform.position.y < maxY)
        {
            onTree = true;
        }
    }

    /// <summary>
    /// Defines the bounds of the tree
    /// </summary>
    private void DefineBounds()
    {
        minX = tree.transform.position.x - treeSprite.bounds.extents.x;
        maxX = tree.transform.position.x + treeSprite.bounds.extents.x;
        minY = tree.transform.position.y - treeSprite.bounds.extents.y;
        maxY = tree.transform.position.y + treeSprite.bounds.extents.y;
    }

    /// <summary>
    /// Getter to see if the ornament has been properly placed
    /// </summary>
    /// <returns></returns>
    public bool isLocked()
    {
        return locked;
    }
}
