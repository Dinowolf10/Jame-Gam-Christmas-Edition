using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirPoint : MonoBehaviour
{
    // Reference grabbed in start
    [SerializeField]
    private StirringManager stirringManager;

    public bool isActivePoint = false;

    // Start is called before the first frame update
    private void Start()
    {
        stirringManager = GameObject.Find("StirringManager").GetComponent<StirringManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivePoint)
        {
            Debug.Log("Correct Point!");
            stirringManager.SetNextActivePoint();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActivePoint)
        {
            Debug.Log("Correct Point!");
            stirringManager.SetNextActivePoint();
        }
    }
}
