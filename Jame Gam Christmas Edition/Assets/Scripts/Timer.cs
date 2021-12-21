using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// file: Timer.cs
/// description: Implements a basic countdown timer
/// author: Nathan Ballay
/// </summary>
public class Timer : MonoBehaviour
{
    // Variables
    [SerializeField] float timeRemaining = 10;
    [SerializeField] Animator timerBar;
    private float maxTime;
    private bool isTimeUp = false;
    private bool drainBar = true;

    private void Start()
    {
        maxTime = timeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (drainBar)
            {
                timerBar.SetFloat("Progress", 1 - (timeRemaining / maxTime));
            }
        }
        else
        {
            isTimeUp = true;
        }
    }

    public bool IsTimeUp()
    {
        return isTimeUp;
    }

    public void StopBarDrain()
    {
        drainBar = false;
    }
}
