using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGiftsManager : MonoBehaviour
{
    // Reference set in inspector
    [SerializeField]
    private Camera cam;

    // Reference set in inspector
    [SerializeField]
    private Timer timer;

    // Reference set in inspector
    [SerializeField]
    private SpriteRenderer display1, display2;

    // Array populated in inspector
    public GameObject[] objects;

    // Populated at start
    public Dictionary<string, GameObject> objectsToGet = new Dictionary<string, GameObject>();

    private GameManager gameManager;

    private bool hasWon = false;
    private bool isWaiting = false;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Populates objectsToGet
        PopulateObjectsToGet();
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks for object hit when user presses left click
        if (Input.GetMouseButtonDown(0))
        {
            CheckObjectHit();
        }

        // Checks if timer is up
        if (timer.IsTimeUp() && !hasWon && !isWaiting)
        {
            isWaiting = true;
            gameManager.LostMiniGame();
        }
    }

    /// <summary>
    /// Populates dictionary of objects to get randomly based on score
    /// </summary>
    private void PopulateObjectsToGet()
    {
        // Clears objectsToGet dictionary
        objectsToGet.Clear();

        // Stores gameManagerScore
        int gameManagerScore = gameManager.GetScore();

        // If user is in the first round of games
        if (gameManagerScore <= 5)
        {
            // Get and populate 1 random gift game object to the objectsToGet dictionary
            GameObject g = objects[Random.Range(0, 5)];
            objectsToGet.Add(g.name, g);

            // Sets the display object sprites and enabled their sprite renderer
            display1.sprite = g.GetComponent<SpriteRenderer>().sprite;
            display1.enabled = true;
        }
        // If user is in the second round of games
        else if (gameManagerScore <= 11)
        {
            // Get 2 random gift game objects
            GameObject g1 = objects[Random.Range(0, 5)];
            GameObject g2 = objects[Random.Range(0, 5)];

            // Make sure the two game objects are different
            while (g1 == g2)
            {
                g2 = objects[Random.Range(0, 5)];
            }

            // Add the game objects to the objectsToGet dictionary
            objectsToGet.Add(g1.name, g1);
            objectsToGet.Add(g2.name, g2);

            // Sets the display object sprites and enabled their sprite renderer
            display1.sprite = g1.GetComponent<SpriteRenderer>().sprite;
            display2.sprite = g2.GetComponent<SpriteRenderer>().sprite;
            display1.enabled = true;
            display2.enabled = true;
        }
    }

    /// <summary>
    /// Checks if an object was hit, if there was an object then check if the object is an object to get
    /// </summary>
    private void CheckObjectHit()
    {
        // Shoots a raycast from the camera to the current mouse position and store the collider hit
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        // If there was a collider hit
        if (hit)
        {
            // Store the name of the game object hit
            string name = hit.transform.gameObject.name;

            // If the object hit is one of the objectsToGet
            if (objectsToGet.ContainsKey(name))
            {
                // Hide the hit object
                objectsToGet[name].SetActive(false);

                // Remove the object from the objectsToGet dictionary
                objectsToGet.Remove(name);

                Debug.Log("Got Object!");

                // If the objectsToGet count is 0 and the player is not currently waiting for the scene switch
                if (objectsToGet.Count == 0 && !isWaiting)
                {
                    // Set isWaiting to true
                    isWaiting = true;

                    // Stop the timer drain
                    timer.StopBarDrain();

                    // Set won mini game to true in the gameManager
                    gameManager.WonMiniGame();

                    // Set hasWon to true
                    hasWon = true;
                }
            }
        }
    }
}
