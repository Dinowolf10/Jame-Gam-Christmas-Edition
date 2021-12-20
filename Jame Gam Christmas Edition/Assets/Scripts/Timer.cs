using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: Timer.cs
/// description: Implements a basic countdown timer
/// author: Nathan Ballay
/// </summary>
public class Timer : MonoBehaviour
{
    // Variables
    [SerializeField] float timeRemaining = 10;
    private bool isTimeUp = false;

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
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
}