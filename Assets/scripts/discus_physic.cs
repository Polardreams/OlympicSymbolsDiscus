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
    public int max_crosshair_radius;
    

    private Vector2 pos_start, pos_end, pos_mov, force;

    // Start is called before the first frame update
    void Start()
    {
        debug_text.text = "Debug";

    }

    // Update is called once per frame
    void Update()
    {
        check_for_touchEvent();
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -13);
    }

    private void check_for_touchEvent()
    {
        if (Input.touches.Length == 1)
        {

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                pos_start = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
            }
            else
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    pos_end = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                    force = new Vector2(pos_start.x-pos_end.x, pos_start.y-pos_end.y);
                    discus_shoot(force);
                } else
                {
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        pos_mov = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                        force = new Vector2(pos_start.x - pos_mov.x, pos_start.y - pos_mov.y);
                        move_crosshair(force);
                    }
                }
            }
        }
    }

    private void move_crosshair (Vector2 v)
    {
        crosshair.transform.position = get_crosshair_position(v);
        
    }

    private void discus_shoot(Vector2 v)
    {
        crosshair.transform.position = get_crosshair_position(v);

        float alpha = getAlpha(v);//Festlegung, jeder Swipe nach unten erhöht den Grad
        int factor = 100;
        debug_text.text = get_ScalfaktorToForce(v, factor).ToString()+'\n';
        debug_text.text = debug_text.text + alpha + '\n';
        debug_text.text = debug_text.text + v;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 ( get_ScalfaktorToForce(v, factor).x, (alpha*(factor)) ));
    }

    private Vector2 get_crosshair_position (Vector2 v)
    {
        Vector2 pos = new Vector2 (0,0);
        float alpha = getAlpha(v);//Festlegung, jeder Swipe nach unten erhöht den Grad
        
        float r = 0;
        if (get_ScalfaktorToForce(v, 1).x < max_crosshair_radius)
        {
            r = get_ScalfaktorToForce(v, 1).x;
        } else
        {
            r = max_crosshair_radius;
        }       
        
        float x = Mathf.Cos((float) DegreeToRadian((double) alpha)) * r;//cos funktion prüfen
        float y =0;
        if (v.x>0)
        {
            y = Mathf.Sin((float)DegreeToRadian((double)alpha)) * r;
        } else
        {
            y = Mathf.Sin((float)DegreeToRadian((double)alpha)) * r*-1;
        }

        pos = new Vector2(gameObject.transform.position.x + x, gameObject.transform.position.y + y);
        return pos;
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

    private float ScreenToFrustum (float px)
    {
        float distance = cam.orthographicSize * 2;//Camera's half-size when in orthographic mode.
        float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        float ScreenHeight = Screen.height;
        float ScreenWidth = Screen.width;

        float result = (100 / ScreenWidth * px) * (frustumWidth);

        return result;
    }

    private float getAlpha (Vector2 v)
    {
        
        float alpha = (360 / (Screen.width * 0.5f)) * v.y;
        
        
        if (v.x > 0)
        {
            if (v.y>0)
            {
                if (alpha>90)
                {
                    alpha = 90;
                }
            } else
            {
                if (alpha<-90)
                {
                    alpha = -90;
                    
                }
            }
        }
        else
        {
            if (v.y > 0)
            {
                if (alpha > 90)
                {
                    alpha = 90;
                }
            }
            else
            {
                if (alpha < -90)
                {
                    alpha = -90;

                }
            }
        }
        
        return alpha;
    }

    private double DegreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}
