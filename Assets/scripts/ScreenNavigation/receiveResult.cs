using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveResult : MonoBehaviour
{
    public GameObject gold;
    public GameObject silver;
    public GameObject bronxe;


    // Start is called before the first frame update
    void Start()
    {
        
        switch (check_goal.medal_index)
        {
            case 0: bronxe.GetComponent<Canvas>().enabled = true; break;
            case 1: silver.GetComponent<Canvas>().enabled = true; break;
            case 2: gold.GetComponent<Canvas>().enabled = true; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
