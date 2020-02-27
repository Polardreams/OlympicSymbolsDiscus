using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class discus_physic : MonoBehaviour
{
    //Displayinformation
    //public Text debug_text, discus_info;
    public Camera cam;
    
    //Discus
    public GameObject crosshair;
    public int max_crosshair_radius;
    public static Vector2 discus_startPosition;
    private double speed_x, speed_y;
    private bool wasMove;
    private Vector2 pos_end, pos_mov, force;
    private Vector2 pos_start;
    public Vector2 startpositionDiscus;
    public bool throwStop;

    //GamePlay - score
    public bool discusfreez;
    //public bool gravity;

    //Test
    //private GameObject r1, r2, r3, r4;
    //private PhysicsMaterial2D symbol_hit;

    //Player
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisations
        discus_startPosition = gameObject.transform.position;
        discusfreez = false;
        wasMove = false;
        throwStop = false;
        player = gameObject.transform.parent.gameObject;
        gameObject.GetComponent<Animator>().enabled = true;
        crosshair.GetComponent<SpriteRenderer>().enabled = true;
        pos_start = new Vector2(0,0);
    }


    // Update is called once per frame
    void Update()
    {
        //Cjecks
        check_for_touchEvent();
        check_freez();
        check_move();

        //Camera Settings
        //cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -13);
        //Debug Text
        //discus_info.text = "Velocity.Y: " + speed_y.ToString() + '\n' + "Velocity.X: " + speed_x.ToString() + '\n';
        //discus_info.text = discus_info.text + "Position: " + gameObject.transform.position.ToString()+ '\n' + "Respawn: "+respawn_index;
    }

    //Checks
    private void check_freez()
    {
        speed_x = Math.Round(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);//ein Discus rollt nicht weit
        speed_y = Math.Round(gameObject.GetComponent<Rigidbody2D>().velocity.y, 2);//aber er kann fliegen

        //if (gravity)
        if (true)
        {
            if (speed_y == 0 && speed_x == 0 && wasMove == true)
            {
                //gameObject.transform.position = discus_startPosition;
                //gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                //gameObject.GetComponent<Rigidbody2D>().Sleep();
                //gameObject.GetComponent<Rigidbody2D>().simulated = false;
                discusfreez = true;
                //wasMove = false;
                //hier wurde nicht das richtige Ziel getroffen
                //popUp.GetComponent<Image>().sprite = GameObject.Find("pop_message1").GetComponent<SpriteRenderer>().sprite;
                //Animator anim = popUp.GetComponent<Animator>();
                //anim.SetBool("isShort", true);
            }
        }
        else
        {
            if (speed_y == 0 && speed_x == 0 && wasMove == true)
            {
                gameObject.transform.position = discus_startPosition;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                gameObject.GetComponent<Rigidbody2D>().Sleep();
                discusfreez = true;
                wasMove = false;
            }
        }

    }

    private void check_move()
    {
        if (speed_x > 0 || speed_y > 0)
        {
            wasMove = true;
        }
    }

    private void check_for_touchEvent()
    {
        if (throwStop == false)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touches.Length == 1)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        pos_start = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                        crosshair.GetComponent<SpriteRenderer>().enabled = true;
                        player_raisePower();
                    }
                    else
                    {
                        if (Input.touches[0].phase == TouchPhase.Ended)
                        {

                            pos_end = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                            force = new Vector2(pos_start.x - pos_end.x, pos_start.y - pos_end.y);
                            discus_shoot(force);
                            crosshair.GetComponent<SpriteRenderer>().enabled = false;
                            player_throw();
                            throwStop = true;
                        }
                        else
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
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pos_start = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    crosshair.GetComponent<SpriteRenderer>().enabled = true;
                    player_raisePower();
                }
                else
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        pos_end = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        force = new Vector2(pos_start.x - pos_end.x, pos_start.y - pos_end.y);
                        discus_shoot(force);
                        crosshair.GetComponent<SpriteRenderer>().enabled = false;
                        player_throw();
                        throwStop = true;
                    }
                    else
                    {
                        if (Input.GetMouseButton(0))
                        {
                            pos_mov = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                            force = new Vector2(pos_start.x - pos_mov.x, pos_start.y - pos_mov.y);
                            move_crosshair(force);
                        }
                    }
                }
            }
        }
    }

    private void player_raisePower ()
    {
        //RaisePower
        player.GetComponent<Animator>().SetBool("rise", true);
        player.GetComponent<Animator>().SetBool("throw", false);
        gameObject.GetComponent<Animator>().SetBool("raiseDiscus", true);
    }

    private void player_throw ()
    {
        gameObject.GetComponent<Animator>().SetBool("raiseDiscus", false);
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        foreach (CircleCollider2D c in gameObject.transform.GetComponents(typeof(CircleCollider2D)))
        {
            c.enabled = true;
        }
        //Throw
        player.GetComponent<Animator>().SetBool("throw", true);
        player.GetComponent<Animator>().SetBool("rise", false);

    }

    

    //Crosshair Script
    private void move_crosshair(Vector2 v)
    {
        crosshair.transform.position = get_crosshair_position(v);
    }
   
    private Vector2 get_crosshair_position(Vector2 v)
    {
        Vector2 pos = new Vector2(0, 0);
        float alpha = getAlpha(v);//Festlegung, jeder Swipe nach unten erhöht den Grad

        float r = 0;
        if (get_ScalfaktorToForce(v, 1).x < max_crosshair_radius)
        {
            r = get_ScalfaktorToForce(v, 1).x;
        }
        else
        {
            r = max_crosshair_radius;
        }

        float x = Mathf.Cos((float)DegreeToRadian((double)alpha)) * r;//cos funktion prüfen
        float y = 0;
        if (v.x > 0)
        {
            y = Mathf.Sin((float)DegreeToRadian((double)alpha)) * r;
        }
        else
        {
            y = Mathf.Sin((float)DegreeToRadian((double)alpha)) * r * -1;
        }

        pos = new Vector2(gameObject.transform.position.x + x, gameObject.transform.position.y + y);
        return pos;
    }
    
    //Discus_physic Physic
    private void discus_shoot(Vector2 v)
    {
        //if (check_goal.firstShot==false)
        if (true)
        {
            check_goal.discus_anz--;
            check_goal.firstShot = true;
        }

        crosshair.transform.position = get_crosshair_position(v);
        float alpha = getAlpha(v);//Festlegung, jeder Swipe nach unten erhöht den Grad
        int factor = 100;
        float power = get_ScalfaktorToForce(v, factor).x;

        
        if (true)//if (gravity)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, (alpha * (factor / 2))));

            /**
             * gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, (alpha * (factor / 2))));
             * Wichtig hierbei ist, dass factir/2 -> 2 ist der Faktor in Beztug auf die CameraSize
             * Je kleiner die Kamera desto kleiner der Faktor (hier ist er 2)
             * **/

            //Test
            /*
            r1 = GameObject.Find("Ground");
            r1.GetComponent<Collider2D>().sharedMaterial = null;
            r2 = GameObject.Find("Skywalls");
            r2.GetComponent<Collider2D>().sharedMaterial = null;
            r3 = GameObject.Find("RightWall");
            r3.GetComponent<Collider2D>().sharedMaterial = null;
            r4 = GameObject.Find("LeftWall");
            r4.GetComponent<Collider2D>().sharedMaterial = null;
            */
        }
        else
        {
            /**
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, (alpha * (factor / 2))));
            //Test
            //symbol_hit = (PhysicsMaterial2D) AssetDatabase.LoadAssetAtPath("Assets/Materials/symbols_hit.physicsMaterial2D", typeof(PhysicsMaterial2D)) ;
            symbol_hit = Resources.Load("symbols_hit.physicsMaterial2D") as PhysicsMaterial2D;
            
            r1 = GameObject.Find("Ground");
            r1.GetComponent<Collider2D>().sharedMaterial = symbol_hit;
            r2 = GameObject.Find("Skywalls");
            r2.GetComponent<Collider2D>().sharedMaterial = symbol_hit;
            r3 = GameObject.Find("RightWall");
            r3.GetComponent<Collider2D>().sharedMaterial = symbol_hit;
            r4 = GameObject.Find("LeftWall");
            r4.GetComponent<Collider2D>().sharedMaterial = symbol_hit;
            **/
        }

        //debug_text.text = "Angle: " + alpha + '\n';
        //debug_text.text = debug_text.text + "Power: " + power;
    }

    //Physic Calculations
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

        Vector2 scale = new Vector2(0, 0);//ini
        try
        {
            float distance = cam.orthographicSize * 2;//Camera's half-size when in orthographic mode.
            float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * cam.aspect;

            float ScreenHeight = Screen.height;
            float ScreenWidth = Screen.width;

            scale = new Vector2((100 / ScreenWidth * v.x) * (frustumWidth / 100) * factor, 0); //Höhe wird von Alpha bestimmt
        }
        catch (Exception e)
        {
            //debug_text.text = e.Message;
        }
        return scale;
    }

    private float ScreenToFrustum(float px)
    {
        float distance = cam.orthographicSize * 2;//Camera's half-size when in orthographic mode.
        float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * cam.aspect;

        float ScreenHeight = Screen.height;
        float ScreenWidth = Screen.width;

        float result = (100 / ScreenWidth * px) * (frustumWidth);

        return result;
    }

    private float getAlpha(Vector2 v)
    {

        float alpha = (360 / (Screen.width * 0.5f)) * v.y;


        if (v.x > 0)
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

    

    /**

    public void setGravity(bool flag)
    {
        gravity = flag;
    }
    **/





}
