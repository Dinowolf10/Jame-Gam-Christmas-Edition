using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: DecorateHouseManager.cs
/// description: Manages the progress and completion of the Decorate House Mini Game
/// author: Nathan Ballay
/// </summary>
public class DecorateHouseManager : MonoBehaviour
{
    // Collections
    [SerializeField] List<GameObject> lightJoints = new List<GameObject>();
    [SerializeField] List<GameObject> lightFixtures = new List<GameObject>();

    // Prefabs
    [SerializeField] GameObject timerPrefab;

    // References
    [SerializeField] GameObject sparkle1;
    [SerializeField] GameObject sparkle2;
    private Timer timer;
    private GameManager gameManager;
    private SoundManager soundManager;

    // Variables
    private bool isGameWon = false;
    private bool isTimeUp = false;
    private bool isWaiting = false;
    [SerializeField] float lightShuffleVariability;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        timer = timerPrefab.GetComponent<Timer>();

        lightJoints[0].transform.position = lightFixtures[0].transform.position;

        if(gameManager.GetRoundNumber() == 2)
        {
            lightJoints[2].gameObject.SetActive(true);
            lightJoints[4].gameObject.SetActive(true);
            lightFixtures[2].gameObject.SetActive(true);
            lightFixtures[4].gameObject.SetActive(true);
        }

        ShuffleLightJoints();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLightPositions();

        CheckTime();

        if (isGameWon && !isTimeUp && !isWaiting)
        {
            soundManager.PlaySparkleSound();
            ActivateSparkles();
            isWaiting = true;
            timer.StopBarDrain();
            gameManager.WonMiniGame();
        }

        if (isTimeUp && !isGameWon && !isWaiting)
        {
            LockAllLightJoints();
            isWaiting = true;
            gameManager.LostMiniGame();
        }
    }

    /// <summary>
    /// Add variation to the spawn location of the light joints
    /// </summary>
    private void ShuffleLightJoints()
    {
        Vector2 newPos;

        for (int i = 1; i < lightJoints.Count; i++)
        {
            newPos = GetRandomShuffleLocation(lightJoints[i].transform);
            lightJoints[i].transform.position = newPos;
        }
    }

    /// <summary>
    /// Locks all light joints at their current location
    /// </summary>
    private void LockAllLightJoints()
    {
        foreach (GameObject lightJoint in lightJoints)
        {
            lightJoint.GetComponent<MoveLightJoint>().LockLight();
        }
    }

    /// <summary>
    /// Generate a new random location based off of the initial location
    /// </summary>
    /// <param name="transform"></param>
    /// <returns>Randomized location vector</returns>
    public Vector2 GetRandomShuffleLocation(Transform transform)
    {
        float newX = transform.position.x + Random.Range(-lightShuffleVariability, lightShuffleVariability);
        float newY = transform.position.y + Random.Range(-lightShuffleVariability, lightShuffleVariability);

        return new Vector2(newX, newY);
    }
    /// <summary>
    /// Determines if all light joints have been placed and locks in properly placed light joints
    /// </summary>
    private void CheckLightPositions()
    {
        bool isComplete = true;
        Vector3 newPos;

        for (int i = 0; i < lightJoints.Count; i++)
        {
            // If a joint has not been placed in the right location, the game is not complete
            if (!lightJoints[i].GetComponent<MoveLightJoint>().isLocked() && lightJoints[i].gameObject.activeSelf)
            {
                isComplete = false;
            }

            // If this joint has been properly placed, snap it in place and lock it
            if (Mathf.Abs(lightJoints[i].transform.position.x - lightFixtures[i].transform.position.x) < 0.5f &&
                Mathf.Abs(lightJoints[i].transform.position.y - lightFixtures[i].transform.position.y) < 0.5f &&
                !lightJoints[i].GetComponent<MoveLightJoint>().isLocked())
            {
                // If this is the pre-placed joint, don't play the sound effect
                if (i != 0)
                {
                    soundManager.PlayGrabSound();
                }
                newPos = lightFixtures[i].transform.position;
                lightJoints[i].transform.position = new Vector3(newPos.x, newPos.y - 0.25f, newPos.z);
                lightJoints[i].GetComponent<MoveLightJoint>().LockLight();
                lightFixtures[i].GetComponent<SpriteRenderer>().color = Color.green;
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
