using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: DecorateTreeManager
/// description: Manages the progress and completion of the Decorate Tree Mini Game
/// author: Nathan Ballay
/// </summary>
public class DecorateTreeManager : MonoBehaviour
{
    // Prefabs
    [SerializeField] GameObject ornamentPrefab;
    [SerializeField] GameObject timerPrefab;

    // References
    private Timer timer;

    // Collections
    private List<GameObject> ornaments = new List<GameObject>();

    // Variables
    private bool isGameWon = false;
    private bool isTimeUp = false;
    private GameObject newOrnament;
    [SerializeField] float startingX;
    [SerializeField] float startingY;
    [SerializeField] float spawnVariability;
    [SerializeField] int numOrnaments;

    // Start is called before the first frame update
    void Start()
    { 
        timer = timerPrefab.GetComponent<Timer>();

        SpawnOrnaments();
    }

    // Update is called once per frame
    void Update()
    {
        CheckComplete();

        CheckTime();

        if (isGameWon && !isTimeUp)
        {
            Debug.Log("WIN");
        }

        if (isTimeUp && !isGameWon)
        {
            Debug.Log("LOSE");
        }
    }

    /// <summary>
    /// Spawns ornaments with some variability and fills the collection
    /// </summary>
    private void SpawnOrnaments()
    {
        for (int i = 0; i < numOrnaments; i++)
        {
            newOrnament = Instantiate(ornamentPrefab, RandomSpawnLocation(), Quaternion.identity);
            ornaments.Add(newOrnament);
        }
    }

    /// <summary>
    /// Generates a random locaiton for ornaments to spawn at
    /// </summary>
    /// <returns>Partially randomized Vector2</returns>
    private Vector3 RandomSpawnLocation()
    {
        float XPos = Random.Range(startingX - spawnVariability, startingX + spawnVariability);
        float YPos = Random.Range(startingY - spawnVariability, startingY + spawnVariability);

        return new Vector2(XPos, YPos);
    }

    /// <summary>
    /// Checks whether all ornaments have been properly placed
    /// </summary>
    private void CheckComplete()
    {
        bool isComplete = true;

        foreach (GameObject ornament in ornaments)
        {
            if (!ornament.GetComponent<MoveOrnament>().isLocked())
            {
                isComplete = false;
                break;
            }
        }

        isGameWon = isComplete;
    }

    /// <summary>
    /// Checks if the timer has run out 
    /// </summary>
    private void CheckTime()
    {
        isTimeUp = timer.IsTimeUp();
    }
}
