using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppableFood : MonoBehaviour
{
    // Reference set in inspector
    [SerializeField]
    private GameObject piece1, piece2;

    [SerializeField]
    private float choppedForce = 0.1f;

    public void GetChopped()
    {
        piece1.SetActive(true);
        piece2.SetActive(true);

        piece1.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.0f * choppedForce, 0.3f), ForceMode2D.Impulse);
        piece2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1.0f * choppedForce, 0.3f), ForceMode2D.Impulse);
    }
}
