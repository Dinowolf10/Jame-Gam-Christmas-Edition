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
    // Art
    [SerializeField] List<Sprite> giftSprites = new List<Sprite>();

    // Prefabs
    [SerializeField] GameObject giftPrefab;
    [SerializeField] GameObject timerPrefab;

    // Collections
    private List<GameObject> gifts = new List<GameObject>();
    private List<GameObject> midpoints = new List<GameObject>();
    private List<GameObject> dragpoints = new List<GameObject>();
    private List<GameObject> wrappingPaperSides = new List<GameObject>();

    // References
    private Timer timer;
    private GameManager gameManager;

    // Variables
    private bool isGameWon = false;
    private bool isTimeUp = false;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        timer = timerPrefab.GetComponent<Timer>();

        GameObject gift;

        if (gameManager.GetRoundNumber() == 1)
        {
            gift = (Instantiate(giftPrefab, Vector3.zero, Quaternion.identity));

            gift.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = giftSprites[Random.Range(0, giftSprites.Count)];

            midpoints.Add(gift.transform.GetChild(6).gameObject);

            for (int i = 2; i < 6; i++)
            {
                wrappingPaperSides.Add(gift.transform.GetChild(i).gameObject);
            }

            for (int i = 7; i < 11; i++)
            {
                dragpoints.Add(gift.transform.GetChild(i).gameObject);
            }

            gifts.Add(gift);

        }
        else
        {
            gift = Instantiate(giftPrefab, new Vector3(-2.5f, 0f, 0f), Quaternion.identity);

            gift.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = giftSprites[Random.Range(0, giftSprites.Count)];

            midpoints.Add(gift.transform.GetChild(6).gameObject);

            for (int i = 2; i < 6; i++)
            {
                wrappingPaperSides.Add(gift.transform.GetChild(i).gameObject);
            }

            for (int i = 7; i < 11; i++)
            {
                dragpoints.Add(gift.transform.GetChild(i).gameObject);
            }

            gifts.Add(gift);

            gift = Instantiate(giftPrefab, new Vector3(2.5f, 0f, 0f), Quaternion.identity);

            gift.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = giftSprites[Random.Range(0, giftSprites.Count)];

            midpoints.Add(gift.transform.GetChild(6).gameObject);

            for (int i = 2; i < 6; i++)
            {
                wrappingPaperSides.Add(gift.transform.GetChild(i).gameObject);
            }

            for (int i = 7; i < 11; i++)
            {
                dragpoints.Add(gift.transform.GetChild(i).gameObject);
            }

            gifts.Add(gift);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDragpointPositions();

        CheckTime();

        if (isGameWon && !isTimeUp && !isWaiting)
        {
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
            if (Mathf.Abs(dragpoints[i].transform.position.x - midpoints[Mathf.Clamp(i - 3, 0, 1)].transform.position.x) < 0.5f &&
                Mathf.Abs(dragpoints[i].transform.position.y - midpoints[Mathf.Clamp(i - 3, 0, 1)].transform.position.y) < 0.5f &&
                !dragpoints[i].GetComponent<Dragpoint>().isLocked())
            {
                newPos = midpoints[Mathf.Clamp(i - 3, 0, 1)].transform.position;
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

        if (index % 4 == 0)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.x - midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.x);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(newPosCoord + midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.x, wrappingPaperSides[index].transform.position.y, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(newScale, 1);
        }
        else if (index % 4 == 1)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.y - midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.y);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(wrappingPaperSides[index].transform.position.x, newPosCoord + midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.y, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(1, newScale);
        }
        else if (index % 4 == 2)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.x - midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.x);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(-newPosCoord + midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.x, wrappingPaperSides[index].transform.position.y, wrappingPaperSides[index].transform.position.z);
            newScale = (distanceToMid - 1.11f) / 1.11f;

            wrappingPaperSides[index].transform.position = newPos;
            wrappingPaperSides[index].GetComponent<SpriteRenderer>().size = new Vector2(newScale, 1);
        }
        else if (index % 4 == 3)
        {
            distanceToMid = Mathf.Abs(dragpoints[index].transform.position.y - midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.y);
            distanceToMid = Mathf.Clamp(distanceToMid, 0f, 2.22f);

            newPosCoord = 0.5f * distanceToMid + 0.55f;
            newPos = new Vector3(wrappingPaperSides[index].transform.position.x, -newPosCoord + midpoints[Mathf.Clamp(index - 3, 0, 1)].transform.position.y, wrappingPaperSides[index].transform.position.z);
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
