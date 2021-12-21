using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: CleanHouseSceneManager.cs
/// description: Manages the progress and completion of the Clean House Mini Game
/// author: Nathan Ballay
/// </summary>
public class CleanHouseSceneManager : MonoBehaviour
{
    // Prefabs
    [SerializeField] GameObject dirtPrefab;
    [SerializeField] GameObject dirtParticlePrefab;
    [SerializeField] GameObject timerPrefab;

    // References
    private Timer timer;
    private GameManager gameManager;

    // Variables
    private GameObject dirt;
    private bool isGameWon = false;
    private bool isTimeUp = false;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        dirt = Instantiate(dirtPrefab, Vector3.zero, Quaternion.identity);
        
        timer = timerPrefab.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();

        if (dirt.transform.childCount == 0 && !isTimeUp && !isWaiting)
        {

            isWaiting = true;
            isGameWon = true;
            timer.StopBarDrain();
            gameManager.WonMiniGame();
        }

        if (isTimeUp && !isGameWon && !isWaiting)
        {
            isWaiting = true;
            gameManager.LostMiniGame();
        }
    }

    /// <summary>
    /// Checks if the timer has run out 
    /// </summary>
    private void CheckTime()
    {
        isTimeUp = timer.IsTimeUp();
    }
}