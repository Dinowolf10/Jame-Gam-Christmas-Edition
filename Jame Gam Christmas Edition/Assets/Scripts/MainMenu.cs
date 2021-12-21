using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Reference grabbed in inspector
    [SerializeField]
    private MouseCursor mouseCursor;

    /// <summary>
    /// Starts the game by loading the next scene
    /// </summary>
    public void StartGame()
    {
        // Resets mouse state to normal
        mouseCursor.ResetMouseState();

        // Loads the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
