using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    // Reference set in inspector
    [SerializeField]
    private Sprite mouseNormal, mouseHover, mouseClick;

    // Reference set in inspector
    [SerializeField]
    private Transform cursorTransform;

    // Reference set in inspector
    [SerializeField]
    private SpriteRenderer cursorSR;

    private Vector2 mousePos;

    private bool isHovering = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Hide mouse cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the mouse cursor sprite position
        UpdateMousePosition();

        // If the user clicks, set the sprite to mouseClick
        if (Input.GetMouseButton(0))
        {
            cursorSR.sprite = mouseClick;
        }
        
        // If the user stops clicking
        if (Input.GetMouseButtonUp(0))
        {
            // If the mouse is not hovering over anything, set sprite to mouseNormal
            if (!isHovering)
            {
                cursorSR.sprite = mouseNormal;
            } 
            // Otherwise set sprite to mouseHover
            else
            {
                cursorSR.sprite = mouseHover;
            }
        }
    }

    /// <summary>
    /// Updates and stores mouse position
    /// </summary>
    private void UpdateMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorTransform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the mouse is hovering over an object, set the sprite to mouseHover and isHovering to true
        cursorSR.sprite = mouseHover;
        isHovering = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the mouse stops hovering over an object, set the sprite to mouseNormal and isHovering to false
        cursorSR.sprite = mouseNormal;
        isHovering = false;
    }

    /// <summary>
    /// Resets the mouse to the normal sprite state
    /// </summary>
    public void ResetMouseState()
    {
        cursorSR.sprite = mouseNormal;
        isHovering = false;
    }
}
