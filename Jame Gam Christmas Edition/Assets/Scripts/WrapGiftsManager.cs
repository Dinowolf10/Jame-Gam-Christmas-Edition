using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: WrapGiftsManager.cs
/// description: Manages the progress and completion of the Wrap Gifts Mini Game
/// author: Nathan Ballay
/// </summary>
public class WrapGiftsManager : MonoBehaviour
{
    // Prefabs
    [SerializeField] GameObject timerPrefab;

    // References
    [SerializeField] GameObject midpoint;

    // Collections
    [SerializeField] List<GameObject> dragpoints;
    [SerializeField] List<GameObject> wrappingPaperSides;

    private Timer timer;
    private GameManager gameManager;

    // Variables
    private bool isGameWon = false;
    private bool isTimeUp = false;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        timer = timerPrefab.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDragpointPositions();

        CheckTime();

        if (isGameWon && !isTimeUp && !isWaiting)
        {
            Debug.Log("WIN");
        }

        if (isTimeUp && !isGameWon && !isWaiting)
        {
            Debug.Log("LOSE");
        }
    }

    private void CheckDragpointPositions()
    {
        bool isComplete = true;
        Vector3 newPos;

        for (int i = 0; i < dragpoints.Count; i++)
        {
            // If a dragpoint has not been placed in the right location, the game is not complete
            if (!dragpoints[i].GetComponent<Dragpoint>().isLocked())
            {
                isComplete = false;
            }

            AdjustWrappingPaperSide(i);

            // If this dragpoint has been properly placed, snap it in place and lock it
            if (Mathf.Abs(dragpoints[i].transform.position.x - midpoint.transform.position.x) < 0.5f &&
                Mathf.Abs(dragpoints[i].transform.position.y - midpoint.transform.position.y) < 0.5f &&
                !dragpoints[i].GetComponent<Dragpoint>().isLocked())
            {
                newPos = midpoint.transform.position;
                dragpoints[i].transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
                dragpoints[i].GetComponent<Dragpoint>().LockDragpoint();
            }
        }

        isGameWon = isComplete;
    }

    private void AdjustWrappingPaperSide(int index)
    {
        Vector3 newPos;
        float newPosCoord;
        float newScale;
        float distanceToMid;

        if (index == 0)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.x);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(newPosCoord, wrappingPaperSides[index].transform.position.y, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(newScale, 1);
        }
        else if (index == 1)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.y);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(wrappingPaperSides[index].transform.position.x, newPosCoord, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(1, newScale);
        }
        else if (index == 2)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.x);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(-newPosCoord, wrappingPaperSides[index].transform.position.y, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(newScale, 1);
        }
        else if (index == 3)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.y);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(wrappingPaperSides[index].transform.position.x, -newPosCoord, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(1, newScale);
        }
    }

    /// <summary>
    /// Checks if the timer has run out 
    /// </summary>
    private void CheckTime()
    {
        isTimeUp = timer.IsTimeUp();
    }
}
