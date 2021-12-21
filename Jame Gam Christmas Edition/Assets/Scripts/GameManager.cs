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
    // Variables
    private int gameResult;
    private int numLives;
    private int score;
    [SerializeField] float betweenWaitTime;
    private float betweenTimer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        numLives = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            betweenTimer = betweenWaitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (betweenTimer > 0)
            {
                Debug.Log(betweenTimer);
                betweenTimer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Going in with timer = " + betweenTimer.ToString());
                betweenTimer = betweenWaitTime;
                SceneManager.LoadScene(Random.Range(1, 6));
                //SceneManager.LoadScene(3);
            }
        }
    }

    /// <summary>
    /// Loads the scene that is displayed between mini games
    /// </summary>
    public void LoadBetweenScene()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Adjusts game state based on a player winning a mini game
    /// </summary>
    public void WonMiniGame()
    {
        SetGameResult(2);
        IncrementScore();
        LoadBetweenScene();
    }

    /// <summary>
    /// Adjusts game state based on a player losing a mini game
    /// </summary>
    public void LostMiniGame()
    {
        SetGameResult(1);
        DeductLife();
        LoadBetweenScene();
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
}
