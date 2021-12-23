using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// file: GameManager.cs
/// description: Manages the progress of the overarching game and scene loading
/// author: Nathan Ballay
/// </summary>
public class GameManager : MonoBehaviour
{
    // Reference set in inspector
    [SerializeField]
    private MouseCursor mouseCursor;

    // Reference set in inspector
    [SerializeField]
    private PauseMenu pauseMenu;

    // Collections
    private HashSet<int> playedScenes = new HashSet<int>();

    // Variables
    private int gameResult;
    private int numLives;
    private int score = 0;
    private int roundNumber = 0;
    [SerializeField] float betweenWaitTime;
    private float betweenTimer;
    private int numGameManagers;

    private void Awake()
    {
        numGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numGameManagers != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        numLives = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        betweenTimer = betweenWaitTime;

        roundNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (betweenTimer > 0)
            {
                betweenTimer -= Time.deltaTime;
            }
            else
            {
                // Resets mouse state to normal
                mouseCursor.ResetMouseState();

                betweenTimer = betweenWaitTime;

                // If the player runs out of lives, load the game over scene
                if (numLives == 0)
                {
                    SceneManager.LoadScene(9);
                }
                else
                {
                    // Otherwise, load another mini game/check for the win condition
                    LoadNextScene();
                }
            }
        }
    }

    /// <summary>
    /// Picks and loads a new game that hasn't been played in this sequence
    /// </summary>
    private void LoadNextScene()
    {
        // Resets mouse state to normal
        mouseCursor.ResetMouseState();

        int idx;

        // If all games have been played, reset the played games tracker and increment the round
        if (playedScenes.Count == 7)
        {
            playedScenes.Clear();
            roundNumber++;
        }

        // If the player has completed two rounds, then they win
        if (roundNumber == 3)
        {
            SceneManager.LoadScene(10);
        }
        // Otherwise, load the next mini game
        else
        {
            // Find the index of a game that hasn't been played yet
            while (playedScenes.Contains(idx = Random.Range(2, 9)))
            {
                continue;
            }

            // Add the scene index to the list of played games
            playedScenes.Add(idx);

            // Load the new game
            SceneManager.LoadScene(idx);
        }
    }

    /// <summary>
    /// Reinitializes round count and number of lives
    /// </summary>
    public void ResetGameState()
    {
        SetGameResult(0);
        numLives = 4;
        roundNumber = 1;
    }

    /// <summary>
    /// Loads the scene that is displayed between mini games
    /// </summary>
    private void LoadBetweenScene()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Adjusts game state based on a player winning a mini game
    /// </summary>
    public void WonMiniGame()
    {
        StartCoroutine(WinPause());
    }

    IEnumerator WinPause()
    {
        yield return new WaitForSeconds(2f);
        SetGameResult(2);
        IncrementScore();
        LoadBetweenScene();
    }
    /// <summary>
    /// Adjusts game state based on a player losing a mini game
    /// </summary>
    public void LostMiniGame()
    {
        StartCoroutine(LosePause());
    }

    IEnumerator LosePause()
    {
        yield return new WaitForSeconds(2f);
        SetGameResult(1);
        DeductLife();
        LoadBetweenScene();
    }

    public bool isGamePaused()
    {
        return pauseMenu.isPaused();
    }

    /// <summary>
    /// Sets the mini game result based on win/loss
    /// </summary>
    /// <param name="result">1 for loss, 2 for win</param>
    private void SetGameResult(int result)
    {
        gameResult = result;
    }

    /// <summary>
    /// Getter for the current game result
    /// </summary>
    /// <returns></returns>
    public int GetGameResult()
    {
        return gameResult;
    }

    /// <summary>
    /// Removes a life from the player
    /// </summary>
    private void DeductLife()
    {
        numLives--;
    }

    /// <summary>
    /// Getter for the current number of lives
    /// </summary>
    /// <returns></returns>
    public int GetLives()
    {
        return numLives;
    }

    /// <summary>
    /// Adds one to the player's score
    /// </summary>
    private void IncrementScore()
    {
        score++;
    }

    /// <summary>
    /// Getter for the current player score
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Getter for the current round number
    /// </summary>
    /// <returns></returns>
    public int GetRoundNumber()
    {
        return roundNumber;
    }
}
