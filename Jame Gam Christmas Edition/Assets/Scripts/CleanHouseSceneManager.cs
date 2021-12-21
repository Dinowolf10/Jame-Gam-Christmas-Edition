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
    [SerializeField] GameObject vacuumSprite;
    [SerializeField] GameObject sparkle1;
    [SerializeField] GameObject sparkle2;
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

        UpdateVacuumPosition();

        if (dirt.transform.childCount == 0 && !isTimeUp && !isWaiting)
        {
            ActivateSparkles();
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
    /// Updates the position of the vacuum sprite to follow the player's cursor
    /// </summary>
    private void UpdateVacuumPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = new Vector3(mousePos.x, mousePos.y, 0);
        vacuumSprite.transform.position = newPos;
    }

    private void ActivateSparkles()
    {
        sparkle1.SetActive(true);
        sparkle2.SetActive(true);
    }
 
    /// <summary>
    /// Checks if the timer has run out 
    /// </summary>
    private void CheckTime()
    {
        isTimeUp = timer.IsTimeUp();
    }
}