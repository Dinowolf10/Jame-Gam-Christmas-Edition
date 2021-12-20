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
    private GameObject gameManager;

    // Reference stored in inspector
    [SerializeField]
    private Timer timer;

    // Array populated in inspector
    public GameObject[] objects;

    // Populated at start
    public Dictionary<string, GameObject> objectsToGet = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
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
        if (timer.IsTimeUp())
        {
            Debug.Log("You Lose!");
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
                    Debug.Log("You Win!");
                }
            }
        }
    }
}
