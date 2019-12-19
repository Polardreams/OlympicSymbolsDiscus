using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.orthographicSize*2*4);
        this.transform.position = pos;
        
        float distance = cam.orthographicSize * 2;//Camera's half-size when in orthographic mode.
        float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;
        Vector2 frustum = new Vector2(frustumWidth, frustumHeight);

        this.GetComponent<SpriteRenderer>().size = frustum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
