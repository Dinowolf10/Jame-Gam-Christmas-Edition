using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGiftsManager : MonoBehaviour
{
    // Reference stored in inspector
    [SerializeField]
    private Camera cam;

    // Reference stored in inspector
    [SerializeField]
    private Timer timer;

    // Array populated in inspector
    public GameObject[] objects;

    // Populated at start
    public Dictionary<string, GameObject> objectsToGet = new Dictionary<string, GameObject>();

    private GameManager gameManager;

    private bool hasWon = false;

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
        if (timer.IsTimeUp() && !hasWon)
        {
            gameManager.LostMiniGame();
        }
    }

    /// <summary>
    /// Populates dictionary of objects to get
    /// </summary>
    private void PopulateObjectsToGet()
    {
        objectsToGet.Add(objects[0].name, objects[0]);
    }

    /// <summary>
    /// Checks if an object was hit, if there was an object then check if the object is an object to get
    /// </summary>
    private void CheckObjectHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            string name = hit.transform.gameObject.name;

            if (objectsToGet.ContainsKey(name))
            {
                objectsToGet[name].SetActive(false);

                objectsToGet.Remove(name);

                Debug.Log("Got Object!");

                if (objectsToGet.Count == 0)
                {
                    timer.StopBarDrain();
                    gameManager.WonMiniGame();
                    hasWon = true;
                }
            }
        }
    }
}
