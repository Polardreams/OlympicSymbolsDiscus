using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discus_collision : MonoBehaviour
{
    public static Collision2D discusCollision;
    public AudioSource hit1, hit2, hit3, hit4, hit5, bounce, aim;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_raisePower_Sound ()
    {
        aim.PlayOneShot(aim.clip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        discusCollision = collision;

        /*
         *Tag System
         * alle GameObjects haben bestimmten Tag, dieser 
         * kann einer bestimmten AudioSource zugeordnet werden
         */

        switch (collision.transform.tag)
        {
            case "ground": bounce.PlayOneShot(bounce.clip); break;
            case "green": hit1.PlayOneShot(hit1.clip); break;
            case "blue": hit2.PlayOneShot(hit2.clip); break;
            case "yellow": hit3.PlayOneShot(hit3.clip); break;
            case "black": hit4.PlayOneShot(hit4.clip); break;
            case "red": hit5.PlayOneShot(hit5.clip); break;
        }
    }

}
