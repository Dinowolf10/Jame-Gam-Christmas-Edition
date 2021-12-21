using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused = false;

    // References grabbed in inspector
    [SerializeField]
    private GameObject pauseButton, backToMenuButton;

    // Start is called before the first frame update
    private void Start()
    {
        // Resumes the game
        ResumeGame();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the user presses the escape or tab key
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            // If the game is already paused, resume the game
            if (isGamePaused)
            {
                ResumeGame();
            }
            // Otherwise pause the game
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// Pauses game by setting time scale to 0
    /// </summary>
    public void PauseGame()
    {
        // Sets time scale to 0
        Time.timeScale = 0.0f;

        // Sets isGamePaused to true
        isGamePaused = true;

        // Shows the pause and back to menu buttons
        pauseButton.SetActive(true);
        backToMenuButton.SetActive(true);
    }

    /// <summary>
    /// Resumes game by setting time scale to 1
    /// </summary>
    public void ResumeGame()
    {
        // Sets time scale to 1
        Time.timeScale = 1.0f;

        // Sets isGamePaused to false
        isGamePaused = false;

        // Hides the pause and back to menu buttons
        pauseButton.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void ReturnToMenu()
    {
        // Loads the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }
}
