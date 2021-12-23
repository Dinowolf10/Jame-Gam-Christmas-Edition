using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused = false;

    // References grabbed in inspector
    [SerializeField]
    private GameObject background, pauseButton, backToMenuButton;

    // References grabbed in inspector
    [SerializeField] 
    private GameManager gameManager;

    private int numPauseMenus;

    private void Awake()
    {
        numPauseMenus = FindObjectsOfType<PauseMenu>().Length;
        if (numPauseMenus != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Resumes the game
        ResumeGame();
    }

    /// <summary>
    /// Getter for the state of the pause menu
    /// </summary>
    /// <returns></returns>
    public bool isPaused()
    {
        return isGamePaused;
    }

    // Update is called once per frame
    private void Update()
    {
        GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // If the user presses the escape or tab key and they are not at the main menu
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
        // If not at the main menu, pause the game
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            // Sets time scale to 0
            Time.timeScale = 0.0f;

            // Sets isGamePaused to true
            isGamePaused = true;

            // Shows the background pause and back to menu buttons
            background.SetActive(true);
            pauseButton.SetActive(true);
            backToMenuButton.SetActive(true);
        }
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

        // Hides the background and pause and back to menu buttons
        background.SetActive(false);
        pauseButton.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void ReturnToMenu()
    {
        // Reset lives, round counter, and game result
        gameManager.ResetGameState();

        // Sets time scale to 1
        Time.timeScale = 1.0f;

        // Sets isGamePaused to false
        isGamePaused = false;

        // Hides the pause and back to menu buttons
        background.SetActive(false);
        pauseButton.SetActive(false);
        backToMenuButton.SetActive(false);

        // Loads the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }
}
