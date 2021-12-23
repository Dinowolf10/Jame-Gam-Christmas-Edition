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
    [SerializeField] GameObject sparkle1;
    [SerializeField] GameObject sparkle2;
    private Timer timer;
    private GameManager gameManager;

    // Collections
    private List<GameObject> ornaments = new List<GameObject>();
    [SerializeField] List<GameObject> treeBlocks = new List<GameObject>();
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    // Variables
    private bool isGameWon = false;
    private bool isTimeUp = false;
    private bool isWaiting = false;
    private GameObject newOrnament;
    [SerializeField] float startingX;
    [SerializeField] float startingY;
    [SerializeField] float spawnVariability;
    private int numOrnaments;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        timer = timerPrefab.GetComponent<Timer>();

        SpawnOrnaments();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGamePaused())
        {
            return;
        }

        CheckOrnamentPlacement();
        CheckComplete();
        CheckTime();

        if (isGameWon && !isTimeUp && !isWaiting)
        {
            gameManager.PlaySparkleSound();
            ActivateSparkles();
            isWaiting = true;
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
    /// Spawns ornaments with some variability and fills the collection
    /// </summary>
    private void SpawnOrnaments()
    {
        int roundNumber = gameManager.GetRoundNumber();

        if (roundNumber == 1)
        {
            numOrnaments = 3;
        }
        else if (roundNumber == 2)
        {
            numOrnaments = 5;
        }

        for (int i = 0; i < numOrnaments; i++)
        {
            newOrnament = Instantiate(ornamentPrefab, RandomSpawnLocation(), Quaternion.identity);
            newOrnament.GetComponent<SpriteRenderer>().sprite = ChooseRandomSprite();
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

    private Sprite ChooseRandomSprite()
    {
        int idx = Random.Range(0, sprites.Count - 1);

        return sprites[idx];
    }

    private void CheckOrnamentPlacement()
    {
        for (int i = 0; i < ornaments.Count; i++)
        {
            for (int j = 0; j < treeBlocks.Count; j++)
            {
                if (AABBCollision(ornaments[i], treeBlocks[j]) && !ornaments[i].GetComponent<MoveOrnament>().isLocked())
                {
                    gameManager.PlayGrabSound();
                    ornaments[i].GetComponent<MoveOrnament>().LockOrnament();
                }
            }
        }
    }

    /// <summary>
    /// Checks for a collision between two objects using the AABB method
    /// </summary>
    /// <param name="o1">First game object being considered for a collision</param>
    /// <param name="o2">Second game object being considered for a collision</param>
    /// <returns>boolean determining whether a collision has occurred</returns>
    public bool AABBCollision(GameObject o1, GameObject o2)
    {
        // Get references to bounds of both objects
        Bounds bounds1 = o1.GetComponent<SpriteRenderer>().bounds;
        Bounds bounds2 = o2.GetComponent<SpriteRenderer>().bounds;

        // Find all mins and maxes of o1
        float minX1 = bounds1.min.x;
        float maxX1 = bounds1.max.x;
        float minY1 = bounds1.min.y;
        float maxY1 = bounds1.max.y;

        // Find all mins and maxes of o2
        float minX2 = bounds2.min.x;
        float maxX2 = bounds2.max.x;
        float minY2 = bounds2.min.y;
        float maxY2 = bounds2.max.y;

        // Check all necessary conditions for a collision
        bool cond1 = minX2 < maxX1;
        bool cond2 = maxX2 > minX1;
        bool cond3 = maxY2 > minY1;
        bool cond4 = minY2 < maxY1;

        // Determine if collision has occurred
        if (cond1 && cond2 && cond3 && cond4)
        {
            return true;
        }
        else
        {
            return false;
        }
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
