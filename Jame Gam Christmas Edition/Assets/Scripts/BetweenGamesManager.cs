using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: BetweenGamesManager.cs
/// description: Manages the state of the Between Games scene
/// author: Nathan Ballay
/// </summary>
public class BetweenGamesManager : MonoBehaviour
{
    // Objects
    [SerializeField] GameObject santa;
    [SerializeField] List<GameObject> hats;

    // References
    private GameManager gameManager;
    private Animator animator;

    // Variables
    private int numLives;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GameObject.Find("Santa").GetComponent<Animator>();

        numLives = gameManager.GetLives();

        if (numLives == 3)
        {
            hats[3].SetActive(false);
        }
        if (numLives == 2)
        {
            hats[3].SetActive(false);
            hats[2].SetActive(false);
        }
        if (numLives == 1)
        {
            hats[3].SetActive(false);
            hats[2].SetActive(false);
            hats[1].SetActive(false);
        }
        if (numLives == 0)
        {
            hats[3].SetActive(false);
            hats[2].SetActive(false);
            hats[1].SetActive(false);
            hats[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Game Result", gameManager.GetGameResult());
    }
}
