using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: RemoveDirt.cs
/// description: Handles the removal of Dirt Particles from the scene
/// author: Nathan Ballay
/// </summary>
public class RemoveDirt : MonoBehaviour
{
    // References
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Removes this dirt particle when the mouse passes over
    /// </summary>
    private void OnMouseEnter()
    {
        if (!gameManager.isGamePaused())
        {
            Destroy(this.gameObject);
        }
    }
}
