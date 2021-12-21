using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            betweenTimer = betweenWaitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (betweenTimer > 0)
            {
                betweenTimer -= Time.deltaTime;
            }
            else
            {
                betweenTimer = betweenWaitTime;
                //SceneManager.LoadScene(Random.Range(1, 6));
                SceneManager.LoadScene(3);
            }
        }
    }

    public void LoadBetweenScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SetGameResult(int result)
    {
        gameResult = result;
    }

    public int GetGameResult()
    {
        return gameResult;
    }

    public void DeductLife()
    {
        numLives--;
    }

    public int GetLives()
    {
        return numLives;
    }

    public void IncrementScore()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }
}
