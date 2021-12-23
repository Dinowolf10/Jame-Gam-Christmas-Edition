using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    // Variables
    [SerializeField] float cutsceneLength;
    private float cutsceneTimer;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneTimer = cutsceneLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneTimer > 0)
        {
            cutsceneTimer -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
