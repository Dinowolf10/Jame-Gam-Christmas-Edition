using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider slider;

    private GameManager gameManager;

    private SoundManager soundManager;

    private MouseCursor mouseCursor;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        mouseCursor = GameObject.Find("MouseCursor").GetComponent<MouseCursor>();

        // If the game has already been played, set the slider value to the current volume
        if (gameManager.GetGameResult() != 0 && SceneManager.GetActiveScene().name == "MainMenu")
        {
            slider.value = soundManager.GetVolume();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            soundManager.SetVolume(slider.value);
        }
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

        gameManager.StopAudio();
        gameManager.PlayMainGameplaySound();

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

        // Stop the current audio/music
        gameManager.StopAudio();
        gameManager.PlayMainMenuSound();

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
