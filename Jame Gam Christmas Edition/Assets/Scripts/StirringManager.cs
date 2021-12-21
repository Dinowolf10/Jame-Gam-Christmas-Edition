using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirringManager : MonoBehaviour
{
    // Reference stored in inspector
    [SerializeField]
    private Camera cam;

    // Reference stored in inspector
    [SerializeField]
    private Transform spoonTransform;

    // Reference stored in inspector
    [SerializeField]
    private Timer timer;

    // Populated in inspector
    [SerializeField]
    private StirPoint[] stirPoints;

    private GameManager gameManager;

    private StirPoint activePoint;
    private Renderer activePointRenderer;

    private int activePointIndex = 0;

    private int score = 0;

    [SerializeField]
    private int targetScore = 100;

    private Vector3 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Hides all points
        for (int i = 0; i < stirPoints.Length; i++)
        {
            stirPoints[i].gameObject.SetActive(false);
        }

        // Sets activePointIndex to 0
        activePointIndex = 0;

        // Sets up first active point
        activePoint = stirPoints[activePointIndex];
        setupActivePoint();
    }

    // Update is called once per frame
    private void Update()
    {
        // If player reaches target score, they win
        if (score >= targetScore)
        {
            timer.StopBarDrain();
            gameManager.WonMiniGame();
        }

        // If time runs out, player loses
        if (timer.IsTimeUp())
        {
            gameManager.LostMiniGame();
        }

        // Updates mouse position
        UpdateMousePosition();

        // Updates spoon position
        UpdateSpoon();
    }

    /// <summary>
    /// Updates and stores mouse position
    /// </summary>
    private void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Updates spoon position to match the mouse position
    /// </summary>
    private void UpdateSpoon()
    {
        spoonTransform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    /// <summary>
    /// Sets the next active point in the stirPoints array
    /// </summary>
    public void SetNextActivePoint()
    {
        // Hides current active point and sets their isActivePoint bool to false
        activePoint.gameObject.SetActive(false);
        activePoint.isActivePoint = false;

        // Resets the current active point color to white
        activePointRenderer.material.color = Color.white;

        // If the activePointIndex has reached the end of the stirPoints array, set activePointIndex to 0
        if (activePointIndex + 1 >= stirPoints.Length)
        {
            activePointIndex = 0;
        }
        // Otherwise increment activePointIndex
        else
        {
            activePointIndex++;
        }

        // Set current active point to the active point index in stirPoints
        activePoint = stirPoints[activePointIndex];

        // Setup the new active point
        setupActivePoint();
    }

    /// <summary>
    /// Sets up the current active point
    /// </summary>
    private void setupActivePoint()
    {
        // Increases score
        score++;

        // Activates the current active point and sets their isActivePoint bool to true
        activePoint.gameObject.SetActive(true);
        activePoint.isActivePoint = true;

        // Stores a reference to the activePoint's renderer component
        activePointRenderer = activePoint.gameObject.GetComponent<Renderer>();

        // Sets the active point's color to green
        activePointRenderer.material.color = Color.green;
    }
}
