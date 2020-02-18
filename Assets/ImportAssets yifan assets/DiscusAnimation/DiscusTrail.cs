using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscusTrail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.position = Pos;
        Pos.z = 0;
        if (Input.GetMouseButton(0))
        {
            transform.position = Pos;
        }
    
    }
}
