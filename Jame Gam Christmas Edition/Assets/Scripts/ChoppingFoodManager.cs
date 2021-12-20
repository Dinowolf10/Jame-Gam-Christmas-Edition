using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingFoodManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float throwForce = 1f;

    // Populated in editor
    [SerializeField]
    private List<Rigidbody2D> foods;

    private bool isCutting = false;

    // Start is called before the first frame update
    private void Start()
    {
        ThrowFoodUp();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        }
    }

    private void ThrowFoodUp()
    {
        foods[0].AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
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

                foods.Remove(hit.transform.GetComponent<Rigidbody2D>());

                if (foods.Count == 0)
                {
                    Debug.Log("You Win!");
                }
            }
        }
    }
}
