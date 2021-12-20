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
            Debug.Log("You Win!");
            return;
        }

        // If time runs out, player loses
        if (timer.IsTimeUp())
        {
            Debug.Log("You Lose!");
            return;
        }

        // Updates mouse position
        UpdateMousePosition();

        // Updates spoon position
        UpdateSpoon();
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateSpoon()
    {
        spoonTransform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    public void SetNextActivePoint()
    {
        activePoint.gameObject.SetActive(false);

        activePoint.isActivePoint = false;

        activePointRenderer.material.color = Color.white;

        if (activePointIndex + 1 >= stirPoints.Length)
        {
            activePointIndex = 0;
        }
        else
        {
            activePointIndex++;
        }

        activePoint = stirPoints[activePointIndex];

        setupActivePoint();
    }

    private void setupActivePoint()
    {
        score++;

        activePoint.gameObject.SetActive(true);

        activePoint.isActivePoint = true;

        activePointRenderer = activePoint.gameObject.GetComponent<Renderer>();

        activePointRenderer.material.color = Color.green;
    }
}
