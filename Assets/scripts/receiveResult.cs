using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveResult : MonoBehaviour
{
    public GameObject won;
    public GameObject loos;
    

    // Start is called before the first frame update
    void Start()
    {
        if (check_goal.ScreenResult)
        {
            won.GetComponent<Canvas>().enabled = true;
        } else
        {
            loos.GetComponent<Canvas>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
