using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    private Sprite mouseNormal, mouseHover, mouseClick;

    [SerializeField]
    private Transform cursorTransform;

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
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMousePosition();

        if (Input.GetMouseButton(0))
        {
            cursorSR.sprite = mouseClick;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (!isHovering)
            {
                cursorSR.sprite = mouseNormal;
            } 
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
        cursorSR.sprite = mouseHover;
        isHovering = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cursorSR.sprite = mouseNormal;
        isHovering = false;
    }

    public void ResetMouseState()
    {
        cursorSR.sprite = mouseNormal;
        isHovering = false;
    }
}
