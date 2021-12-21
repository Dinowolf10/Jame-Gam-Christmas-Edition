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
        else if (Input.GetMouseButtonUp(0))
        {
            cursorSR.sprite = mouseNormal;
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

    private void OnMouseEnter()
    {
        Debug.Log("Enter Hovering");
        //cursorSR.sprite = mouseHover;
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit Hovering");
        cursorSR.sprite = mouseNormal;
    }

    private void OnMouseOver()
    {
        Debug.Log("Hovering");
        cursorSR.sprite = mouseHover;
    }
}
