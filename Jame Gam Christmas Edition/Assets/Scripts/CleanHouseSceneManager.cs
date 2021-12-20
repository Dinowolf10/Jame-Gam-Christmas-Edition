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

    // Variables
    private GameObject dirt;
    private bool isGameWon = false;
    private bool isTimeUp = false;

    // Start is called before the first frame update
    void Start()
    {
        dirt = Instantiate(dirtPrefab, Vector3.zero, Quaternion.identity);
        timer = timerPrefab.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();

        if (dirt.transform.childCount == 0 && !isTimeUp)
        {
            isGameWon = true;
            Debug.Log("WIN");
        }

        if (isTimeUp && !isGameWon)
        {
            Debug.Log("LOSE");
        }
    }

    /// <summary>
<<<<<<< Updated upstream
    /// Checks if the timer has run out 
=======
    /// Checks the Timer to see if time has run out
>>>>>>> Stashed changes
    /// </summary>
    private void CheckTime()
    {
        isTimeUp = timer.IsTimeUp();
    }
}