using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class discus_physic : MonoBehaviour
{
    public Text debug_text;
    public GameObject crosshair;
    public Camera cam;

    private Vector2 pos_start, pos_end, force;

    // Start is called before the first frame update
    void Start()
    {
        debug_text.text = "Debug";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //...
        }

        check_for_touchEvent();
        
    }

    private void check_for_touchEvent()
    {
        if (Input.touches.Length == 1)
        {

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                //debug_text.text = "";
                //debug_text.text = debug_text.text + Input.touches[0].phase + " | " + Input.touches[0].position + '\n';
                pos_start = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
            }
            else
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    //debug_text.text = debug_text.text + Input.touches[0].phase + " | " + Input.touches[0].position + '\n';
                    pos_end = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                    force = new Vector2(pos_start.x-pos_end.x, pos_start.y-pos_end.y);
                    //debug_text.text = debug_text.text + " Force: " + force+'\n';
                    discus_shoot(force);
                }
            }
        }
    }

    private void discus_shoot(Vector2 v)
    {
        float alpha = get_ScalfaktorToForce(v, 10).y;//Festlegung, jeder Swipe nach unten erhöht den Grad
        float r = get_ScalfaktorToForce(v, 1).x;
        float x = Mathf.Cos(alpha) * r;
        float y = Mathf.Sin(alpha) * r;

        debug_text.text = debug_text.text + " Alpha: " + alpha+'\n';
        debug_text.text = debug_text.text + " Radius: " + r + '\n';
        debug_text.text = debug_text.text + " X: " + x + '\n';
        debug_text.text = debug_text.text + " Y: " + y + '\n';

        crosshair.transform.position = new Vector2(gameObject.transform.position.x+x, gameObject.transform.position.y+y);

        int factor = 100;
        get_ScalfaktorToForce(v, factor);
        gameObject.GetComponent<Rigidbody2D>().AddForce(v);
    }

    private Vector2 get_ScalfaktorToForce(Vector2 v, int factor)
    {
        /**
         * Das Frustum ist der Kameraauschnitt von dem der 
         * Spieler die virtuelle Welt betrachten kann.
         * Er wird unter anderem benötigt, wenn
         * bestimmte GameObjects immer von der Kamera erfasst werden 
         * müssen, so dass beim Verlassen des GameObjects
         * aus dem Frustum die Kamera zum Beispiel nicht
         * mehr zoomen darf. 
         * 
         * In meinem Fall benötige ich das Frustum, um einen 
         * Faktor zu errechnen, um die zugefügte Kraft auf den
         * Discus von einer Screenposition anzugeben.
         * 
         * Ich möchte keinen Willkürlichen Faktor wählen
         * 
         * https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
         * https://docs.unity3d.com/Manual/UnderstandingFrustum.html
         **/

        Vector2 scale = new Vector2(0,0);//ini
        try
        {
            float distance = cam.orthographicSize * 2;//Camera's half-size when in orthographic mode.
            float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * cam.aspect;

            float ScreenHeight = Screen.height;
            float ScreenWidth = Screen.width;

            scale = new Vector2((100/ ScreenWidth * v.x)*(frustumWidth / 100) *factor, (100/ ScreenHeight * v.y)*(frustumHeight/100)*factor);
        } catch (Exception e)
        {
            debug_text.text = e.Message;
        }
        return scale;
    }

}
