using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;

    private MouseCursor mouseCursor;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        mouseCursor = GameObject.Find("MouseCursor").GetComponent<MouseCursor>();
    }

    /// <summary>
    /// Starts the game by loading the next scene
    /// </summary>
    public void StartGame()
    {
        // Reset lives and round counter
        gameManager.ResetGameState();

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
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
