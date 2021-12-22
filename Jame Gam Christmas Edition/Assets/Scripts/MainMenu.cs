using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MouseCursor mouseCursor;

    private void Start()
    {
        mouseCursor = GameObject.Find("MouseCursor").GetComponent<MouseCursor>();
    }

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

    /// <summary>
    /// Load the main menu of the game
    /// </summary>
    public void LoadMainMenu()
    {
        // Resets mouse state to normal
        mouseCursor.ResetMouseState();

        // Loads the main menu
        SceneManager.LoadScene(0);
    }
}
