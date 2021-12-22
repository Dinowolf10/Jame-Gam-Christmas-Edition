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

    [SerializeField]
    private Animator stirAnimator;

    [SerializeField]
    private GameObject stirVictory, startingArrow;

    [SerializeField]
    private Transform clockwiseArrows, counterClockwiseArrows;

    [SerializeField]
    private float directionArrowRotationSpeed;

    // Populated in inspector
    [SerializeField]
    private StirPoint[] stirPoints;

    private bool hasStartedStirring;
    private bool isStirring;

    private GameManager gameManager;

    private StirPoint activePoint;
    private Renderer activePointRenderer;

    private int activePointIndex = 0;

    private int score = 0, roundNumber = 1;

    private bool isWaiting = false;

    [SerializeField]
    private int targetScore = 100;

    private Vector2 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        roundNumber = gameManager.GetRoundNumber();

        if (roundNumber == 1)
        {
            counterClockwiseArrows.gameObject.SetActive(true);
        }
        else if (roundNumber == 2)
        {
            clockwiseArrows.gameObject.SetActive(true);
        }

        // Hides all points
        /*for (int i = 0; i < stirPoints.Length; i++)
        {
            stirPoints[i].gameObject.SetActive(false);
        }*/

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
        if (score >= targetScore && !isWaiting)
        {
            isWaiting = true;
            timer.StopBarDrain();
            gameManager.WonMiniGame();

            StopCoroutine("StartTimerBetweenPoints");
            stirVictory.SetActive(true);
            stirAnimator.SetBool("hasWon", true);
        }

        // If time runs out, player loses
        if (timer.IsTimeUp() && !isWaiting)
        {
            isWaiting = true;
            gameManager.LostMiniGame();
        }

        // Updates mouse position
        UpdateMousePosition();

        // Updates spoon position
        UpdateSpoon();

        CheckStirring();

        RotateDirectionArrows();
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
        spoonTransform.position = mousePos;
    }

    /// <summary>
    /// Sets the next active point in the stirPoints array
    /// </summary>
    public void SetNextActivePoint()
    {
        if (isWaiting)
        {
            return;
        }

        StopCoroutine("StartTimerBetweenPoints");
        Debug.Log("Stopped Coroutine");

        if (!hasStartedStirring)
        {
            stirAnimator.enabled = true;
            stirAnimator.SetBool("hasStartedStirring", true);
            hasStartedStirring = true;
            startingArrow.SetActive(false);
        }

        isStirring = true;

        // Hides current active point and sets their isActivePoint bool to false
        activePoint.gameObject.SetActive(false);
        activePoint.isActivePoint = false;

        // Resets the current active point color to white
        //activePointRenderer.material.color = Color.white;

        if (roundNumber == 1)
        {
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
        }
        else if (roundNumber == 2)
        {
            // If the activePointIndex has reached the end of the stirPoints array, set activePointIndex to 0
            if (activePointIndex == 0)
            {
                activePointIndex = stirPoints.Length - 1;
            }
            // Otherwise increment activePointIndex
            else
            {
                activePointIndex--;
            }
        }

        // Set current active point to the active point index in stirPoints
        activePoint = stirPoints[activePointIndex];

        // Setup the new active point
        setupActivePoint();

        StartCoroutine("StartTimerBetweenPoints");
    }

    private IEnumerator StartTimerBetweenPoints()
    {
        Debug.Log("Started Coroutine");

        yield return new WaitForSeconds(0.5f);

        isStirring = false;
    }

    private void CheckStirring()
    {
        if (isStirring)
        {
            stirAnimator.enabled = true;
            Debug.Log("stirring");
        }
        else
        {
            stirAnimator.enabled = false;
            Debug.Log("not stirring");
        }
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
        //activePointRenderer = activePoint.gameObject.GetComponent<Renderer>();

        // Sets the active point's color to green
        //activePointRenderer.material.color = Color.green;
    }

    private void RotateDirectionArrows()
    {
        Vector3 rotation;

        if (roundNumber == 1)
        {
            rotation = new Vector3(counterClockwiseArrows.eulerAngles.x, counterClockwiseArrows.eulerAngles.y, counterClockwiseArrows.eulerAngles.z + directionArrowRotationSpeed * Time.deltaTime);
            counterClockwiseArrows.eulerAngles = rotation;
        }
        else if (roundNumber == 2)
        {
            rotation = new Vector3(clockwiseArrows.eulerAngles.x, clockwiseArrows.eulerAngles.y, clockwiseArrows.eulerAngles.z + directionArrowRotationSpeed * Time.deltaTime);
            clockwiseArrows.eulerAngles = rotation;
        }
    }
}
