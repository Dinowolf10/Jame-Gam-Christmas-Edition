using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingFoodManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform knife;

    private Vector2 mousePos;

    [SerializeField]
    private float throwForce = 1f;

    // Populated in editor
    [SerializeField]
    private List<Rigidbody2D> foods;

    // Populated in editor
    [SerializeField]
    private List<Rigidbody2D> santaHats;
    
    [SerializeField]
    private List<Rigidbody2D> foodsToChop;

    private bool isCutting = false;
    private bool isWaiting = false;

    private GameManager gameManager;
    private SoundManager soundManager;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        ThrowFoodUp();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isWaiting || gameManager.isGamePaused())
        {
            return;
        }

        UpdateKnife();

        /*if (Input.GetMouseButtonDown(0))
        {
            isCutting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCutting = false;
        }

        if (isCutting)
        {
            CheckObjectHit();
        }*/
    }

    /// <summary>
    /// Updates knife position to current mouse position
    /// </summary>
    private void UpdateKnife()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        knife.position = new Vector2(mousePos.x, mousePos.y);
    }

    private void ThrowFoodUp()
    {
        int roundNumber = gameManager.GetRoundNumber();
        //int roundNumber = 2;
        int i = 0;
        Rigidbody2D f;

        if (roundNumber == 1)
        {
            i = 2;
        }
        else if (roundNumber == 2)
        {
            i = 4;
        }

        while (i > 0)
        {
            santaHats[i - 1].AddForce(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0.75f, 1.5f) * throwForce), ForceMode2D.Impulse);

            f = foods[Random.Range(0, foods.Count)];
            f.AddForce(new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(0.8f, 1.2f) * throwForce), ForceMode2D.Impulse);
            Debug.Log(f.name);
            foodsToChop.Add(f);
            foods.Remove(f);
            i--;
        }
    }

    /// <summary>
    /// Checks if an object was hit, if there was an object then check if the object is an object to get
    /// </summary>
    private void CheckObjectHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            if (hit.transform.gameObject.tag == "ChoppableFood")
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);

                hit.transform.GetComponent<ChoppableFood>().GetChopped();

                hit.transform.GetComponent<BoxCollider2D>().enabled = false;

                hit.transform.GetComponent<Renderer>().enabled = false;

                foodsToChop.Remove(hit.transform.GetComponent<Rigidbody2D>());

                if (foodsToChop.Count == 0 && !isWaiting)
                {
                    isWaiting = true;
                    gameManager.WonMiniGame();
                }
            }
            else if (hit.transform.gameObject.tag == "SantaHat" && !isWaiting)
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);

                isWaiting = true;
                gameManager.LostMiniGame();
            }
        }
    }

    public void RemoveFoodToChop(Rigidbody2D rb)
    {
        foodsToChop.Remove(rb);

        if (foodsToChop.Count == 0 && !isWaiting)
        {
            isWaiting = true;
            gameManager.WonMiniGame();
        }
    }

    public void LoseGame()
    {
        isWaiting = true;
        gameManager.LostMiniGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (foodsToChop.Contains(collision.GetComponent<Rigidbody2D>()) && !isWaiting)
        {
            Debug.Log("Hit " + collision.transform.gameObject.name);

            isWaiting = true;
            gameManager.LostMiniGame();
        }
    }

    public Vector2 GetMousePosition()
    {
        return mousePos;
    }
}
