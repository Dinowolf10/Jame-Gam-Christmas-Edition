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
    /// <summary>
    /// Removes this dirt particle when the mouse passes over
    /// </summary>
    private void OnMouseEnter()
    {
        Destroy(this.gameObject);
    }
}
