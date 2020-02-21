using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firework_position : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 startPos = gameObject.transform.position;
        gameObject.transform.position = new Vector2(startPos.x, startPos.y-23);
    }


}
