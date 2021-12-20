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

    private Vector3 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
        activePointIndex = 0;

        activePoint = stirPoints[activePointIndex];
        setupActivePoint();
    }

    // Update is called once per frame
    private void Update()
    {
        if (score >= 100)
        {
            Debug.Log("You Win!");
            return;
        }

        if (timer.IsTimeUp())
        {
            Debug.Log("You Lose!");
            return;
        }

        UpdateMousePosition();

        UpdateSpoon();
    }

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

        activePoint.isActivePoint = true;

        activePointRenderer = activePoint.gameObject.GetComponent<Renderer>();

        activePointRenderer.material.color = Color.green;
    }
}
