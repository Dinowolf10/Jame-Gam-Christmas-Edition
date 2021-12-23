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
    private SoundManager soundManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    /// <summary>
    /// Removes this dirt particle when the mouse passes over
    /// </summary>
    private void OnMouseEnter()
    {
        if (!gameManager.isGamePaused())
        {
            soundManager.PlayVacuumSuckSound();
            Destroy(this.gameObject);
        }
    }
}
